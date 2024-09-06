using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

public record SpotifyActions( [property: JsonPropertyName( "interrupting_playback" )] bool? InterruptingPlayback , [property: JsonPropertyName( "pausing" )] bool? Pausing , [property: JsonPropertyName( "resuming" )] bool? Resuming , [property: JsonPropertyName( "seeking" )] bool? Seeking , [property: JsonPropertyName( "skipping_next" )] bool? SkippingNext , [property: JsonPropertyName( "skipping_prev" )] bool? SkippingPrev , [property: JsonPropertyName( "toggling_repeat_context" )] bool? TogglingRepeatContext , [property: JsonPropertyName( "toggling_shuffle" )] bool? TogglingShuffle , [property: JsonPropertyName( "toggling_repeat_track" )] bool? TogglingRepeatTrack , [property: JsonPropertyName( "transferring_playback" )] bool? TransferringPlayback );
