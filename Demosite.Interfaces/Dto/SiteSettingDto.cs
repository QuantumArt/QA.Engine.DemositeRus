namespace Demosite.Interfaces.Dto;

public class SiteSettingDto
{
	public SiteSettingDto(string key, SettingType type, string stringValue, string imageValueUrl)
	{
		Key = key;
		Type = type;
		StringValue = stringValue;
		ImageValueUrl = imageValueUrl;
	}

	public string Key { get; }
	public SettingType Type { get; }
	public string StringValue { get; }
	public string ImageValueUrl { get; }
}

public enum SettingType
{
    Unknown,
    String,
    Image
}

