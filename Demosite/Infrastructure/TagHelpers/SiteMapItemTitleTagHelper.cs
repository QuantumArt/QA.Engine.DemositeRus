using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Demosite.Infrastructure.TagHelpers;

public class SiteMapItemTitleTagHelper : TagHelper
{
	public bool IsPrimary { get; set; }

	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		output.TagName = IsPrimary ? "strong" : string.Empty;

		base.Process(context, output);
	}
}
