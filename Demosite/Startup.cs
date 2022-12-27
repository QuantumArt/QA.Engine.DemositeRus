using Demosite.Interfaces;
using Demosite.Middlewares.Captcha;
using Demosite.Models.Pages;
using Demosite.Postgre.DAL;
using Demosite.Postgre.DAL.NotQP;
using Demosite.Services;
using Demosite.Services.Hosted;
using Demosite.Services.Search;
using Demosite.Services.Settings;
using Demosite.ViewModels.Builders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Provider.Search;
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
using RazorLight;
using SixLabors.Fonts;
using SixLaborsCaptcha.Mvc.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

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
            services.AddResponseCompression(options => options.EnableForHttps = true);

            var httpCacheControl = Configuration.GetSection("HttpCacheControl").Get<HttpCacheControl>();
            var mvc = services.AddMvc(options =>
            {
                options.CacheProfiles.Add("Caching",
                    new CacheProfile
                    {
                        Location = ResponseCacheLocation.Any,
                        Duration = (int)httpCacheControl.MaxAge.TotalSeconds
                    });
            }).AddRazorRuntimeCompilation();

            services.AddLogging();
            var qpSettings = Configuration.GetSection("QpSettings").Get<QpSettings>();
            if (!Enum.TryParse(qpSettings.DatabaseType, true, out DatabaseType dbType))
            {
                dbType = DatabaseType.SqlServer;
            }
            qpSettings.ConnectionString = Configuration.GetConnectionString("DatabaseQPPostgre");
            services.AddSingleton(qpSettings);

            services.AddScoped<ISiteSettingsService, SiteSettingsService>();

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

            services.Configure<CacheSettings>(Configuration.GetSection("Cache"));

            services.AddScoped<CacheTagUtilities>();
            var cascheTagService = services.AddCacheTagServices();
            if (qpSettings.IsStage)
            {
                cascheTagService.WithInvalidationByMiddleware(@"^.*\/.+\.[a-zA-Z0-9]+$");
            }
            else
            {
                cascheTagService.WithInvalidationByTimer();
            }
            //включаем инвалидацию по кештегам QP
            cascheTagService.WithCacheTrackers(invalidation =>
            {
                //QpContentCacheTracker - уже реализованный ICacheTagTracker, который работает на базе механизма CONTENT_MODIFICATION из QP
                invalidation.Register<QpContentCacheTracker>();
            });
            //возможность работы с режимом onscreen
            services.AddOnScreenIntegration(mvc, options =>
            {
                options.AdminSiteBaseUrl = Configuration.GetSection("OnScreen").Get<OnScreenSettings>().AdminSiteBaseUrl;
                options.AvailableFeatures = qpSettings.IsStage ? OnScreenFeatures.Widgets | OnScreenFeatures.AbTests : OnScreenFeatures.None;
                options.UseQpSettings(qpSettings);
            });

            //сервис рассылки новостей
            services.AddScoped<IWarmUp, WarmUp>();
            var notificationIsActive = Configuration.GetSection("NewsNotificationServiceConfig").GetSection("NotificationServiceIsActive").Get<bool>();
            var newsNotificationServiceSettings = notificationIsActive
                ? Configuration.GetSection("NewsNotificationServiceConfig").Get<EmailNotificationSettings>()
                : new EmailNotificationSettings() { NotificationServiceIsActive = notificationIsActive };
            if (newsNotificationServiceSettings.NotificationServiceIsActive)
            {
                services.AddHostedService<EmailNotificationHostedService>();
            }
            services.AddSingleton(newsNotificationServiceSettings);
            services.AddScoped<IEmailNotificationService, EmailNotificationService>();
            services.AddScoped<INotificationTemplateEngine, NotificationTemplateEngine>();
            var engine = new RazorLightEngineBuilder()
            .UseEmbeddedResourcesProject(System.Reflection.Assembly.GetEntryAssembly())
            .UseMemoryCachingProvider()
            .Build();
            services.AddSingleton(engine);

            //Captcha
            var captchaIsActive = Configuration.GetSection("CaptchaSettings").GetSection("IsActive").Get<bool>();
            CaptchaSettings captchaSettings = captchaIsActive
                ? Configuration.GetSection("CaptchaSettings").Get<CaptchaSettings>()
                : new CaptchaSettings() { IsActive = captchaIsActive };
            if (captchaSettings.IsActive)
            {
                var colors = captchaSettings.GetColors();
                services.AddSixLabCaptcha(x =>
                {
                    x.Width = captchaSettings.CaptchaWidth;
                    x.Height = captchaSettings.CaptchaHeight;
                    x.FontSize = captchaSettings.FontSize;
                    x.MaxRotationDegrees = captchaSettings.MaxAngle;
                    x.NoiseRate = (ushort)captchaSettings.BackgroundNoiseLevel;
                    x.DrawLines = captchaSettings.DrawLineNoise;
                    x.FontFamilies = new string[] { SystemFonts.Families.FirstOrDefault().Name };
                    if (colors.Length > 0)
                    {
                        x.TextColor = colors;
                    }
                });
            }
            services.AddSingleton(captchaSettings);

            services.AddScoped<ICacheService, CacheService>();

            services.AddSearch(Configuration);
            services.AddScoped<ISearchService, SearchService>();
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
            }

            app.UseStatusCodePages(ctx =>
            {
                if (ctx.HttpContext.Response.StatusCode > 400)
                    ctx.HttpContext.Response.Redirect("/Error");

                return Task.CompletedTask;
            });

            app.UseResponseCompression();

            var httpCacheControl = Configuration.GetSection("HttpCacheControl").Get<HttpCacheControl>();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public,max-age={httpCacheControl.StaticFilesMaxAge.TotalSeconds}");
                }
            });
            app.UseSession();
            //включаем инвалидацию по кештегам QP
            app.UseCacheTagsInvalidation();

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

            var captchaIsActive = Configuration.GetSection("CaptchaSettings").GetSection("IsActive").Get<bool>();
            if (captchaIsActive)
            {
                app.UseCaptchaImage("/captcha");
            }

            PostgreQpDataContext.SetHttpContextAccessor(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
        }
    }
}
