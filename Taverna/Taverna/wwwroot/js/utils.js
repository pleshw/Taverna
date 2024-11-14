function fetchWithTimeout(url, fetchParameters = {}, timeout = 2000) {
    return Promise.race([
        fetch(url, fetchParameters),
        new Promise((_, reject) =>
            setTimeout(() => reject(new Error("Request timed out")), timeout)
        )
    ]);
}