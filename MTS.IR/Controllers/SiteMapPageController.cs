using Microsoft.AspNetCore.Mvc;
using MTS.IR.Models.Pages;
using QA.DotNetCore.Engine.Routing;
using MTS.IR.ViewModels.Builders;
using System.Xml;
using Microsoft.AspNetCore.Http.Features;
using System.Collections.Generic;
using System.Linq;

namespace MTS.IR.Controllers
{
    public class SiteMapPageController : ContentControllerBase<SiteMapPage>
    {
        private MenuViewModelBuilder _builder { get; }
        public SiteMapPageController(MenuViewModelBuilder builder)
        {
            this._builder = builder;
        }
        public IActionResult Index()
        {
            var result = _builder.Build(StartPage, CurrentItem);
            return View(result.Items);
        }

        [Route("/sitemap.xml")]
        public void SitemapXml()
        {
            var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
            if (syncIOFeature != null)
            {
                syncIOFeature.AllowSynchronousIO = true;
            }
            string host = Request.Scheme + "://" + Request.Host;

            Response.ContentType = "application/xml";

            using (var xml = XmlWriter.Create(Response.Body, new XmlWriterSettings { Indent = true }))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");
                xml.WriteStartElement("url");
                xml.WriteElementString("loc", host);
                xml.WriteEndElement();
                var sitemap = _builder.Build(StartPage, CurrentItem);
                SiteMap(sitemap.Items, xml);
                xml.WriteEndElement();
            }
        }

        private void SiteMap(IEnumerable<MTS.IR.ViewModels.MenuItem> sitemap, XmlWriter xml)
        {
            foreach (var page in sitemap)
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
