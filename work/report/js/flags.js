(function () {
    "use strict";
    var flag = {
        flagContainer : document.getElementById("flagContainer"),
        flagVertical : document.getElementById("crossVertical"),
        flagHorizontal : document.getElementById("crossHorizontal"),
    };

    document.getElementById("sweden-btn").addEventListener("click", swedenFlag);
    document.getElementById("denmark-btn").addEventListener("click", denmarkFlag);
    document.getElementById("finland-btn").addEventListener("click", finlandFlag);
    flag.flagContainer.addEventListener("click", hideFlag);
    //Set hidden at start
    hideFlag();

    function swedenFlag(){
        setOpacityToFlag(1);
        flag.flagContainer.style.background = "blue";
        flag.flagHorizontal.style.background = "yellow";
        flag.flagVertical.style.background = "yellow";
    };

    function denmarkFlag(){
        setOpacityToFlag(1);
        flag.flagContainer.style.background = "red";
        flag.flagHorizontal.style.background = "white";
        flag.flagVertical.style.background = "white";
    };

    function finlandFlag(){
        setOpacityToFlag(1);
        flag.flagContainer.style.background = "white";
        flag.flagHorizontal.style.background = "blue";
        flag.flagVertical.style.background = "blue";
    };

    function setOpacityToFlag(value){
        flag.flagContainer.style.opacity = value;
    }

    function hideFlag(){
        setOpacityToFlag(0);
    }
}());