using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

/// <summary>
/// Does not include display name
/// </summary>
/// <param name="ExternalUrls"></param>
/// <param name="Followers"></param>
/// <param name="ProfileURL"></param>
/// <param name="Id"></param>
/// <param name="Type"></param>
/// <param name="ProfileUri"></param>
public record SpotifyPlaylistUserSimplified( [property: JsonPropertyName( "external_urls" )] SpotifyExternalUrls? ExternalUrls , [property: JsonPropertyName( "followers" )] SpotifyFollowers? Followers , [property: JsonPropertyName( "href" )] string? ProfileURL , [property: JsonPropertyName( "id" )] string? Id , [property: JsonPropertyName( "type" )] string? Type , [property: JsonPropertyName( "uri" )] string? ProfileUri  );