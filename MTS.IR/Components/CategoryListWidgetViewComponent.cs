using Microsoft.AspNetCore.Mvc;
using MTS.IR.ViewModels.Builders;
using QA.DotNetCore.Engine.QpData;
using QA.DotNetCore.Engine.Widgets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTS.IR.Components
{
    public class CategoryListWidgetViewComponent: WidgetComponentBase<AbstractWidget>
    {
        public CategoryListWidgetViewComponent(NewsPageViewModelBuilder builder)
        {
            Builder = builder;
        }

        private NewsPageViewModelBuilder Builder { get; }
        public override Task<IViewComponentResult> RenderAsync(AbstractWidget widget, IDictionary<string, object> argumets)
        {
            return Task.FromResult<IViewComponentResult>(View(Builder.BuildCategories()));
        }
    }
}
