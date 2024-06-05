var key = "AR7dxRTBngNcOUPjkGdbDh2uLkWtjdsMnfdKfYa9vgxjrQVyQpUIJQQJ99AEAC5RqLJUHxJaAAAgAZMPLAuF";
var map;
var markers = [];

function InitMap() {
    map = new atlas.Map("map", {
        view: "Auto",
        center: [27, -26],
        zoom: 8,
        language: "en-GB",
        authOptions: {
            authType: "subscriptionKey",
            subscriptionKey: key
        }
    });

	/*Add the Style Control to the map*/
map.controls.add(new atlas.control.StyleControl({
	mapStyles: ['road', 'grayscale_dark', 'night', 'road_shaded_relief', 'satellite', 'satellite_road_labels', 'high_contrast_dark', 'high_contrast_light', 'grayscale_light'],
	layout: 'list'
  }), {
	position: 'top-right'
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

function setMapView(latitude, longitude, zoomLevel) {
	if (map) {
		map.setCamera({
			center: [longitude, latitude],
			zoom: zoomLevel
		});
	}
}

function SetDarkMode() {
    map.setStyle({ style: "night" });
}

function SetLightMode() {
    map.setStyle({ style: "road" });
}
