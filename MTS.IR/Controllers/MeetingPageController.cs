using Microsoft.AspNetCore.Mvc;
using MTS.IR.Helpers;
using MTS.IR.Models.Pages;
using MTS.IR.ViewModels.Builders;
using QA.DotNetCore.Engine.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTS.IR.Controllers
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
