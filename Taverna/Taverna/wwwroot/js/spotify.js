export class SpotifyManager {
    static deviceName = 'CdR - Web App';
    static deviceId;
    static player;

    static async getPlayer(accessToken) {
        if (!accessToken) {
            return SpotifyManager.player;
        }

        SpotifyManager.player = new Spotify.Player({
            name: SpotifyManager.deviceName,
            getOAuthToken: cb => { cb(accessToken); },
            volume: 1
        });

        SpotifyManager.player.isConnected = false;

        await SpotifyManager.setPlayerListeners();
        await connectSpotifyPlayer();

        return SpotifyManager.player;
    }

    static async setPlayerListeners(spotifyPlayer) {
        SpotifyManager.player.addListener('ready', ({ device_id }) => {
            SpotifyManager.deviceId = device_id;
            DotNet.invokeMethodAsync('Taverna', 'SetSpotifyDeviceId', device_id);
        });

        SpotifyManager.player.addListener('not_ready', ({ device_id }) => {
            console.log('Você saiu do Spotify', device_id);
        });

        SpotifyManager.player.addListener('initialization_error', ({ message }) => {
            console.error(message);
        });

        SpotifyManager.player.addListener('authentication_error', ({ message }) => {
            DotNet.invokeMethodAsync('Taverna', 'RefreshSpotifyToken');
            console.warn(message);
        });

        SpotifyManager.player.addListener('account_error', ({ message }) => {
            debugger
            console.error(message);
        });

        SpotifyManager.player.addListener('player_state_changed', (state => {
            if (!state) {
                return;
            }

            DotNet.invokeMethodAsync('Taverna', 'SpotifyStateHasChanged', state)
                .then(success => {
                    if (!success) {
                        console.warn("Falha ao atualizar status do Spotify!");
                    }
                });
        }));
    }
}

export function setSpotifyPlayerListeners(spotifyPlayer) {
     spotifyPlayer.addListener('ready', ({ device_id }) => {
        SpotifyManager.deviceId = device_id;
        DotNet.invokeMethodAsync('Taverna', 'SetSpotifyDeviceId', device_id);
    });

    spotifyPlayer.addListener('not_ready', ({ device_id }) => {
        console.log('Você saiu do Spotify', device_id);
    });

    spotifyPlayer.addListener('initialization_error', ({ message }) => {
        console.error(message);
    });

    spotifyPlayer.addListener('authentication_error', ({ message }) => {
        DotNet.invokeMethodAsync('Taverna', 'RefreshSpotifyToken');
        console.warn(message);
    });

    spotifyPlayer.addListener('account_error', ({ message }) => {
        debugger
        console.error(message);
    });

    spotifyPlayer.addListener('player_state_changed', (state => {
        if (!state) {
            return;
        }

        DotNet.invokeMethodAsync('Taverna', 'SpotifyStateHasChanged', state)
            .then(success => {
                if (!success) {
                    console.warn("Falha ao atualizar status do Spotify!");
                }
            });
    }));
}

export function createSpotifyPlayer(accessToken) {
    return new Spotify.Player({
        name: 'CdR',
        getOAuthToken: cb => { cb(accessToken); },
        volume: 1
    });
}

export async function updateStateSpotifyPlayer(spotifyPlayer) {
    const playerState = await DOMTools.handlePromiseWithTimeout(spotifyPlayer.getCurrentState());

    DotNet.invokeMethodAsync('Taverna', 'SpotifyStateHasChanged', playerState);
}

export async function connectSpotifyPlayer(spotifyPlayer) {
    spotifyPlayer.connect().then(success => {
        if (success) {
            console.log('Você está conectado ao Spotify!');
        }
    });
}

export async function disconnectSpotifyPlayer(spotifyPlayer) {
    spotifyPlayer.disconnect().then(success => {
        if (success) {
            console.log('Você desconectou do player do Spotify.');
        }
    });
}

export async function play(spotifyPlayer) {
    DotNet.invokeMethodAsync('Taverna', 'TransferPlaybackToPlayerJS', SpotifyManager.deviceName).then(shouldPlay => {
        if (shouldPlay) {
            spotifyPlayer.resume();
        } else {
            spotifyPlayer.pause();
        }
    });
}

export async function pause() {
    const currentPlayer = await SpotifyManager.getPlayer();
    currentPlayer.pause();
}