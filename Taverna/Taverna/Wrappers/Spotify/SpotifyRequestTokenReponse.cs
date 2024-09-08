using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

public record SpotifyRequestTokenReponse( [property: JsonPropertyName( "access_token" )] string? AccessToken , [property: JsonPropertyName( "token_type" )] string? TokenType,
    [property: JsonPropertyName( "expires_in" )] int? ExpiresIn );
