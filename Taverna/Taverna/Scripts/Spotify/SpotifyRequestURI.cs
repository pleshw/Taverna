namespace Taverna.Scripts.Spotify;

public static class SpotifyRequestURI
{
    public static readonly SpotifyEndpoint GetCurrentUser = new("https://api.spotify.com/v1/me", HttpMethod.Get);
    public static readonly SpotifyEndpoint GetPlayerPlaybackState = new("https://api.spotify.com/v1/me/player", HttpMethod.Get);

    public static readonly SpotifyEndpoint TransferPlayback = new("https://api.spotify.com/v1/me/player", HttpMethod.Put);
}
