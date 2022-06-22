using Microsoft.AspNetCore.Mvc;
using Demosite.Helpers;
using Demosite.Models.Pages;
using Demosite.ViewModels.Builders;
using QA.DotNetCore.Engine.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demosite.Controllers
{
    public class MeetingPageController : ContentControllerBase<MeetingPage>
    {
        private MeetingPageViewModelBuilder _modelBuilder { get; }
        public MeetingPageController(MeetingPageViewModelBuilder modelBuilder)
        {
            this._modelBuilder = modelBuilder;
        }
        public IActionResult Index(bool includeArchive = true)
        {
            var vm = _modelBuilder.BuildList(CurrentItem, includeArchive);
            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var vm = _modelBuilder.BuidDetails(CurrentItem, id);
            return View(vm);
        }
    }
}
