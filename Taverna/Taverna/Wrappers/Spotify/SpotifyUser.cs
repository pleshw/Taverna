using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

public record SpotifyUser( [property: JsonPropertyName( "country" )] string? Country , [property: JsonPropertyName( "display_name" )] string? DisplayName , [property: JsonPropertyName( "email" )] string? Email , [property: JsonPropertyName( "explicit_content" )] SpotifyExplicitContent? ExplicitContent , [property: JsonPropertyName( "external_urls" )] SpotifyExternalUrls? ExternalURLs , [property: JsonPropertyName( "followers" )] SpotifyFollowers? Followers , [property: JsonPropertyName( "href" )] string? Href , [property: JsonPropertyName( "id" )] string? Id , [property: JsonPropertyName( "images" )] SpotifyImage[]? Images , [property: JsonPropertyName( "product" )] string? Product , [property: JsonPropertyName( "type" )] string? Type , [property: JsonPropertyName( "uri" )] string? URI );
