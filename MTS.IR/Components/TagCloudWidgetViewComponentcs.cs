﻿using Microsoft.AspNetCore.Mvc;
using MTS.IR.ViewModels.Builders;
using QA.DotNetCore.Engine.QpData;
using QA.DotNetCore.Engine.Widgets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTS.IR.Components
{
    public class TagCloudWidgetViewComponent : WidgetComponentBase<AbstractWidget>
    {
        public TagCloudWidgetViewComponent(BlogPageViewModelBuilder builder)
        {
            Builder = builder;
        }

        public BlogPageViewModelBuilder Builder { get; }

        public override Task<IViewComponentResult> RenderAsync(AbstractWidget widget, IDictionary<string, object> argumets)
        {
            return Task.FromResult<IViewComponentResult>(View(Builder.BuildTags()));
        }
    }
}
