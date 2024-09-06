namespace Taverna.Scripts.Spotify;

public class SpotifyEndpoint( string uRL , HttpMethod method ) : ISpotifyEndpoint
{
    public string URL { get; set; } = uRL;
    public HttpMethod Method { get; set; } = method;
}
