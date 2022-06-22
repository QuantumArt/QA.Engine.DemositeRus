using Demosite.Interfaces.Dto;
using Demosite.Interfaces.Dto.Request;
using System.Collections.Generic;

namespace Demosite.Interfaces
{
    public interface IMediaService
    {
        IEnumerable<EventDto> GetAllEvents();
    }
}
