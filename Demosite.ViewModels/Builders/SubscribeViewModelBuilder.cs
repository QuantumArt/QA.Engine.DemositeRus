using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using Demosite.Interfaces.Dto.Request;
using QA.DotNetCore.Engine.Abstractions;
using System.Threading.Tasks;

namespace Demosite.ViewModels.Builders
{
    public class SubscribeViewModelBuilder
    {
        private readonly IEmailNotificationService _notificationService;
        public SubscribeViewModelBuilder(IEmailNotificationService emailNotification)
        {
            _notificationService = emailNotification;
        }

        public SubscribeViewModel BuildForm(IAbstractItem widget)
        {
            SubscribeViewModel viewModel = new() { Title = widget.Title };
            return viewModel;
        }

        public async Task<SubscriptionStatus> AddSubscriber(SubscribeViewModel subscriber)
        {
            SubscriptionStatus result = await _notificationService.AddSubscriber(Map(subscriber));
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
