using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using Demosite.ViewModels.Helpers;
using QA.DotNetCore.Engine.Abstractions;
using System.Linq;

namespace Demosite.ViewModels.Builders
{
    public class MediaPageViewModelBuilder
    {
        private readonly IMediaService _mediaService;
        public MediaPageViewModelBuilder(IMediaService service)
        {
            _mediaService = service;
        }
        public MediaPageViewModel BuildList(IAbstractPage widget)
        {
            MediaPageViewModel viewModel = new() { Title = widget.Title };
            EventItem[] events = _mediaService.GetAllEvents().Select(Map).ToArray();
            viewModel.Events.AddRange(events);
            viewModel.BreadCrumbs = widget.GetBreadCrumbs();
            return viewModel;
        }
        private EventItem Map(EventDto model)
        {
            return new EventItem()
            {
                Id = model.Id,
                Title = model.Title,
                Text = model.Text,
                TextBelow = model.TextBelow,
                Images = model.EventImages.Select(Map).ToList()
            };
        }

        private EventImageItem Map(EventImageDto image)
        {
            return new EventImageItem()
            {
                Title = image.Title,
                SortOrder = image.SortOrder ?? 100,
                ImageURL = image.ImageURL
            };
        }
    }
}
