using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify
{
    public record class SpotifyWebPlaybackTrack
    {
        [JsonPropertyName( "uri" )]
        public string? Uri { get; set; } // Spotify URI

        [JsonPropertyName( "id" )]
        public string? Id { get; set; } // Spotify ID from URI (can be null)

        [JsonPropertyName( "type" )]
        public string? Type { get; set; } // Content type: can be "track", "episode" or "ad"

        [JsonPropertyName( "media_type" )]
        public string? MediaType { get; set; } // Type of file: can be "audio" or "video"

        [JsonPropertyName( "name" )]
        public string? Name { get; set; } // Name of content

        [JsonPropertyName( "is_playable" )]
        public bool? IsPlayable { get; set; } // Flag indicating whether it can be played

        [JsonPropertyName( "album" )]
        public SpotifyWebPlaybackAlbum? Album { get; set; }

        [JsonPropertyName( "artists" )]
        public List<SpotifyWebPlaybackArtist>? Artists { get; set; }
    }

    public record class SpotifyWebPlaybackAlbum
    {
        [JsonPropertyName( "uri" )]
        public string? Uri { get; set; } // Spotify Album URI

        [JsonPropertyName( "name" )]
        public string? Name { get; set; } // Album Name

        [JsonPropertyName( "images" )]
        public List<SpotifyWebPlaybackImage>? Images { get; set; }
    }

    public record class SpotifyWebPlaybackArtist
    {
        [JsonPropertyName( "uri" )]
        public string? Uri { get; set; } // Spotify Artist URI

        [JsonPropertyName( "name" )]
        public string? Name { get; set; } // Artist Name
    }

    public record class SpotifyWebPlaybackImage
    {
        [JsonPropertyName( "url" )]
        public string? Url { get; set; }
    }
}
