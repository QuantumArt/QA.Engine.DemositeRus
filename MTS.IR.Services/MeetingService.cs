using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using MTS.IR.Interfaces;
using MTS.IR.Interfaces.Dto;
using MTS.IR.Postgre.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MTS.IR.Services
{
    public class MeetingService : IMeetingService
    {
        public IDbContext QpDataContext { get; }
        public ICacheService MemoryCache { get; }
        public MeetingService(IDbContext context,
                              ICacheService memoryCache)
        {
            this.QpDataContext = context;
            MemoryCache = memoryCache;
        }

        public MeetingDto GetMeeting(int id)
        {
            return MemoryCache.GetFromCache<MeetingDto>(GetCacheKey(id), () =>
            {
                return Map((QpDataContext as PostgreQpDataContext).Meetings
                  .FirstOrDefault(m => m.Id == id));
            });
        }

        public IEnumerable<MeetingDto> GetMeetings(bool includeArchive = false)
        {
            return MemoryCache.GetFromCache<IEnumerable<MeetingDto>>(GetCacheKey(includeArchive), () =>
            {
                var query = (QpDataContext as PostgreQpDataContext).Meetings.AsQueryable();
                if (!includeArchive)
                {
                    query = query.Where(m => m.IsArchive == false);
                }
                return query.OrderByDescending(m => m.MeetingDate ?? new System.DateTime())
                            .Select(Map)
                            .ToArray();
            });
        }

        private MeetingDto Map(Postgre.DAL.Meeting meeting)
        {
            if (meeting == null)
                return null;
            return new MeetingDto()
            {
                Id = meeting.Id,
                IsArchive = meeting.IsArchive ?? false,
                MeetingDate = meeting.MeetingDate,
                Text = meeting.Text,
                Title = meeting.Title
            };
        }

        static private string GetCacheKey(int id)
        {
            return $"meeting_{id}";
        }

        static private string GetCacheKey(bool includeArchive)
        {
            return $"meetings_all_{includeArchive}";
        }
    }
}
