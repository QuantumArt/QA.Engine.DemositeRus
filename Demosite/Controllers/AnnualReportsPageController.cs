using Microsoft.AspNetCore.Mvc;
using Demosite.Models.Pages;
using QA.DotNetCore.Engine.Routing;
using Demosite.ViewModels.Builders;

namespace Demosite.Controllers
{
    public class AnnualReportsPageController : ContentControllerBase<AnnualReportsPage>
    {
        private AnnualReportPageViewModelBuilder modelBuilder { get; }
        public AnnualReportsPageController(AnnualReportPageViewModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }
        public IActionResult Index()
        {
            var ids = CurrentItem.ReportsItemIds; ;
            var vm = modelBuilder.BuildForm(CurrentItem, ids);
            vm.CountReportsToShow = CurrentItem.CountReportToShow;
            return View(vm);
        }
    }
}
