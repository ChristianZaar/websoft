
"use strict";
(function () {
    var duck = document.createElement("img"); 
    addDuck(duck);

    setInterval(function () { 
        blink(duck);
    }, 400);
  })();

function addDuck(duck){
    duck.src = "img/duck.png";
    duck.style.width = "100px";
    duck.style.position = "absolute";
    document.body.appendChild(duck);
    duck.onclick = ()=>{
        duck.style.top = Math.floor(Math.random() * 50 + 25) + '%';
        duck.style.left = Math.floor(Math.random() * 50 + 25) + '%'
    };
}

function blink(duck) {
    if (duck.style.visibility === "visible")
        duck.style.visibility = "hidden";
    else
        duck.style.visibility = "visible";
}
  