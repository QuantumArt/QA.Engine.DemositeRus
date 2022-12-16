using QA.DotNetCore.Engine.QpData;

namespace Demosite.Models.Pages
{
    public class TextPage : AbstractPage
    {
        public string Text => GetDetail("Text", string.Empty);

        public bool HideTitle => GetDetail("HideTitle", false);
    }
}
