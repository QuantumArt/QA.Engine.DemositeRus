using QA.DotNetCore.Engine.QpData;

namespace MTS.IR.Models.Widgets
{
    public class HtmlWidget : AbstractWidget
    {
        public string HTML => GetDetail("HTML", string.Empty);
    }
}
