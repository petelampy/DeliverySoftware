var map;
var geocoder;
var directionsService;
var directionsRenderer;

function loadMap() {
    geocoder = new google.maps.Geocoder();
    directionsService = new google.maps.DirectionsService();
    directionsRenderer = new google.maps.DirectionsRenderer();

    var customerPostCode = document.getElementById('customerPostCode').value;
    var customerHouseNumber = document.getElementById('customerHouseNumber').value;
    var combinedCustomerAddress = customerHouseNumber + " , " + customerPostCode;

    var driverPostCode = document.getElementById('driverPostCode').value;
    var driverHouseNumber = document.getElementById('driverHouseNumber').value;
    var combinedDriverAddress = driverHouseNumber + " , " + driverPostCode;

    var isOutForDelivery = document.getElementById('isOutForDelivery').value;

    map = new google.maps.Map(document.getElementById('trackingMap'), {
        zoom: 7,
        disableDefaultUI: true,
        scrollWheelZoom: 'center'
    });

    directionsRenderer.setMap(map);

    createMapMarker(combinedCustomerAddress, "You", true, false);

    if (isOutForDelivery == 'True') {
        createMapMarker(combinedDriverAddress, "Delivery Driver", false, true);
    }
}

function createMapMarker(address, markerName, isCenter, isVan) {

    geocoder.geocode({ address: address }, function (results, status) {
        if (status === 'OK') {
            var location = results[0].geometry.location;

            var icon;
            if (isVan) {
                icon = loadCustomVanIcon(); // Use the van icon
            } else {
                icon = null; // Use the default icon
            }

            var marker = new google.maps.Marker({
                position: location,
                map: map,
                title: markerName,
                icon: icon
            });

            if (isCenter) {
                map.setCenter(location);
            }
            
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}

function loadCustomVanIcon() {
    var vanIcon = {
        url: '../images/icons/deliveryvan.png',
        size: new google.maps.Size(240, 240),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(24, 37),
        scaledSize: new google.maps.Size(30, 30)
    };

    return vanIcon
}