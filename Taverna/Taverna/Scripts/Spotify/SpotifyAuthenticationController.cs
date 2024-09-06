using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Taverna.Scripts.Spotify
{
    [Authorize(AuthenticationSchemes = "Spotify")]
    public class SpotifyAuthenticationController : Controller
    {
        [HttpGet("challenge-spotify")]
        public IActionResult ChallengeSpotify()
        {
            // Trigger the Spotify OAuth2 authentication flow.
            var properties = new AuthenticationProperties
            {
                RedirectUri = "/fichas",
            };

            return Challenge(properties, "Spotify");
        }

        [HttpGet("signin-spotify")]
        public async Task<IActionResult> OnUserAuthorized(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return RedirectToAction("/fichas"); // Handle error or redirect as needed
            }

            // Exchange the code for an access token and user claims
            var authenticationResult = await HttpContext.AuthenticateAsync("Spotify");

            if (authenticationResult.Succeeded)
            {
                // User successfully authenticated with Spotify
                // You can access user claims and handle the authentication as needed

                return RedirectToAction("Authenticated"); // Redirect to a success page
            }
            else
            {
                // Handle authentication failure
                return RedirectToAction("Login"); // Redirect to a login page or error page
            }
        }
    }
}
