using QA.DotNetCore.Engine.QpData;

namespace MTS.IR.Models.Pages
{
    public class RedirectPage : AbstractPage
    {
        public string RedirectTo => GetDetail("RedirectTo", string.Empty);
    }
}
