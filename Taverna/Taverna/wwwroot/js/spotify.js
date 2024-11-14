class SpotifyManager {
    static deviceId;
    static player;

    static async getPlayer(accessToken) {
        
        if (!accessToken && !SpotifyManager.player) {
            throw new Error("Player does not exist and no access token was provided.");
        }

        if (SpotifyManager.player && !accessToken) {
            return SpotifyManager.player;
        }

        if (SpotifyManager.player && accessToken) {
            await SpotifyManager.player.disconnect();
        }

        SpotifyManager.player = new Spotify.Player({
            name: 'CdR',
            getOAuthToken: cb => { cb(accessToken); },
            volume: 1
        });

        await SpotifyManager.setPlayerListeners();

        return SpotifyManager.player;
    }

    static async setPlayerListeners() {
        const currentPlayer = await SpotifyManager.getPlayer();
        currentPlayer.addListener('ready', ({ device_id }) => {
            SpotifyManager.deviceId = device_id;
            DotNet.invokeMethodAsync('Taverna', 'SetSpotifyDeviceId', device_id);
        });

        currentPlayer.addListener('not_ready', ({ device_id }) => {
            console.log('Você saiu do Spotify', device_id);
        });

        currentPlayer.addListener('initialization_error', ({ message }) => {
            debugger
            console.error(message);
        });

        currentPlayer.addListener('authentication_error', ({ message }) => {
            DotNet.invokeMethodAsync('Taverna', 'RefreshSpotifyToken');
            console.warn(message);
        });

        currentPlayer.addListener('account_error', ({ message }) => {
            debugger
            console.error(message);
        });

        currentPlayer.addListener('player_state_changed', (state => {
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

export async function createSpotifyPlayer(accessToken) {
    const player =  SpotifyManager.getPlayer(accessToken);
    return player;
}

export async function updateStateSpotifyPlayer() {
    const currentPlayer = await SpotifyManager.getPlayer();
    var playerState = await currentPlayer.getCurrentState();

    DotNet.invokeMethodAsync('Taverna', 'SpotifyStateHasChanged', playerState);
}

export async function connectSpotifyPlayer() {
    const currentPlayer = await SpotifyManager.getPlayer();
    const success = await currentPlayer.connect();

    if (success) {
        console.log('Você está conectado ao Spotify!');
    }
}

export async function disconnectSpotifyPlayer() {
    const currentPlayer = await SpotifyManager.getPlayer();
    const success = await currentPlayer.disconnect();

    if (success) {
        console.log('Você desconectou do player do Spotify.');
    }
}

export async function play() {
    const currentPlayer = await SpotifyManager.getPlayer();
    var playerState = await currentPlayer.getCurrentState();

    if (playerState === null) {
        await connectSpotifyPlayer();
    }

    DotNet.invokeMethodAsync('Taverna', 'TransferPlaybackToPlayerJS').then(shouldPlay => {
        if (shouldPlay) {
            currentPlayer.resume();
        } else {
            currentPlayer.pause();
        }
    });
}

export function pause() {
    const currentPlayer = await SpotifyManager.getPlayer();
    currentPlayer.pause();
}