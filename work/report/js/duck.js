
(function () {
    setInterval(function () { 
      blink();
    }, 1000);
  })();

function blink() {

    var duck  = document.getElementById("duck-img") 
    if (document.getElementById("duck-img") != null) {
        if (document.getElementById("duck-img").style.visibility === "visible"){
            setTimeout(function () {
                document.getElementById("duck-img").style.visibility = "hidden";
            }, 500);
        }
        else{
            setTimeout(function () {
                document.getElementById("duck-img").style.visibility = "visible";
            }, 500);
        }
    }
}
  