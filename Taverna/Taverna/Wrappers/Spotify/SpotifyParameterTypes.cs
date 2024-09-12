using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

public static class SpotifyParameterTypes
{
    public record SetPlayerCurrentPlayback( [property: JsonPropertyName( "device_ids" )] string[] DeviceIds , [property: JsonPropertyName( "play" )] bool? Play );
    public record GetResourceWithPagination( [property: JsonPropertyName( "limit" )] int Limit , [property: JsonPropertyName( "offset" )] int Offset );
}
