using Microsoft.AspNetCore.Mvc;
using Demosite.Models.Pages;
using QA.DotNetCore.Engine.Routing;
using Demosite.ViewModels.Builders;

namespace Demosite.Controllers
{
    public class AnnualReportsPageController : ContentControllerBase<AnnualReportsPage>
    {
        private AnnualReportsPageViewModelBuilder modelBuilder { get; }
        public AnnualReportsPageController(AnnualReportsPageViewModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }
        public IActionResult Index()
        {
            var id = CurrentItem.ReportItemId;
            var vm = modelBuilder.BuildForm(CurrentItem, id);
            return View(vm);
        }
    }
}
