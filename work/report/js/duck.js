
"use strict";
(function () {
    var duck = document.createElement("img"); 
    addDuck(duck);
    var move = 0;

    setInterval(function () { 
        move++;
        move %= 8; 
        blink(duck, move);
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

function blink(duck, move) {
    /*if (move === 1){
        duck.style.top = Math.floor(Math.random() * 50 + 25) + '%';
        duck.style.left = Math.floor(Math.random() * 50 + 25) + '%';
    }*/

    if (duck.style.visibility === "visible"){
        setTimeout(function () {
            duck.style.visibility = "hidden";
        }, 500);
    }
    else{
        setTimeout(function () {
            duck.style.visibility = "visible";
        }, 500);
    }
}
  