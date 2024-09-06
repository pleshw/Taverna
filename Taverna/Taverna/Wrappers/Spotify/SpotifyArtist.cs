using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

public record SpotifyArtist( [property: JsonPropertyName( "external_urls" )] SpotifyExternalUrls? ExternalUrls , [property: JsonPropertyName( "href" )] string? Href , [property: JsonPropertyName( "id" )] string? Id , [property: JsonPropertyName( "name" )] string? Name , [property: JsonPropertyName( "type" )] string? Type , [property: JsonPropertyName( "uri" )] string? Uri );
