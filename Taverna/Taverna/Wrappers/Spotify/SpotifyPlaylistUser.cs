using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

public record SpotifyPlaylistUser( [property: JsonPropertyName( "external_urls" )] SpotifyExternalUrls? ExternalUrls , [property: JsonPropertyName( "followers" )] SpotifyFollowers? Followers, [property: JsonPropertyName( "href" )] string? ProfileURL, [property: JsonPropertyName( "id" )] string? Id, [property: JsonPropertyName( "type" )] string? Type, [property: JsonPropertyName( "uri" )] string? ProfileUri, [property: JsonPropertyName( "display_name" )] string? DisplayName);
