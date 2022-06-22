using Microsoft.EntityFrameworkCore;
using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using Demosite.Postgre.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demosite.Services
{
    public class MediaService : IMediaService
    {
        private PostgreQpDataContext qpDataContext { get; }
        public MediaService(IDbContext context)
        {
            qpDataContext = context as PostgreQpDataContext;
        }

        public IEnumerable<EventDto> GetAllEvents()
        {
            var results = qpDataContext.Events.OrderByDescending(e => e.EventDate)
                                       .Include(e => e.EventImages.OrderBy(i => i.SortOrder))
                                       .ToArray();
            return results.Select(Map).ToArray();
        }

        private EventDto Map (Postgre.DAL.Event model)
        {
            return new EventDto()
            {
                Id = model.Id,
                Text = model.Text,
                Title = model.Title,
                TextBelow = model.TextBelow,
                EventImages = model.EventImages.Select(Map).ToArray(),
            };
        }

        private EventImageDto Map (Postgre.DAL.EventImage image)
        {
            return new EventImageDto()
            {
                Id = image.Id,
                SortOrder = image.SortOrder,
                ImageURL = image.ImageUrl,
                Title = image.Title
            };
        }
    }
}
