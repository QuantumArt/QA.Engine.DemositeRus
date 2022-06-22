using MTS.IR.Interfaces;
using MTS.IR.ViewModels.Helpers;
using QA.DotNetCore.Engine.Abstractions;
using System.Linq;

namespace MTS.IR.ViewModels.Builders
{
    public class MeetingPageViewModelBuilder
    {
        public IMeetingService _meetingService { get; }
        public MeetingPageViewModelBuilder(IMeetingService meetingService)
        {
            this._meetingService = meetingService;
        }

        public MeetingPageViewModel BuildList(IAbstractPage meetingPage, bool includeArchive)
        {
            MeetingPageViewModel pageViewModel = new MeetingPageViewModel()
            {
                Header = meetingPage.Title
            };
            var meets = _meetingService.GetMeetings(includeArchive);
            pageViewModel.Items = meets.Select(m => new MeetingPageItemInListViewModel()
            {
                Id = m.Id,
                MeetingDate = m.MeetingDate,
                IsArchive = m.IsArchive,
                Text = m.Text,
                Title = m.Title,
                 URL = $"{meetingPage.GetUrl()}/details/{m.Id}"
            }).ToList();
            pageViewModel.BreadCrumbs = meetingPage.GetBreadCrumbs();
            return pageViewModel;
        }

        public MeetingDetailsViewModel BuidDetails (IAbstractPage meetingPage, int id)
        {
            var meet = _meetingService.GetMeeting(id);
            var breadCrumbs = meetingPage.GetBreadCrumbs();
            breadCrumbs.Add(new BreadCrumbViewModel()
            {
                Text = "Detail news page"
            });
            return new MeetingDetailsViewModel
            {
                Title = meet.Title,
                Date = meet.MeetingDate,
                Text = meet.Text,
                AllMeetingsUrl = meetingPage.GetUrl(),
                BreadCrumbs = breadCrumbs
            };
        }
    }
}
