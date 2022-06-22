using Demosite.Interfaces.Dto;
using System.Collections.Generic;

namespace Demosite.Interfaces
{
    public interface IMeetingService
    {
        IEnumerable<MeetingDto> GetMeetings(bool includeArchive = false);
        MeetingDto GetMeeting(int id);
    }
}
