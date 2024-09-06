namespace Taverna.Scripts.Spotify;

public interface ISpotifyEndpoint
{
    public string URL { get; set; }
    public HttpMethod Method { get; set; }
}
