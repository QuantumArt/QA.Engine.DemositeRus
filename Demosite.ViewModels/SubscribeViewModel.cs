using Demosite.ViewModels.Interface;

namespace Demosite.ViewModels
{
    public class SubscribeViewModel : ICaptchaModel
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public char Gender { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Activity { get; set; }
        public int[] NewsCategory { get; set; }
        public string CaptchaKey { get; set; }
        public string TokenCaptcha { get; set; }
    }
}
