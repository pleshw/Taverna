namespace Taverna.Scripts.Spotify;

public static class SpotifyRequestURI
{
    public static readonly SpotifyEndpoint RequestAccessToken = new SpotifyEndpoint( "https://accounts.spotify.com/api/token" , HttpMethod.Post );
    public static readonly SpotifyEndpoint GetCurrentUser = new("https://api.spotify.com/v1/me", HttpMethod.Get);
    public static readonly SpotifyEndpoint GetPlayerPlaybackState = new("https://api.spotify.com/v1/me/player", HttpMethod.Get);
    public static readonly SpotifyEndpoint GetCurrentUserQueue = new( "https://api.spotify.com/v1/me/player/queue" , HttpMethod.Get );
    public static readonly SpotifyEndpoint GetCurrentUserCurrentlyPlaying = new( "https://api.spotify.com/v1/me/player/currently-playing" , HttpMethod.Get );
    public static readonly SpotifyEndpoint TransferPlayback = new ("https://api.spotify.com/v1/me/player", HttpMethod.Put);
    public static readonly SpotifyEndpoint GetDevices = new( "https://api.spotify.com/v1/me/player/devices" , HttpMethod.Get );

    public static readonly SpotifyEndpoint GetUserPlaylists = new ( "https://api.spotify.com/v1/users/{user_id}/playlists" , HttpMethod.Get );
    public static readonly SpotifyEndpoint GetTrack = new( "https://api.spotify.com/v1/tracks/{track_id}" , HttpMethod.Get );
    public static readonly SpotifyEndpoint GetPlaylist = new( "https://api.spotify.com/v1/playlists/{playlist_id}" , HttpMethod.Get );
}
