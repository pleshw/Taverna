namespace Taverna.Wrappers.Spotify;

public static class SpotifyPlaybackStateUtils
{
    public static bool IsActive( this SpotifyPlaybackState spotifyPlaybackState )
    {
        return spotifyPlaybackState.Device != null && spotifyPlaybackState.Device.IsActive != null && spotifyPlaybackState.Device.IsActive.Value;
    }

    public static bool IsPlaying( this SpotifyPlaybackState spotifyPlaybackState )
    {
        return spotifyPlaybackState.IsActive() && spotifyPlaybackState.IsPlaying != null && spotifyPlaybackState.IsPlaying.Value;
    }

    public static bool IsConnectedToDevice( this SpotifyPlaybackState spotifyPlaybackState , string deviceIdToCheck )
    {
        return spotifyPlaybackState.IsActive() && spotifyPlaybackState.Device!.Id == deviceIdToCheck;
    }
}
