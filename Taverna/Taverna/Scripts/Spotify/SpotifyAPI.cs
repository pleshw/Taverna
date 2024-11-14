using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Taverna.Wrappers.Spotify;
using static Taverna.Wrappers.Spotify.SpotifyParameterTypes;

namespace Taverna.Scripts.Spotify;

public static class SpotifyAPI
{
    private readonly static JsonSerializerOptions _JSONParseOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public static async Task<bool> CurrentlyPlayingOnDevice(this HttpClient client, string accessToken, string deviceName)
    {
        SpotifyPlaybackState? currentState = await client.GetPlayerPlaybackState(accessToken);
        return currentState != null && currentState?.Device?.Name == deviceName && currentState.IsPlaying();
    }

    public static async Task<SpotifyUser?> GetSpotifyCurrentUser(this HttpClient client, string accessToken)
    {
        return await Request<SpotifyUser>(
                accessToken,
                SpotifyRequestURI.GetCurrentUser,
                client);
    }

    public static async Task<SpotifyPlaybackState?> GetPlayerPlaybackState( this HttpClient client , string accessToken )
    {
        return await Request<SpotifyPlaybackState>(
                accessToken ,
                SpotifyRequestURI.GetPlayerPlaybackState ,
                client );
    }

    public static async Task<SpotifyUserQueue?> GetCurrentUserQueue( this HttpClient client, string accessToken)
    {
        return await Request<SpotifyUserQueue>(
                accessToken,
                SpotifyRequestURI.GetCurrentUserQueue,
                client);
    }

    public static async Task<SpotifyGenericPlayerResponse?> GetCurrentUserCurrentlyPlaying( this HttpClient client , string accessToken )
    {
        return await Request<SpotifyGenericPlayerResponse>(
                accessToken ,
                SpotifyRequestURI.GetCurrentUserCurrentlyPlaying ,
                client );
    }

    public static async Task<SpotifyUserPlaylistsQuery?> GetUserPlaylists( this HttpClient client , string accessToken, string userId , int limit=40, int offset = 0)
    {
        return await Request<SpotifyUserPlaylistsQuery?>(
                accessToken ,
                SpotifyRequestURI.GetUserPlaylists.GetEndpointReplaceUserId( userId ) ,
                client,
                Parse(new GetResourceWithPagination( limit , offset ) ) );
    }

    public static async Task<SpotifyUserPlaylistsQuery?> GetCurrentUserPlaylists( this HttpClient client , string accessToken , int limit = 40 , int offset = 0 )
    {
        SpotifyUser? user = await client.GetSpotifyCurrentUser(accessToken);

        if (user == null || user.Id == null)
        {
            return null;
        }

        return await client.GetUserPlaylists(accessToken, user.Id, limit , offset);
    }

    public static async Task<SpotifyDevice?> GetUserDevices( this HttpClient client , string accessToken )
    {
        return await Request<SpotifyDevice>(
                accessToken ,
                SpotifyRequestURI.GetDevices,
                client );
    }

    public static async Task<SpotifyTrack?> GetSpotifyTrack(this HttpClient client, string accessToken, string trackId)
    {
        if (string.IsNullOrEmpty(trackId))
        {
            return null;
        }

        return await Request<SpotifyTrack>(
                accessToken ,
                SpotifyRequestURI.GetTrack.GetEndpointReplaceTrackId( trackId ) ,
                client
                );
    }

    public static async Task<SpotifyPlaylist?> GetSpotifyPlaylist( this HttpClient client , string accessToken , string playlistId )
    {
        if (string.IsNullOrEmpty( playlistId ))
        {
            return null;
        }

        return await Request<SpotifyPlaylist>(
                accessToken ,
                SpotifyRequestURI.GetPlaylist.GetEndpointReplacePlaylistId( playlistId ) ,
                client
                );
    }

    public static async Task<string?> SetPlayerCurrentPlayback(this HttpClient client, string accessToken, string deviceId)
    {
        return await GetRequestStringAsync(
                accessToken,
                SpotifyRequestURI.TransferPlayback,
                client,
                Parse(new SetPlayerCurrentPlayback( [deviceId] , false))
                );
    }

    public static async Task<string?> SetPlayerCurrentPlaybackAndPlay(this HttpClient client, string accessToken, string deviceId)
    {
        return await GetRequestStringAsync(
                accessToken,
                SpotifyRequestURI.TransferPlayback,
                client,
                Parse(new SetPlayerCurrentPlayback( [deviceId], true ))
                );
    }

    public static async Task<T?> Request<T>(string accessToken, SpotifyEndpoint endpoint, HttpClient? backchannelClient, JsonDocument? parameters = null)
    {
        string? response = await GetRequestStringAsync(accessToken, endpoint, backchannelClient, parameters);

        return response == null
            ? default
            : JsonSerializer.Deserialize<T>(response);
    }

    public static async Task<string?> GetRequestStringAsync(string accessToken, SpotifyEndpoint endpoint, HttpClient? backchannelClient, JsonDocument? parameters = null)
    {
        HttpRequestMessage request = new(endpoint.Method, endpoint.URL);
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("Authorization", $"Bearer {accessToken}");

        if (parameters != null)
        {
            SetQueryParameters(request, endpoint.Method, parameters);
        }

        HttpClient client = backchannelClient ?? new HttpClient();
        HttpResponseMessage response = await client.SendAsync(request);
        string spotifyRequestResult = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(spotifyRequestResult))
        {
            return spotifyRequestResult;
        }

        return null;
    }

    public static async Task<SpotifyRequestTokenReponse?> RequestAccessTokenAsync()
    {
        return await RequestTokenAsync(new HttpClient());
    }

    public static async Task<SpotifyRequestTokenReponse?> RequestTokenAsync( HttpClient httpClient )
    {
        Dictionary<string , string> parameters = new()
        {
            {"grant_type", "client_credentials" },
            {"client_id", "2a581139f71642bebb60f0a2f2540137" },
            {"client_secret", "6340635eb58b442b9013e2e15aef1492" }
        };

        HttpRequestMessage request = new( SpotifyRequestURI.RequestAccessToken.Method , SpotifyRequestURI.RequestAccessToken.URL );
        request.Headers.Add( "Accept" , "application/json" );
        request.Content = new FormUrlEncodedContent( parameters );

        HttpResponseMessage response = await httpClient.SendAsync( request );
        string spotifyRequestResult = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode && !string.IsNullOrEmpty( spotifyRequestResult ))
        {
            return JsonSerializer.Deserialize<SpotifyRequestTokenReponse>( spotifyRequestResult );
        }

        return null;
    }

    /// <summary>
    /// Adds parameters from json parameters to a web query to proper format, being GET or SET
    /// </summary>
    /// <param name="request"></param>
    /// <param name="httpMethod"></param>
    /// <param name="parameters"></param>
    public static void SetQueryParameters(HttpRequestMessage request, HttpMethod httpMethod, JsonDocument parameters)
    {
        if (httpMethod == HttpMethod.Get)
        {
            // Append parameters to the query string.
            StringBuilder? queryString = new();
            foreach (JsonProperty property in parameters.RootElement.EnumerateObject())
            {
                switch (property.Value.ValueKind)
                {
                    case JsonValueKind.Undefined: throw new NotImplementedException(); 
                    case JsonValueKind.Object: throw new NotImplementedException(); 
                    case JsonValueKind.Array: throw new NotImplementedException(); 
                    case JsonValueKind.String: queryString.Append( $"{property.Name}={property.Value.GetString()}&" ); break;
                    case JsonValueKind.Number: queryString.Append( $"{property.Name}={property.Value.GetInt32().ToString()}&" ); break;
                    case JsonValueKind.True: throw new NotImplementedException();
                    case JsonValueKind.False: throw new NotImplementedException();
                    case JsonValueKind.Null: throw new NotImplementedException();
                }
            }
            // Remove the trailing '&' character.
            if (queryString.Length > 0)
            {
                queryString.Length--;
            }
            request.RequestUri = new Uri($"{request.RequestUri}?{queryString}");
        }
        else
        {
            // Assuming POST or other HTTP methods where you can include parameters in the request body.
            string? parameterJson = parameters.RootElement.ToString();
            request.Content = new StringContent(parameterJson, Encoding.UTF8, "application/json");
        }
    }

    /// <summary>
    /// Makes an object of any class A JsonLikeStructure
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static JsonDocument Parse(object item)
    {
        return JsonDocument.Parse(JsonSerializer.Serialize(item, _JSONParseOptions));
    }
}
