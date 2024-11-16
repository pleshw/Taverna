using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

public record SpotifyAvailableDevices( [property: JsonPropertyName( "devices" )] SpotifyDevice[]? Devices );
