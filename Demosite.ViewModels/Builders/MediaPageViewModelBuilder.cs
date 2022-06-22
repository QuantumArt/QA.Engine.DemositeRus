using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using Demosite.ViewModels.Helpers;
using QA.DotNetCore.Engine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosite.ViewModels.Builders
{
    public class MediaPageViewModelBuilder
    {
        private IMediaService mediaService { get; }
        public MediaPageViewModelBuilder(IMediaService service)
        {
            this.mediaService = service;
        }
        public MediaPageViewModel BuildList(IAbstractPage widget)
        {
            var vm = new MediaPageViewModel() {Title = widget.Title };
            var events = mediaService.GetAllEvents().Select(Map).ToArray();
            vm.Events.AddRange(events);
            vm.BreadCrumbs = widget.GetBreadCrumbs();
            return vm;
        }
        private EventItem Map (EventDto model)
        {
            return new EventItem()
            {
                Id= model.Id,
                Title = model.Title,
                Text = model.Text,
                TextBelow = model.TextBelow,
                Images = model.EventImages.Select(Map).ToList()
            };
        }

        private EventImageItem Map (EventImageDto image)
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
