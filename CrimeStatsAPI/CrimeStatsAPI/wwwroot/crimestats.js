var CrimeStats = {

    settings: {
        detailedSearchUrl: "http://localhost:44334/DetailedPostcodeLookup/",

        map: {
            zoom: { enabled: true, level: 15, min: 2, max: 22 },
            startposition: { lat: 53.9784, lng: -1.566635 },
            allowpan: true
        }
    },

    currentlocation: {
        longitude: null,
        latitude:null,
        name: null
    },

    currentcrimereports: null,

    search: function(postcode) {

        //I'd usually show the spinner here

        var jqxhr = $.get( CrimeStats.settings.detailedSearchUrl + postcode, function() {})
            .done(function(data, status, request) {

                if (data.length === 0 || Object.keys(data).length === 0) {
                    return;
                } else {
                    
                    if (data.success = true){
                        CrimeStats.currentcrimereports = data.crimeReports.reports;
                        CrimeStats.currentlocation = data.location;
                        CrimeStats.map.drawcrimereports(CrimeStats.currentcrimereports);
                        CrimeStats.map.drawlocation(CrimeStats.currentlocation);
                    }
                }

            })
            .fail(function() {
              //alert( "error" );
            })
            .always(function() {
                //I'd usually hide the spinner here
            });

    },

    map: {

        current: null,

        drawcrimereports: function (crimereports) {

            //Remove group of existing markers
            CrimeStats.map.current.removeLayer(CrimeStats.map.markerGroup);

            //Create new group to add new markers
            var markerGroup = L.layerGroup().addTo(CrimeStats.map.current);
            CrimeStats.map.markerGroup = markerGroup;

            //Draw markers into markergroup
            for (j = 0; j < CrimeStats.currentcrimereports.length; j += 1) {
                var marker = L.marker([crimereports[j].location.latitude, crimereports[j].location.longitude]).addTo(CrimeStats.map.markerGroup);
                
                var crimeoutcome = crimereports[j].outcome || 'None';
                marker.bindPopup('<b>' + crimereports[j].category.toUpperCase() + '</b><br />' + crimereports[j].month + '<br /> Outcome: ' + crimeoutcome);
            }

            var marker = L.marker([CrimeStats.currentlocation.latitude, CrimeStats.currentlocation.longitude]).addTo(CrimeStats.map.markerGroup);
            marker.bindPopup('There are ' + crimereports.length + ' crime reports for ' + CrimeStats.currentlocation.name).openPopup();

        },

        drawlocation: function(location) {

            //Draw search radius onto the map

            var circle = L.circle([location.latitude, location.longitude], {
                color: 'red',
                fillColor: '#f03',
                fillOpacity: 0.2,
                radius: 1609
            }).addTo(CrimeStats.map.markerGroup);

            CrimeStats.map.current.flyTo([location.latitude, location.longitude],14);
        },

        setupmap: function() {

            //Build the initial map view

            var map = L.map('map').setView([CrimeStats.settings.map.startposition.lat,CrimeStats.settings.map.startposition.lng], 13);
            CrimeStats.map.current  = map;

            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: 'Technical test submission by Darryl Leckenby. Using leafletJS and OpenStreetMap'
            }).addTo(CrimeStats.map.current);

            //Setup a default marker group

            var markerGroup = L.layerGroup().addTo(CrimeStats.map.current);
            CrimeStats.map.markerGroup = markerGroup;

            //Setup a handler for the keydown event on the postcode textbox
            $('#txtPostcode').keyup(function (e) {
            if (e.keyCode === 13) {
                CrimeStats.search( $('#txtPostcode').val());
                event.preventDefault();
                return false;
            }
            });

        }

    }

}