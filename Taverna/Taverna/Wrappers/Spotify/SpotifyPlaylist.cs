﻿using System.Text.Json.Serialization;

namespace Taverna.Wrappers.Spotify;

public record SpotifyPlaylist ( [property: JsonPropertyName( "collaborative" )] bool? IsCollaborative , [property: JsonPropertyName( "description" )] string? Description , [property: JsonPropertyName( "external_urls" )] SpotifyExternalUrls? ExternalUrls , [property: JsonPropertyName( "href" )] string? PlaylistURL , [property: JsonPropertyName( "id" )] string? PlaylistId , [property: JsonPropertyName( "images" )] List<SpotifyImage>? Images , [property: JsonPropertyName( "name" )] string? PlaylistName , [property: JsonPropertyName( "owner" )] SpotifyPlaylistUser? PlaylistOwner , [property: JsonPropertyName( "public" )] bool? IsPublic , [property: JsonPropertyName( "snapshot_id" )] string? SnapshotId , [property: JsonPropertyName( "tracks" )] SpotifyPlaylistTrackCount? TrackCount , [property: JsonPropertyName( "type" )] string? Type , [property: JsonPropertyName( "uri" )] string? PlaylistUri, [property: JsonPropertyName("tracks")]  SpotifyPlaylistTracksQuery? Tracks );
