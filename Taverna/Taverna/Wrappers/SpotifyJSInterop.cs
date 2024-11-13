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
        if (SpotifyModule is null)
        {
            throw new( nameof( SpotifyModule ) );
        }

        return await SpotifyModule.InvokeAsync<IJSObjectReference>( "createSpotifyPlayer" , accessToken );
    }

    public async Task SetSpotifyPlayerListeners( )
    {
        if (SpotifyModule is null || SpotifyPlayer is null)
        {
            throw new( nameof( SpotifyModule ) );
        }

        await SpotifyModule.InvokeVoidAsync( "setSpotifyPlayerListeners" , SpotifyPlayer );
    }

    public async Task DisconnectSpotifyPlayer()
    {
        if (SpotifyModule is null || SpotifyPlayer is null)
        {
            throw new( nameof( SpotifyModule ) );
        }

        await SpotifyModule.InvokeVoidAsync( "disconnectSpotifyPlayer" , SpotifyPlayer );
    }

    public async Task ConnectSpotifyPlayer( )
    {
        if (SpotifyModule is null || SpotifyPlayer is null)
        {
            throw new( nameof( SpotifyModule ) );
        }

        await SpotifyModule.InvokeVoidAsync( "connectSpotifyPlayer" , SpotifyPlayer );
    }

    public async Task<IJSObjectReference?> UpdateStateSpotifyPlayer( string accessToken )
    {
        if (SpotifyModule is null)
        {
            throw new( nameof( SpotifyModule ) );
        }

        return await SpotifyModule.InvokeAsync<IJSObjectReference>( "updateStateSpotifyPlayer" , accessToken );
    }

    public async Task<IJSObjectReference?> InitSpotifyPlayer( string accessToken , IJSObjectReference spotifyModule )
    {
        this.spotifyModule = spotifyModule;
        SpotifyPlayer ??= await CreateSpotifyPlayer( accessToken );
        await SetSpotifyPlayerListeners();
        await ConnectSpotifyPlayer();
        return SpotifyPlayer;
    }

    public async ValueTask DisposeAsync()
    {
        if (SpotifyModule is not null)
        {
            await SpotifyModule.DisposeAsync();
        }

        if (SpotifyPlayer is not null)
        {
            await SpotifyPlayer.DisposeAsync();
        }

        GC.SuppressFinalize( this );
    }
}
