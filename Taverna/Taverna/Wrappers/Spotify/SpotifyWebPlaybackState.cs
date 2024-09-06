using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify
{
    public record class SpotifyWebPlaybackState
    {
        [JsonPropertyName( "context" )]
        public SpotifyWebPlaybackContext? Context { get; set; }

        [JsonPropertyName( "disallows" )]
        public SpotifyWebPlaybackDisallows? Disallows { get; set; }

        [JsonPropertyName( "paused" )]
        public bool? Paused { get; set; }

        [JsonPropertyName( "position" )]
        public int? Position { get; set; }

        [JsonPropertyName( "repeat_mode" )]
        public int? RepeatMode { get; set; }

        [JsonPropertyName( "shuffle" )]
        public bool? Shuffle { get; set; }

        [JsonPropertyName( "track_window" )]
        public SpotifyWebPlaybackTrackWindow? TrackWindow { get; set; }
    }

    public record class SpotifyWebPlaybackContext
    {
        [JsonPropertyName( "uri" )]
        public string? Uri { get; set; }

        [JsonPropertyName( "metadata" )]
        public Dictionary<string , object>? Metadata { get; set; }
    }

    public record class SpotifyWebPlaybackDisallows
    {
        [JsonPropertyName( "pausing" )]
        public bool? Pausing { get; set; }

        [JsonPropertyName( "peeking_next" )]
        public bool? PeekingNext { get; set; }

        [JsonPropertyName( "peeking_prev" )]
        public bool? PeekingPrev { get; set; }

        [JsonPropertyName( "resuming" )]
        public bool? Resuming { get; set; }

        [JsonPropertyName( "seeking" )]
        public bool? Seeking { get; set; }

        [JsonPropertyName( "skipping_next" )]
        public bool? SkippingNext { get; set; }

        [JsonPropertyName( "skipping_prev" )]
        public bool? SkippingPrev { get; set; }
    }

    public record class SpotifyWebPlaybackTrackWindow
    {
        [JsonPropertyName( "current_track" )]
        public SpotifyWebPlaybackTrack? CurrentTrack { get; set; }

        [JsonPropertyName( "previous_tracks" )]
        public List<SpotifyWebPlaybackTrack>? PreviousTracks { get; set; }

        [JsonPropertyName( "next_tracks" )]
        public List<SpotifyWebPlaybackTrack>? NextTracks { get; set; }
    }
}
