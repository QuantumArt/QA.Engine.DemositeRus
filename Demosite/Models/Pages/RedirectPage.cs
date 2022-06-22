using QA.DotNetCore.Engine.QpData;

namespace Demosite.Models.Pages
{
    public class RedirectPage : AbstractPage
    {
        public string RedirectTo => GetDetail("RedirectTo", string.Empty);
    }
}
