using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

public static class SpotifyResponseTypes
{
    public record SetPlayerCurrentPlayback( [property: JsonPropertyName( "device_ids" )] string[] DeviceIds , [property: JsonPropertyName( "play" )] bool? Play );
}
