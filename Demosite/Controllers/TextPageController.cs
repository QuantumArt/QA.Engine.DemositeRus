using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Demosite.Models.Pages;
using QA.DotNetCore.Engine.Abstractions;
using QA.DotNetCore.Engine.Routing;

namespace Demosite.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class TextPageController : ContentControllerBase<TextPage>
    {
        AbstractItemStorage _provider;
        private static ILogger<TextPageController> _logger;

        public TextPageController(IAbstractItemStorageProvider abstractItemProvider, ILogger<TextPageController> logger)
        {
            _provider = abstractItemProvider.Get();
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(CurrentItem);
        }
    }
}
