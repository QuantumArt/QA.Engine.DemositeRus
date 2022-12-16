using Demosite.Models.Pages;
using Demosite.ViewModels;
using Demosite.ViewModels.Builders;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using QA.DotNetCore.Engine.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Demosite.Controllers
{
    public class SiteMapPageController : ContentControllerBase<SiteMapPage>
    {
        private readonly MenuViewModelBuilder _builder;
        public SiteMapPageController(MenuViewModelBuilder builder)
        {
            _builder = builder;
        }
        public IActionResult Index()
        {
            ViewModels.MenuViewModel result = _builder.Build(StartPage, CurrentItem);
            return View(result.Items);
        }

        [Route("/sitemap.xml")]
        public void SitemapXml()
        {
            IHttpBodyControlFeature syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
            if (syncIOFeature != null)
            {
                syncIOFeature.AllowSynchronousIO = true;
            }
            string host = Request.Scheme + "://" + Request.Host;

            Response.ContentType = "application/xml";

            using XmlWriter xml = XmlWriter.Create(Response.Body, new XmlWriterSettings { Indent = true });
            xml.WriteStartDocument();
            xml.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");
            xml.WriteStartElement("url");
            xml.WriteElementString("loc", host);
            xml.WriteEndElement();
            MenuViewModel sitemap = _builder.Build(StartPage, CurrentItem);
            SiteMap(sitemap.Items, xml);
            xml.WriteEndElement();
        }

        private void SiteMap(IEnumerable<Demosite.ViewModels.MenuItem> sitemap, XmlWriter xml)
        {
            foreach (ViewModels.MenuItem page in sitemap)
            {
                string url = Request.Scheme + "://" + Request.Host + page.Href;
                xml.WriteStartElement("url");
                xml.WriteElementString("loc", url);
                xml.WriteEndElement();
                if (page.Children.Any())
                {
                    SiteMap(page.Children, xml);
                }
            }
        }
    }
}
