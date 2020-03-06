
"use strict"


const express = require("express");
const router  = express.Router();


router.get("/lotto", (req, res) => {
  let numbers = getNumbers();
  let queryString = (req.query.row);

  let lotto = formatData(numbers, queryString)
  res.render("lotto", lotto);

})

function getNumbers(){

  let picks = [];

  while (picks.length < 7){
      let randomNumber = 1 + Math.floor(Math.random() * 35);
	 if ( ! picks.includes(randomNumber))
		  picks.push(randomNumber);
  }

  return picks;
}

//Json lotto
router.get('/lotto-json', (req, res) => {
  let queryString = (req.query.row);

  let numbers = getNumbers();

  let lotto = formatData(numbers, queryString);
  res.json(lotto);
})

function formatData(numbers, queryString){
  let data = {
    correct_numbers : numbers
  };
  if (queryString !== undefined){
    //parses string to int
      let arr = stringToNumArray(queryString);
      data.your_numbers = arr;
      data.lucky_numbers = arr.filter(element => data.correct_numbers.includes(element));
  }
  else{
    data.your_numbers ="";
    data.lucky_numbers = "";
  }
  return data;
}



function stringToNumArray(str){
    return str.split(",").map(function(item) {
      return parseInt(item, 0);
    });
}

module.exports = router;