using QA.DotNetCore.Engine.QpData;

namespace Demosite.Models.Widgets
{
    public class HtmlWidget : AbstractWidget
    {
        public string HTML => GetDetail("HTML", string.Empty);
    }
}
