using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Demosite.Infrastructure.TagHelpers;

/// <summary>
/// <see cref="ITagHelper"/> implementation targeting &lt;use&gt; elements that supports file versioning.
/// </summary>
[HtmlTargetElement(Attributes = HrefAttributeName, TagStructure = TagStructure.WithoutEndTag)]
[HtmlTargetElement(Attributes = LegacyHrefAttributeName, TagStructure = TagStructure.WithoutEndTag)]
public class UseTagHelper : UrlResolutionTagHelper
{
	private const string HrefAttributeName = "href";
	private const string LegacyHrefAttributeName = "xlink:href";

	private readonly IFileVersionProvider _fileVersionProvider;

	/// <summary>
	/// Source of the image.
	/// </summary>
	/// <remarks>
	/// Passed through to the generated HTML in all cases.
	/// </remarks>
	[HtmlAttributeName(HrefAttributeName)]
	public string? Href { get; set; }

	/// <summary>
	/// Source of the image from deprecated attribute.
	/// </summary>
	/// <remarks>
	/// Passed through to the generated HTML in all cases.
	/// </remarks>
	[HtmlAttributeName(LegacyHrefAttributeName)]
	public string? LegacyHref { get; set; }

	/// <summary>
	/// Creates a new <see cref="UseTagHelper"/>.
	/// </summary>
	/// <param name="fileVersionProvider">The <see cref="IFileVersionProvider"/>.</param>
	/// <param name="urlHelperFactory">The <see cref="IUrlHelperFactory"/>.</param>
	/// <param name="htmlEncoder">The <see cref="HtmlEncoder"/>.</param>
	public UseTagHelper(
		IFileVersionProvider fileVersionProvider,
		IUrlHelperFactory urlHelperFactory,
		HtmlEncoder htmlEncoder)
		: base(urlHelperFactory, htmlEncoder)
	{
		_fileVersionProvider = fileVersionProvider;
	}

	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		FixupHrefFromPreceedingTagHelpersResults(output);
		MigrateFromDeprecatedAttribute();

		SetPathAttribute(HrefAttributeName, Href);
		SetPathAttribute(LegacyHrefAttributeName, LegacyHref);

		void SetPathAttribute(string name, string? path)
		{
			if (path is null)
			{
				return;
			}

			var pathBase = ViewContext.HttpContext.Request.PathBase;
			string pathWithFileVersion = _fileVersionProvider.AddFileVersionToPath(pathBase, path);

			output.Attributes.SetAttribute(name, pathWithFileVersion);
		}

		ProcessUrlAttribute(HrefAttributeName, output);
		ProcessUrlAttribute(LegacyHrefAttributeName, output);
	}

	private void FixupHrefFromPreceedingTagHelpersResults(TagHelperOutput output)
	{
		if (output.Attributes[HrefAttributeName]?.Value is string href)
		{
			Href = href;
		}

		if (output.Attributes[LegacyHrefAttributeName]?.Value is string legacyHref)
		{
			LegacyHref = legacyHref;
		}
	}

	private void MigrateFromDeprecatedAttribute()
	{
		Href ??= LegacyHref;
	}
}
