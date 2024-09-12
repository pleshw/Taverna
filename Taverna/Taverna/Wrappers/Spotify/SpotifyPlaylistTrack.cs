using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

public record SpotifyPlaylistTrack( [property: JsonPropertyName( "added_at" )] DateTime? AddedAt, [property: JsonPropertyName( "added_by" )] SpotifyPlaylistUserSimplified? AddedBy , [property: JsonPropertyName( "is_local" )] bool? IsLocalFile , [property: JsonPropertyName( "track" )] SpotifyTrack? Track);