using Taverna.Wrappers.Spotify;

namespace Taverna.Scripts.Spotify;

public class SpotifyCredentialsProvider
{
    private static SpotifyCredentials? _instance;
    public static SpotifyCredentials? Instance { get => _instance; }

    public SpotifyCredentialsProvider(string id, string secret)
    {
        _instance = new SpotifyCredentials(id, secret);
    }

    public SpotifyCredentialsProvider(IConfiguration configuration)
    {
        _instance = new SpotifyCredentials(configuration);
    }

    public static string ClientId
    {
        get
        {
            if (Instance == null)
            {
                throw new InvalidOperationException("An _instance of Spotify Credentials have to be set on app initialization(program.cs).");
            }

            return Instance.ClientId;
        }
    }

    public static string ClientSecret
    {
        get
        {
            if (Instance == null)
            {
                throw new InvalidOperationException("An _instance of Spotify Credentials have to be set on app initialization(program.cs).");
            }

            return Instance.ClientSecret;
        }
    }
}
