using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

public record SpotifyExternalUrls( [property: JsonPropertyName( "spotify" )] string? Spotify );
