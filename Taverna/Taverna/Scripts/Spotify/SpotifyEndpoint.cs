namespace Taverna.Scripts.Spotify;

public class SpotifyEndpoint( string url , HttpMethod method ) : ISpotifyEndpoint
{
    public string URL { get; set; } = url;
    public HttpMethod Method { get; set; } = method;
}
