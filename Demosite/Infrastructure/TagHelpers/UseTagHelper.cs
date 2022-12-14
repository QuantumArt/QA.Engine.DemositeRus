using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace Demosite.Infrastructure.TagHelpers;

/// <summary>
/// <see cref="ITagHelper"/> implementation targeting &lt;use&gt; elements that supports file versioning.
/// </summary>
[HtmlTargetElement(Attributes = HREF_ATTRIBUTE_NAME, TagStructure = TagStructure.WithoutEndTag)]
[HtmlTargetElement(Attributes = LEGACY_HREF_ATTRIBUTE_NAME, TagStructure = TagStructure.WithoutEndTag)]
public class UseTagHelper : UrlResolutionTagHelper
{
    private const string HREF_ATTRIBUTE_NAME = "href";
    private const string LEGACY_HREF_ATTRIBUTE_NAME = "xlink:href";

    private readonly IFileVersionProvider _fileVersionProvider;

    /// <summary>
    /// Source of the image.
    /// </summary>
    /// <remarks>
    /// Passed through to the generated HTML in all cases.
    /// </remarks>
    [HtmlAttributeName(HREF_ATTRIBUTE_NAME)]
    public string? Href { get; set; }

    /// <summary>
    /// Source of the image from deprecated attribute.
    /// </summary>
    /// <remarks>
    /// Passed through to the generated HTML in all cases.
    /// </remarks>
    [HtmlAttributeName(LEGACY_HREF_ATTRIBUTE_NAME)]
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

        SetPathAttribute(HREF_ATTRIBUTE_NAME, Href);
        SetPathAttribute(LEGACY_HREF_ATTRIBUTE_NAME, LegacyHref);

        void SetPathAttribute(string name, string? path)
        {
            if (path is null)
            {
                return;
            }

            Microsoft.AspNetCore.Http.PathString pathBase = ViewContext.HttpContext.Request.PathBase;
            string pathWithFileVersion = _fileVersionProvider.AddFileVersionToPath(pathBase, path);

            output.Attributes.SetAttribute(name, pathWithFileVersion);
        }
        ProcessUrlAttribute(HREF_ATTRIBUTE_NAME, output);
        ProcessUrlAttribute(LEGACY_HREF_ATTRIBUTE_NAME, output);
    }

    private void FixupHrefFromPreceedingTagHelpersResults(TagHelperOutput output)
    {
        if (output.Attributes[HREF_ATTRIBUTE_NAME]?.Value is string href)
        {
            Href = href;
        }

        if (output.Attributes[LEGACY_HREF_ATTRIBUTE_NAME]?.Value is string legacyHref)
        {
            LegacyHref = legacyHref;
        }
    }

    private void MigrateFromDeprecatedAttribute()
    {
        Href ??= LegacyHref;
    }
}
