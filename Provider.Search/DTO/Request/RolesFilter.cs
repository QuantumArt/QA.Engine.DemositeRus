using System.Text.Json.Serialization;

namespace Provider.Search.DTO.Request;

public class RolesFilter
{
	[JsonPropertyName("roles")]
	public string[]? Roles { get; }

	public RolesFilter(string[]? roles)
	{
		Roles = roles;
	}
}
