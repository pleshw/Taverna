export class SpotifyManager {
    static deviceName = 'CdR - Web App';
    static deviceId;
    static player;

    static async getPlayer(accessToken) {
        if (!accessToken || SpotifyManager.player) {
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

    static async  setPlayerListeners() {
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
            console.log(state);

            if (!state) {
                console.error("Invalid state");

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
    return await SpotifyManager.getPlayer(accessToken);
}

export async function updateStateSpotifyPlayer() {
    const currentPlayer = await SpotifyManager.getPlayer();
    const playerState = await DOMTools.handlePromiseWithTimeout(currentPlayer.getCurrentState());

    DotNet.invokeMethodAsync('Taverna', 'SpotifyStateHasChanged', playerState);
}

export async function connectSpotifyPlayer() {
    const currentPlayer = await SpotifyManager.getPlayer();
    const connectResponse = await DOMTools.handlePromiseWithTimeout(currentPlayer.connect());

    if (connectResponse.error) {
        console.error("Connection to Spotify timed out.")
        return;
    }

    const responseSuccess = connectResponse.data();

    if (responseSuccess) {
        currentPlayer.isConnected = true;
        console.log('Você está conectado ao Spotify!');
    } else {
        console.warn('Falha ao conectar ao Spotify, atualize a página e tente novamente.');
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
    
    DotNet.invokeMethodAsync('Taverna', 'TransferPlaybackToPlayerJS', SpotifyManager.deviceName).then(async shouldPlay => {
        if (shouldPlay) {
            await currentPlayer.resume();
        } else {
            await currentPlayer.pause();
        }
    });
}

export async function pause() {
    const currentPlayer = await SpotifyManager.getPlayer();
    currentPlayer.pause();
}