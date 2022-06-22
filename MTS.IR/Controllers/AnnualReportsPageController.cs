using Microsoft.AspNetCore.Mvc;
using MTS.IR.Models.Pages;
using QA.DotNetCore.Engine.Routing;
using MTS.IR.ViewModels.Builders;

namespace MTS.IR.Controllers
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
