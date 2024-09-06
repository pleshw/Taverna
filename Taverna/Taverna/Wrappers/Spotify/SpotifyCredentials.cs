namespace Taverna.Wrappers.Spotify;

public class SpotifyCredentials( string id , string secret )
{
    public string ClientId { get; set; } = id;
    public string ClientSecret { get; set; } = secret;

    public SpotifyCredentials( IConfiguration configuration ) :
        this( configuration["Services:Authentication:Spotify:ClientId"]! , configuration["Services:Authentication:Spotify:ClientSecret"]! )
    {
    }
}
