using Microsoft.EntityFrameworkCore;
using MTS.IR.Interfaces;
using MTS.IR.Interfaces.Dto;
using MTS.IR.Postgre.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MTS.IR.Services
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
