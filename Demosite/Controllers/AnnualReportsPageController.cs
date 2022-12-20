using Demosite.Models.Pages;
using Demosite.ViewModels;
using Demosite.ViewModels.Builders;
using Microsoft.AspNetCore.Mvc;
using QA.DotNetCore.Engine.Routing;
using System.Collections.Generic;

namespace Demosite.Controllers
{
    public class AnnualReportsPageController : ContentControllerBase<AnnualReportsPage>
    {
        private readonly AnnualReportPageViewModelBuilder _modelBuilder;
        public AnnualReportsPageController(AnnualReportPageViewModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }
        public IActionResult Index()
        {
            IEnumerable<int> ids = CurrentItem.ReportsItemIds;
            AnnualReportsPageViewModel viewModel = _modelBuilder.BuildForm(CurrentItem, ids);
            viewModel.CountReportsToShow = CurrentItem.CountReportToShow;
            return View(viewModel);
        }
    }
}
