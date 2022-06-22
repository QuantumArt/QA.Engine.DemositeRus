namespace Demosite.ViewModels.Interface
{
    public interface ICaptchaModel
    {
        public string CaptchaKey { get; set; }
        public string TokenCaptcha { get; set; }
    }
}
