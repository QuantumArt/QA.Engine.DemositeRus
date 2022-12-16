using Demosite.Interfaces.Dto;
using System.Collections.Generic;

namespace Demosite.Interfaces
{
    public interface IMediaService
    {
        IEnumerable<EventDto> GetAllEvents();
    }
}
