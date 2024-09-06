using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Security.Claims;
using Taverna.Client.Pages;
using Taverna.Components;
using Taverna.Scripts.Spotify;
using Taverna.Wrappers.Spotify;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

ConfigureServices( builder.Services , builder.Configuration );

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler( "/Error" );
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof( Taverna.Client._Imports).Assembly);

app.Run();


static void ConfigureServices( IServiceCollection services , IConfiguration configuration )
{
    services.AddOptions();
    services.AddHttpContextAccessor();
    services.AddSession();
    services.AddHttpClient();
    services.AddAuthorizationCore();
    services.AddRazorPages();
    services.AddServerSideBlazor();

    AddSpotifyServiceAuthentication( services,configuration);
}

static void AddSpotifyServiceAuthentication( IServiceCollection services , IConfiguration configuration )
{
    SpotifyCredentialsProvider? spotifyCredentialsProvider = new( configuration );
    services.AddSingleton( provider => spotifyCredentialsProvider );

    services.AddAuthentication( options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    } )
    .AddCookie()
    .AddOAuth( "Spotify" , options =>
    {
        options.ClientId = SpotifyCredentialsProvider.ClientId;
        options.ClientSecret = SpotifyCredentialsProvider.ClientSecret;
        options.CallbackPath = "/signin-spotify"; // Adjust this path as needed
        options.AuthorizationEndpoint = "https://accounts.spotify.com/authorize";
        options.TokenEndpoint = "https://accounts.spotify.com/api/token";
        options.SaveTokens = true;

        options.Scope.Add( "ugc-image-upload" );
        options.Scope.Add( "user-read-playback-state" );
        options.Scope.Add( "user-modify-playback-state" );
        options.Scope.Add( "user-read-currently-playing" );
        options.Scope.Add( "app-remote-control" );
        options.Scope.Add( "streaming" );
        options.Scope.Add( "playlist-read-private" );
        options.Scope.Add( "playlist-read-collaborative" );
        options.Scope.Add( "playlist-modify-private" );
        options.Scope.Add( "playlist-modify-public" );
        options.Scope.Add( "user-follow-modify" );
        options.Scope.Add( "user-follow-read" );
        options.Scope.Add( "user-read-playback-position" );
        options.Scope.Add( "user-top-read" );
        options.Scope.Add( "user-read-recently-played" );
        options.Scope.Add( "user-library-modify" );
        options.Scope.Add( "user-library-read" );
        options.Scope.Add( "user-read-email" );
        options.Scope.Add( "user-read-private" );

        options.Events = new OAuthEvents
        {
            OnCreatingTicket = async context =>
            {
                if (string.IsNullOrEmpty( context.AccessToken ))
                {
                    return;
                }

                if (context.Identity is not null)
                {
                    SpotifyUser? userContextUpdated = await context.Backchannel.GetSpotifyUser( context.AccessToken );

                    ClaimsIdentity? claimsIdentity = (ClaimsIdentity?)context.Principal?.Identity;

                    if(claimsIdentity == null)
                    {
                        return;
                    }

                    string expirationTime = context.ExpiresIn != null
                        ? DateTime.UtcNow.AddSeconds( context.ExpiresIn.Value.TotalSeconds ).ToString()
                        : DateTime.UtcNow.AddHours( 1 ).ToString();

                    claimsIdentity.AddClaim( new Claim( "spotifyAccessTokenExpiration" , expirationTime ) );
                    claimsIdentity.AddClaim( new Claim( "spotifyAccessToken" , context.AccessToken ) );
                    claimsIdentity.AddClaim( new Claim( "spotifyCountry" , userContextUpdated?.Country ?? "" ) );
                    claimsIdentity.AddClaim( new Claim( "spotifyDisplayName" , userContextUpdated?.DisplayName ?? "" ) );
                    claimsIdentity.AddClaim( new Claim( "spotifyEmail" , userContextUpdated?.Email ?? "" ) );
                    claimsIdentity.AddClaim( new Claim( "spotifyHref" , userContextUpdated?.Href ?? "" ) );
                    claimsIdentity.AddClaim( new Claim( "spotifyId" , userContextUpdated?.Id ?? "" ) );
                    claimsIdentity.AddClaim( new Claim( "spotifyType" , userContextUpdated?.Type ?? "" ) );
                    claimsIdentity.AddClaim( new Claim( "spotifyUri" , userContextUpdated?.URI ?? "" ) );
                    claimsIdentity.AddClaim( new Claim( "spotifyProduct" , userContextUpdated?.Product ?? "" ) );
                }
            }
        };
    } );
}