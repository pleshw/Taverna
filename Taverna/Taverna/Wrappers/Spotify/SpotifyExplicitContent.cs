using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

public record SpotifyExplicitContent( [property: JsonPropertyName( "filter_enabled" )] bool Enabled , [property: JsonPropertyName( "filter_locked" )] bool Locked );
