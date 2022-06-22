using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MTS.IR.Models.Pages;
using QA.DotNetCore.Engine.Abstractions;
using QA.DotNetCore.Engine.Routing;

namespace MTS.IR.Controllers
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
