using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

public record SpotifyResponse( [property: JsonPropertyName( "device" )] SpotifyDevice? Device , [property: JsonPropertyName( "repeat_state" )] string? RepeatState , [property: JsonPropertyName( "shuffle_state" )] bool? ShuffleState , [property: JsonPropertyName( "context" )] SpotifyContext? Context , [property: JsonPropertyName( "timestamp" )] long? Timestamp , [property: JsonPropertyName( "progress_ms" )] long? ProgressMs , [property: JsonPropertyName( "is_playing" )] bool? IsPlaying , [property: JsonPropertyName( "item" )] SpotifyItem? Item , [property: JsonPropertyName( "currently_playing_type" )] string? CurrentlyPlayingType , [property: JsonPropertyName( "actions" )] SpotifyActions? Actions );
