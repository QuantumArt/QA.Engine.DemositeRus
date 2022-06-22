using MTS.IR.Interfaces.Dto;
using MTS.IR.Interfaces.Dto.Request;
using System.Collections.Generic;

namespace MTS.IR.Interfaces
{
    public interface IMediaService
    {
        IEnumerable<EventDto> GetAllEvents();
    }
}
