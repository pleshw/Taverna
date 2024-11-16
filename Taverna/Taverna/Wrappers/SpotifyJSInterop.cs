using Microsoft.JSInterop;
using Taverna.Components.Shared.Spotify;

namespace Taverna.Wrappers;

public class SpotifyJSInterop : IAsyncDisposable
{
    private readonly IJSRuntime _jsRuntime;

    public IJSObjectReference? SpotifyPlayer { get; private set; }


    private IJSObjectReference? spotifyModule = null;
    public IJSObjectReference SpotifyModule { get => spotifyModule ?? throw new( "Invalid spotify module" ); }

    public SpotifyJSInterop( IJSRuntime jsRuntime )
    {
        _jsRuntime = jsRuntime;
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

    public async Task<IJSObjectReference?> InitSpotifyPlayer( string accessToken )
    {
        spotifyModule ??= await _jsRuntime.InvokeAsync<IJSObjectReference>( "import" , "./Components/Shared/Spotify/SpotifyWrapper.razor.js" );
        SpotifyPlayer ??= await CreateSpotifyPlayer( accessToken );
        return SpotifyPlayer;
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize( this );

        if(spotifyModule is not null)
        {
            await spotifyModule.DisposeAsync();
        }
    }
}
