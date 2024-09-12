namespace Taverna.Scripts.Spotify;

public class SpotifyEndpoint( string url , HttpMethod method ) : ISpotifyEndpoint
{
    public string URL { get; set; } = url;
    public HttpMethod Method { get; set; } = method;


    public SpotifyEndpoint GetEndpointReplaceUserId (string userId)
    { 
        return new SpotifyEndpoint( URL.Replace( "{user_id}" , userId ) , Method) ;
    }

    public SpotifyEndpoint GetEndpointReplaceTrackId( string trackId )
    {
        return new SpotifyEndpoint( URL.Replace( "{track_id}" , trackId ) , Method );
    }


    public SpotifyEndpoint GetEndpointReplacePlaylistId( string playlistId )
    {
        return new SpotifyEndpoint( URL.Replace( "{playlist_id}" , playlistId ) , Method );
    }
}
