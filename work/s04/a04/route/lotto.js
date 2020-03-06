
"use strict"


const express = require("express");
const router  = express.Router();

function getNumbers(){

  var picks = [];

  while (picks.length < 7){
     var randomNumber = Math.floor(Math.random() * 36);
	 if ( ! picks.includes(randomNumber))
		picks.push(randomNumber);
	 
  }

  return picks;
}