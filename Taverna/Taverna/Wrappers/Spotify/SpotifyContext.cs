using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

public record SpotifyContext( [property: JsonPropertyName( "type" )] string? Type , [property: JsonPropertyName( "href" )] string? Href , [property: JsonPropertyName( "external_urls" )] SpotifyExternalUrls? ExternalUrls , [property: JsonPropertyName( "uri" )] string? Uri );
