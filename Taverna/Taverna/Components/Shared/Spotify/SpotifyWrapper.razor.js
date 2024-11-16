export class SpotifyManager {
    static deviceName = 'CdR - Web App';
    static deviceId;
    static player;
    static isInitialized = false;

    static async getPlayer(accessToken) {
        if (!accessToken) {
            return SpotifyManager.player;
        }

        // If player exists but isn't connected, try to reconnect
        if (SpotifyManager.player && !SpotifyManager.player.isConnected) {
            await connectSpotifyPlayer();
            return SpotifyManager.player;
        }

        // Only create new player if one doesn't exist
        if (!SpotifyManager.player) {
            SpotifyManager.player = new Spotify.Player({
                name: SpotifyManager.deviceName,
                getOAuthToken: cb => { cb(accessToken); },
                volume: 1
            });

            SpotifyManager.player.isConnected = false;
            await SpotifyManager.setPlayerListeners();
        }

        // Ensure connection
        if (!SpotifyManager.player.isConnected) {
            await connectSpotifyPlayer();
        }

        return SpotifyManager.player;
    }

    static async setPlayerListeners() {
        if (SpotifyManager.isInitialized) {
            return;
        }

        SpotifyManager.player.addListener('ready', ({ device_id }) => {
            SpotifyManager.deviceId = device_id;
            SpotifyManager.player.isConnected = true;
            DotNet.invokeMethodAsync('Taverna', 'SetSpotifyDeviceId', device_id);
        });

        SpotifyManager.player.addListener('not_ready', ({ device_id }) => {
            console.log('Device disconnected:', device_id);
            SpotifyManager.player.isConnected = false;
        });

        SpotifyManager.player.addListener('initialization_error', ({ message }) => {
            console.error('Initialization error:', message);
            SpotifyManager.player.isConnected = false;
        });

        SpotifyManager.player.addListener('authentication_error', ({ message }) => {
            console.warn('Authentication error:', message);
            SpotifyManager.player.isConnected = false;
            DotNet.invokeMethodAsync('Taverna', 'RefreshSpotifyToken');
        });

        SpotifyManager.player.addListener('account_error', ({ message }) => {
            console.error('Account error:', message);
            SpotifyManager.player.isConnected = false;
        });

        SpotifyManager.player.addListener('player_state_changed', async (state) => {
            if (!state) {
                console.warn("Invalid state received");
                return;
            }

            try {
                await DotNet.invokeMethodAsync('Taverna', 'SpotifyStateHasChanged', state);
            } catch (error) {
                console.warn("Failed to update Spotify state:", error);
            }
        });

        SpotifyManager.isInitialized = true;
    }
}

export async function connectSpotifyPlayer() {
    const currentPlayer = await SpotifyManager.getPlayer();
    if (!currentPlayer) return false;

    try {
        const connectResponse = await Promise.race([
            currentPlayer.connect(),
            new Promise((_, reject) =>
                setTimeout(() => reject(new Error('Connection timeout')), 5000)
            )
        ]);

        if (connectResponse) {
            currentPlayer.isConnected = true;
            console.log('Connected to Spotify');
            return true;
        }
    } catch (error) {
        console.error("Failed to connect to Spotify:", error);
        currentPlayer.isConnected = false;
    }
    return false;
}

export async function createSpotifyPlayer(accessToken) {
    return await SpotifyManager.getPlayer(accessToken);
}

export async function updateStateSpotifyPlayer() {
    const currentPlayer = await SpotifyManager.getPlayer();
    if (!currentPlayer || !currentPlayer.isConnected) {
        await connectSpotifyPlayer();
        return;
    }

    try {
        const playerState = await currentPlayer.getCurrentState();
        await DotNet.invokeMethodAsync('Taverna', 'SpotifyStateHasChanged', playerState);
    } catch (error) {
        console.warn("Failed to update player state:", error);
    }
}

export async function disconnectSpotifyPlayer() {
    const currentPlayer = await SpotifyManager.getPlayer();
    if (currentPlayer && currentPlayer.isConnected) {
        try {
            await currentPlayer.disconnect();
            currentPlayer.isConnected = false;
            console.log('Disconnected from Spotify player');
        } catch (error) {
            console.error("Failed to disconnect:", error);
        }
    }
}

export async function play() {
    const currentPlayer = await SpotifyManager.getPlayer();
    if (!currentPlayer || !currentPlayer.isConnected) {
        await connectSpotifyPlayer();
    }

    try {
        const shouldPlay = await DotNet.invokeMethodAsync('Taverna', 'TransferPlaybackToPlayerJS', SpotifyManager.deviceName);
        if (shouldPlay) {
            await currentPlayer.resume();
        } else {
            await currentPlayer.pause();
        }
    } catch (error) {
        console.error("Playback control failed:", error);
    }
}

export async function pause() {
    const currentPlayer = await SpotifyManager.getPlayer();
    if (currentPlayer && currentPlayer.isConnected) {
        await currentPlayer.pause();
    }
}