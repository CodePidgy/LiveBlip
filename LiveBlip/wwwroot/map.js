var key = "AR7dxRTBngNcOUPjkGdbDh2uLkWtjdsMnfdKfYa9vgxjrQVyQpUIJQQJ99AEAC5RqLJUHxJaAAAgAZMPLAuF";
var map;
var markers = [];

function InitMap() {
    map = new atlas.Map("map", {
        view: "Auto",
        center: [27, -26],
        zoom: 3,
        language: "en-GB",
        authOptions: {
            authType: "subscriptionKey",
            subscriptionKey: key
        }
    });
}

function AddMarker(coords) {
    const marker = new atlas.HtmlMarker({ position: [coords.longitude, coords.latitude] });
    map.markers.add(marker);
    markers.push(marker);
}

function RemoveMarker(index) {
    if (index >= 0 && index < markers.length) {
        map.markers.remove(markers[index]);
        markers.splice(index, 1);
    }
}

function SetDarkMode() {
    map.setStyle({ style: "night" });
}

function SetLightMode() {
    map.setStyle({ style: "road" });
}
