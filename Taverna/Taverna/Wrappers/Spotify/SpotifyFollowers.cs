using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

public record SpotifyFollowers( [property: JsonPropertyName( "href" )] string? Href , [property: JsonPropertyName( "total" )] int? Total );
