using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Demosite.Interfaces;
using Demosite.Middlewares.Captcha;
using Demosite.Models.Pages;
using Demosite.Postgre.DAL;
using Demosite.Postgre.DAL.NotQP;
using Demosite.Services;
using Demosite.Services.Hosted;
using Demosite.Services.Settings;
using Demosite.Templates;
using Demosite.ViewModels.Builders;
using Npgsql;
using QA.DotNetCore.Engine.Abstractions;
using QA.DotNetCore.Engine.Abstractions.OnScreen;
using QA.DotNetCore.Engine.AbTesting.Configuration;
using QA.DotNetCore.Engine.CacheTags;
using QA.DotNetCore.Engine.CacheTags.Configuration;
using QA.DotNetCore.Engine.OnScreen.Configuration;
using QA.DotNetCore.Engine.Persistent.Interfaces;
using QA.DotNetCore.Engine.Persistent.Interfaces.Settings;
using QA.DotNetCore.Engine.QpData.Configuration;
using QA.DotNetCore.Engine.Routing.Configuration;
using System;
using RazorLight;

namespace Demosite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            var mvc = services.AddMvc(o =>
            {
                //o.EnableEndpointRouting = false;
            }).AddRazorRuntimeCompilation();

            services.AddLogging();
            services.AddSingleton<ILogger>(provider => provider.GetRequiredService<ILogger<Program>>());
            var qpSettings = Configuration.GetSection("QpSettings").Get<QpSettings>();
            if (!Enum.TryParse(qpSettings.DatabaseType, true, out DatabaseType dbType))
            {
                dbType = DatabaseType.SqlServer;
            }
            qpSettings.ConnectionString = Configuration.GetConnectionString("DatabaseQPPostgre");
            services.AddSingleton(qpSettings);

            //структура сайта виджетной платформы
            services.AddSiteStructureEngine(options =>
            {
                options.UseQpSettings(qpSettings);
                options.TypeFinder.RegisterFromAssemblyContaining<RootPage, IAbstractItem>();
            });

            //ef контекст

            services.AddScoped<NpgsqlConnection>(_ => new NpgsqlConnection(qpSettings.ConnectionString));
            services.AddScoped<IDbContext>(sp => PostgreQpDataContext.CreateWithStaticMapping(
                qpSettings.IsStage
                ? Quantumart.QP8.EntityFrameworkCore.Generator.Models.ContentAccess.Stage
                : Quantumart.QP8.EntityFrameworkCore.Generator.Models.ContentAccess.Live,
                sp.GetService<NpgsqlConnection>()));
            //ef контекст без использования QP

            services.AddDbContext<IDbContextNotQP, PostgreDataContext>(options =>
            {
                options.UseNpgsql(qpSettings.ConnectionString);
            });

            //сервисы слоя данных
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IFoldBoxListService, FoldBoxListService>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<IReportsService, ReportsService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IBannerWidgetService, BannerWidgetService>();


            //сервисы построения view-model
            services.AddScoped<NewsPageViewModelBuilder>();
            services.AddScoped<NewsRoomViewModelBuilder>();
            services.AddSingleton<MenuViewModelBuilder>();
            services.AddScoped<FoldBoxListViewModelBuilder>();
            services.AddScoped<SubscribeViewModelBuilder>();
            services.AddScoped<MediaPageViewModelBuilder>();
            services.AddScoped<FeedbackViewModelBuilder>();
            services.AddScoped<BannerWidgetViewModelBuilder>();
            services.AddScoped<AnnualReportPageViewModelBuilder>();

            services.AddAbTestServices(options =>
            {
                options.UseQpSettings(qpSettings);
            });

            services.AddScoped<CacheTagUtilities>();
            services.AddCacheTagServices(options =>
            {
                if (qpSettings.IsStage)
                {
                    options.InvalidateByMiddleware(@"^.*\/.+\.[a-zA-Z0-9]+$");//отсекаем левые запросы для статики (для каждого сайта может настраиваться индивидуально)
                }
                else
                {
                    options.InvalidateByTimer(TimeSpan.FromSeconds(30));
                }
            });
            //возможность работы с режимом onscreen
            services.AddOnScreenIntegration(mvc, options =>
            {
                options.AdminSiteBaseUrl = Configuration.GetSection("OnScreen").Get<OnScreenSettings>().AdminSiteBaseUrl;
                options.AvailableFeatures = qpSettings.IsStage ? OnScreenFeatures.Widgets | OnScreenFeatures.AbTests : OnScreenFeatures.None;
                options.UseQpSettings(qpSettings);
            });

            //сервис рассылки новостей
            var newsNotificationServiceSettings = Configuration.GetSection("NewsNotificationServiceConfig").Get<EmailNotificationSettings>();
            services.AddSingleton(newsNotificationServiceSettings);
            services.AddScoped<IWarmUp, WarmUp>();
            if (newsNotificationServiceSettings.NotificationServiceIsActive)
            {
                services.AddHostedService<EmailNotificationHostedService>();
            }
            services.AddScoped<IEmailNotificationService, EmailNotificationService>();
            services.AddScoped<INotificationTemplateEngine, NotificationTemplateEngine>();
            var engine = new RazorLightEngineBuilder()
            .UseEmbeddedResourcesProject(System.Reflection.Assembly.GetEntryAssembly())
            .UseMemoryCachingProvider()
            .Build();
            services.AddSingleton(engine);

            //Captcha
            var captchaSettings = Configuration.GetSection("CaptchaSettings").Get<CaptchaSettings>();
            services.AddSingleton(captchaSettings);

            services.AddMemoryCache();
            services.AddScoped<ICacheService, CacheService>();

            services.AddControllersWithViews(options =>
            {
                options.CacheProfiles.Add("Caching",
                    new CacheProfile
                    {
                        Location = ResponseCacheLocation.Any,
                        Duration = int.Parse(Configuration["Cache:Duration"])
                    });
            });
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.            
            }


            app.UseStaticFiles();
            app.UseSession();
            //включаем инвалидацию по кештегам QP
            app.UseCacheTagsInvalidation(invalidation =>
            {
                invalidation.Register<QpContentCacheTracker>();
            });

            //активируем структуру сайта (добавляем в http-контекст структуру сайта)
            app.UseSiteStructure();

            app.UseRouting();

            var qpSettings = Configuration.GetSection("QpSettings").Get<QpSettings>();
            app.UseOnScreenMode(qpSettings.CustomerCode);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAbtestEndpointRoute();
                endpoints.MapSiteStructureControllerRoute();
                endpoints.MapControllers();
            });
            app.UseCaptchaImage("/captcha");
            PostgreQpDataContext.SetHttpContextAccessor(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
        }
    }
}
