using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

public record SpotifyRestrictions( [property: JsonPropertyName( "reason" )] string? Reason );
