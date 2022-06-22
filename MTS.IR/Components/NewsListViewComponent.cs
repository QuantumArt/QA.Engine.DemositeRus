using Microsoft.AspNetCore.Mvc;
using MTS.IR.ViewModels.Builders;
using QA.DotNetCore.Engine.Abstractions;
using System.Threading.Tasks;

namespace MTS.IR.Components
{
    public class NewsListViewComponent : ViewComponent
    {
        private NewsPageViewModelBuilder NewsPageViewModelBuilder { get; }
        public NewsListViewComponent(NewsPageViewModelBuilder newsPageViewModelBuilder)
        {
            this.NewsPageViewModelBuilder = newsPageViewModelBuilder;
        }

        public Task<IViewComponentResult> InvokeAsync(IAbstractPage CurrentItem, int? year, int? month, int? categoryId, int? page)
        {
            var vm = NewsPageViewModelBuilder.BuildList(CurrentItem, year, month, categoryId, page ?? 1);
            return Task.FromResult<IViewComponentResult>(View(vm));
        }
    }
}
