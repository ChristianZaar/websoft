(function () {
    "use strict";

    var restUrl = "https://api.scb.se/UF0109/v2/skolenhetsregister/sv/kommun/";
    //proxy
    var cors = "https://cors-anywhere.herokuapp.com/";
    
    var url = cors + restUrl;

    fetch(url)
        .then((response) => {
            return response.json();
        })
        .then((json) => {
            console.log(json);

            var district = json.Kommuner;
            console.log(district);

            //Set first selection as selected
            var districtCode = district[0].Kommunkod;
            

            console.log(districtCode);
            var select = document.getElementById("districtSelect");
            district.forEach(dist => {
                console.log(dist.Namn);
                select.options[select.options.length] = new Option(dist.Namn,dist.Kommunkod);
            });

            select.addEventListener("change", function() {
                districtCode = select.options[select.selectedIndex].value;
                console.log(districtCode);
                getSchools();
            });

            //Get schools first load
            getSchools();

           function getSchools(){
                fetch(url + districtCode)
                    .then((response) => {
                        return response.json();
                }).catch(function(error){
                    alert(error);
                })
                .then((json) => {
                    console.log('Data retrieved, filling table..');

                    var districtSchools = json.Skolenheter;
                    console.log(districtSchools);

                    //Get body and clear
                    var tableBody = document.getElementById('table-body')
                    
                    console.log(tableBody);
                    var body = "";
                    districtSchools.forEach(school => {
                        body += "<tr><td>" + school.Skolenhetsnamn + "</td>";
                        body += "<td>" + school.Skolenhetskod + "</td>";
                        body += "<td>" + school.Kommunkod + "</td>";
                        body += "<td>" + school.PeOrgNr + "</td></tr>";
                    });
                    console.log(body);
                    tableBody.innerHTML = body;
                })
            
            


            };
        }).catch(function(error){
            alert(error);
        });
})();