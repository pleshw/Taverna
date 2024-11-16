using Microsoft.JSInterop;
using Taverna.Components.Shared.Spotify;

namespace Taverna.Wrappers;

public class SpotifyJSInterop : IAsyncDisposable
{

    public IJSObjectReference? SpotifyPlayer { get; private set; }


    private IJSObjectReference? spotifyModule = null;
    public IJSObjectReference SpotifyModule { get => spotifyModule ?? throw new( "Invalid spotify module" ); }

    public SpotifyJSInterop()
    {

    }

    public async Task<IJSObjectReference> CreateSpotifyPlayer( string accessToken )
    {
        return await SpotifyModule.InvokeAsync<IJSObjectReference>( "createSpotifyPlayer" , accessToken );
    }

    public async Task DisconnectSpotifyPlayer()
    {
        await SpotifyModule.InvokeVoidAsync( "disconnectSpotifyPlayer"  );
    }

    public async Task ConnectSpotifyPlayer( )
    {
        await SpotifyModule.InvokeVoidAsync( "connectSpotifyPlayer"  );
    }

    public async Task<IJSObjectReference?> UpdateStateSpotifyPlayer( string accessToken )
    {
        return await SpotifyModule.InvokeAsync<IJSObjectReference>( "updateStateSpotifyPlayer" , accessToken );
    }

    public async Task<IJSObjectReference?> InitSpotifyPlayer( string accessToken , IJSObjectReference spotifyModule )
    {
        this.spotifyModule = spotifyModule;
        SpotifyPlayer = await CreateSpotifyPlayer( accessToken );
        return SpotifyPlayer;
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize( this );

        if (spotifyModule is not null)
        {
            await spotifyModule.DisposeAsync();
            SpotifyPlayer = null;
            spotifyModule = null;
        }
    }
}
