using MTS.IR.Interfaces.Dto;
using System.Collections.Generic;

namespace MTS.IR.Interfaces
{
    public interface IMeetingService
    {
        IEnumerable<MeetingDto> GetMeetings(bool includeArchive = false);
        MeetingDto GetMeeting(int id);
    }
}
