using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

public record SpotifySimplifiedPlaylist( [property: JsonPropertyName( "collaborative" )] bool? IsCollaborative, [property: JsonPropertyName( "description" )] string? Description, [property: JsonPropertyName( "external_urls" )] SpotifyExternalUrls? ExternalUrls, [property: JsonPropertyName( "href" )] string? Href, [property: JsonPropertyName( "id" )] string? Id , [property: JsonPropertyName( "images" )] List<SpotifyImage>? Images, [property: JsonPropertyName( "name" )] string? Name, [property: JsonPropertyName( "owner" )] SpotifyPlaylistUser? Owner , [property: JsonPropertyName( "public" )] bool? IsPublic, [property: JsonPropertyName( "snapshot_id" )] string? SnapshotId , [property: JsonPropertyName( "tracks" )] SpotifyPlaylistTrackCount? TrackCount, [property: JsonPropertyName( "type" )] string? Type , [property: JsonPropertyName( "uri" )] string? Uri);
