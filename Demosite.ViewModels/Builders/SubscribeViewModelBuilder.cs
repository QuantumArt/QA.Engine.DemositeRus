using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using Demosite.Interfaces.Dto.Request;
using QA.DotNetCore.Engine.Abstractions;
using System.Threading.Tasks;

namespace Demosite.ViewModels.Builders
{
    public class SubscribeViewModelBuilder
    {
        public SubscribeViewModelBuilder(IEmailNotificationService emailNotification)
        {
            this._notificationService = emailNotification;
        }

        private IEmailNotificationService _notificationService { get; }

        public SubscribeViewModel BuildForm(IAbstractItem widget)
        {
            var vm = new SubscribeViewModel { Title = widget.Title };
            return vm;
        }

        public async Task<SubscriptionStatus> AddSubscriber(SubscribeViewModel subscriber)
        {
            var result = await _notificationService.AddSubscriber(Map(subscriber));
            return result;
        }
        private NewsSubscriber Map(SubscribeViewModel subscribe)
        {
            return new NewsSubscriber()
            {
                Activity = subscribe.Activity,
                Company = subscribe.Company,
                Country = subscribe.Country,
                Email = subscribe.Email,
                FirstName = subscribe.FirstName,
                LastName = subscribe.LastName,
                Gender = subscribe.Gender,
                IsActive = false,
                NewsCategory = subscribe.NewsCategory
            };
        }
    }
}
