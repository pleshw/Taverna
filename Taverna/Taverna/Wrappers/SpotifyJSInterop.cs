using Microsoft.JSInterop;

namespace Taverna.Wrappers;

public class SpotifyJSInterop : IDisposable
{
    public SpotifyJSInterop()
    {
    }

    public async Task<IJSObjectReference> CreateSpotifyPlayer( string accessToken , IJSObjectReference spotifyModule )
    {
        return await spotifyModule.InvokeAsync<IJSObjectReference>( "createSpotifyPlayer" , accessToken );
    }

    public async Task SetSpotifyPlayerListeners( IJSObjectReference spotifyPlayer , IJSObjectReference spotifyModule )
    {
        await spotifyModule.InvokeVoidAsync( "setSpotifyPlayerListeners" , spotifyPlayer );
    }

    public async Task ConnectSpotifyPlayer( IJSObjectReference spotifyPlayer , IJSObjectReference spotifyModule )
    {
        await spotifyModule.InvokeVoidAsync( "connectSpotifyPlayer" , spotifyPlayer );
    }

    public async Task<IJSObjectReference?> InitSpotifyPlayer( string accessToken , IJSObjectReference spotifyModule )
    {
        IJSObjectReference? spotifyPlayer = await CreateSpotifyPlayer( accessToken , spotifyModule );
        await SetSpotifyPlayerListeners( spotifyPlayer , spotifyModule );
        await ConnectSpotifyPlayer( spotifyPlayer , spotifyModule );
        return spotifyPlayer;
    }

    public void Dispose()
    {
        GC.SuppressFinalize( this );
    }
}
