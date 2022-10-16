export function UserAgent() {
    return navigator.userAgent;
}
export function Network(obj) {
    window.addEventListener("online", () => obj.invokeMethod("Online"))
    window.addEventListener("offline", () => obj.invokeMethod("Offline"))
    return navigator.onLine;
}