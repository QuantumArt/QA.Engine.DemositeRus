using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace Demosite.Infrastructure.TagHelpers;

/// <summary>
/// Form tag helper that can process controller names with `Controller` suffix.
/// </summary>
/// <remarks>
/// Usage:
/// <code>
///	&lt;form asp-action="@nameof(<see cref="AccountController"/>.Login)" asp-controller="@nameof(<see cref="AccountController"/>)"&gt;
///	  ...
///	&lt;/form&gt;
/// </code>
/// </remarks>
[HtmlTargetElement("form")]
public class FixedFormTagHelper : FormTagHelper
{
	[HtmlAttributeName("asp-controller")]
	public new string Controller
	{
		get => base.Controller;
		set => base.Controller = FixupControllerName(value);
	}

	public FixedFormTagHelper(IHtmlGenerator generator) : base(generator)
	{
	}

	private static string FixupControllerName(string name)
	{
		const string ControllerPostfix = "Controller";

		if (name.EndsWith(ControllerPostfix, StringComparison.OrdinalIgnoreCase))
		{
			return name[..^ControllerPostfix.Length];
		}

		return name;
	}
}
