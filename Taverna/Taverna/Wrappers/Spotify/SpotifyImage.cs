using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

public record SpotifyImage( [property: JsonPropertyName( "url" )] string? URL , [property: JsonPropertyName( "width" )] int? Width , [property: JsonPropertyName( "height" )] int? Height );
