using Demosite.Interfaces.Dto;
using System;

// ReSharper disable once CheckNamespace
namespace Demosite.Postgre.DAL;

public partial class SiteSetting
{
	public SettingType TypeExact =>
		Enum.TryParse(Type, true, out SettingType exact)
			? exact
			: default;
}
