var key = "AR7dxRTBngNcOUPjkGdbDh2uLkWtjdsMnfdKfYa9vgxjrQVyQpUIJQQJ99AEAC5RqLJUHxJaAAAgAZMPLAuF";
var map;
var markers = [];

function InitMap() {
    map = new atlas.Map(
		"map", {
			view: "Auto",
			center: [27, -26],
			zoom: 3,
			language: "en-GB",
			authOptions: {
				authType: "subscriptionKey",
				subscriptionKey: key
			}
    	}
	);
}

function AddMarker(coords) {
	const marker = new atlas.HtmlMarker({ position: [coords.longitude, coords.latitude] });

    map.markers.add(marker);
	markers.push(marker);

    return marker;
}

function RemoveMarker(index) {
	map.markers.remove(markers[index]);
	markers = markers.filter(m => m !== markers[index]);
}

function SetDarkMode() {
	map.setStyle({ style: "night" });
}

function SetLightMode() {
	map.setStyle({ style: "road" });
}
