using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Security.Claims;
using Taverna.Scripts.Spotify;
using Taverna.Wrappers.Spotify;

namespace Taverna.Tests;

[TestClass]
public class UnitTestSpotifyAPI
{
    public readonly string? _accessToken;
    public readonly HttpClient _httpClient = new();

    public UnitTestSpotifyAPI()
    {
        _accessToken = SpotifyAPI.RequestTokenAsync( _httpClient ).GetAwaiter().GetResult()?.AccessToken;
    }

    [TestMethod]
    [DataRow( "266WEiJhZI1dNUcOqr7Yzi" )]
    [DataRow( "3B5UbSndRz907IZhhmUfLi" )]
    [DataRow( "3oQaOjaIYPsnJbGNzXcIID" )]
    [DataRow( "3O8G8eVrhfXTGttyQ1xVuq" )]
    public async Task Test_Get_Spotify_Track( string value )
    {
        Assert.IsNotNull( _accessToken , "Access token should not be null. Failed to authenticate." );

        // Proceed with the API call only if the access token is valid
        SpotifyTrack? result = await _httpClient.GetSpotifyTrack( _accessToken , value );

        // You can add additional assertions to check the validity of the result
        Assert.IsNotNull( result , "The response should not be null." );
    }
}