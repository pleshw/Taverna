using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

public record SpotifyDevice( [property: JsonPropertyName( "id" )] string? Id , [property: JsonPropertyName( "is_active" )] bool? IsActive , [property: JsonPropertyName( "is_private_session" )] bool? IsPrivateSession , [property: JsonPropertyName( "is_restricted" )] bool? IsRestricted , [property: JsonPropertyName( "name" )] string? Name , [property: JsonPropertyName( "type" )] string? Type , [property: JsonPropertyName( "volume_percent" )] int? VolumePercent , [property: JsonPropertyName( "supports_volume" )] bool? SupportsVolume );
