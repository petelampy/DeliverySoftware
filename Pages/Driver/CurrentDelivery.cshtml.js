var map;
var geocoder;
var directionsService;
var directionsRenderer;
var combinedCustomerAddress;

const locationOptions = {
    enableHighAccuracy: true,
    timeout: 5000,
    maximumAge: 0,
};

function error(err) {
    console.warn(`ERROR(${err.code}): ${err.message}`);
}

function success(pos) {
    const position = pos.coords;

    var originLatLng = new google.maps.LatLng(position.latitude, position.longitude);

    geocoder.geocode({ address: combinedCustomerAddress }, function (results, status) {
        if (status === 'OK') {
            var location = results[0].geometry.location;

            map.setCenter(location);
            var request = {
                origin: originLatLng, // Use current location as start point
                destination: combinedCustomerAddress, // Set the customer address as end point
                travelMode: 'DRIVING'
            };
            directionsService.route(request, function (result, status) {
                if (status === 'OK') {
                    directionsRenderer.setDirections(result);
                }
            });
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}

function loadMap() {
    geocoder = new google.maps.Geocoder();
    directionsService = new google.maps.DirectionsService();
    directionsRenderer = new google.maps.DirectionsRenderer();

    var customerPostCode = document.getElementById('dropPostCode').value;
    var customerHouseNumber = document.getElementById('dropHouseNumber').value;
    combinedCustomerAddress = customerHouseNumber + " , " + customerPostCode;

    map = new google.maps.Map(document.getElementById('routeMap'), {
        zoom: 7,
        disableDefaultUI: true,
        scrollWheelZoom: 'center'
    });

    directionsRenderer.setMap(map);

    navigator.geolocation.getCurrentPosition(success, error, locationOptions);
}