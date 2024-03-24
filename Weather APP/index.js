// let weatherUrl = `https://api.openweathermap.org/data/2.5/forecast?q=${input.value}&units=metric&APPID=2095b65c75e8d13fe9e3b0e095b36936`;


/**
 * CITY-NAME-HERE => Should bethe input's value
 * API-KEY-HERE => Should be the api key we generate
 */

// the f-n to prepare the input data
// the function for clearing the inputs

/**
 * Create a HTML file;
 * 2. Create a JS file;
 * 3. Connect the JS file with the HTML and test it;
 * 4. Create simple input text and button in HTML;
 * 5. Select those element;
 * 6. Create a function that makes API request to the corresponding URL (Use JQuerie's AJAX);
 * 7. Analayse the repsonse, think about what you may use or what needs to be generated;
 * 
 * NOTE: Feel free to use BOOTSTRAP;
 */


// http://openweathermap.org/img/w/ICON-CODE-HERE.png Ex: http://openweathermap.org/img/w/10d.png

let inputText = document.getElementById("inputText");
let buttons = document.getElementsByTagName("button");
let searchBtn = document.getElementById("searchBtn");
let myDiv = document.getElementById("myDiv");
let tableToPrint = document.getElementById("tableToPrint");
let homeBtn = document.getElementById("homeBtn");
let hourlyBtn = document.getElementById("hourlyBtn");



function weatherForecast(){
    let urlHelper = `https://api.openweathermap.org/data/2.5/forecast?q=${inputText.value}&units=metric&APPID=2095b65c75e8d13fe9e3b0e095b36936`;
    $.ajax({
        url: urlHelper,
        method: "GET",
        success: function(responce){
            console.log(responce);
            console.log("humidity", responce.list[0].main.humidity);
            console.log("current temp", currentTemp(responce.list));
        //    console.log("City Name is", cityName(responce));
            weatherDataObject.cityName = responce.city.name;
            weatherDataObject.feelsLike = responce.list[0].main.feels_like;
            weatherDataObject.description = responce.list[0].weather[0].description;
            weatherDataObject.icon = responce.list[0].weather[0].icon;
            console.log(responce.city.name);
            console.log("feels_like", responce.list[0].main.feels_like);
            console.log("clouds??");
            console.log("avg temp is", avgTemp(responce.list));
            console.log(maxTemp(responce.list));
            console.log(lowTemp(responce.list));
            console.log("avg humidity is", avgHumidity(responce.list));
            console.log("max humidity is", maxHumidity(responce.list));
            console.log("low humidity is", lowHumidity(responce.list));
            // console.log("icon" , responce.list[0].weather[0].icon);
            printPageOne(myDiv);
            printTable(tableToPrint, responce);
        },
        error: function(error){
            console.log(error)
        }
    })
}

searchBtn.addEventListener("click", function(){
    weatherForecast()
})

let weatherDataObject = {};
let hourlyDataObject = {};

function currentTemp(response){
    let currentTemp = 0;
    for(let i = 0; i < response.length; i++){
        currentTemp = response[0].main.temp;
        weatherDataObject.currentTemp = currentTemp;
        hourlyDataObject.currentTemp = currentTemp;
    }return weatherDataObject.currentTemp, hourlyDataObject.currentTemp;
}


function avgTemp(response){
    let averageTemp = 0;
    for(let i = 0; i < response.length; i++){
        averageTemp += response[i].main.temp / response.length;
        weatherDataObject.avgTemp = averageTemp.toFixed(2);
    }return weatherDataObject.avgTemp;
}

console.log(weatherDataObject);
console.log(hourlyDataObject);

function maxTemp(response){
    let maximumTemp = response[0].main.temp;
    warmestPer = "";
    for(let i = 0; i < response.length; i++){
        if(response[i].main.temp > maximumTemp){
            maximumTemp = response[i].main.temp;
            weatherDataObject.maxTemp = maximumTemp;
            warmestPer = response[i].dt_txt;
            weatherDataObject.warmestPeriod = warmestPer;
        }
    }return weatherDataObject.maxTemp, weatherDataObject.warmestPeriod ;
}

function lowTemp(response){
    let minimumTemp = response[0].main.temp;
    let coldestPer = "";
    for(let i = 0; i < response.length; i++){
        if(response[i].main.temp < minimumTemp){
            minimumTemp = response[i].main.temp;
            weatherDataObject.minTemp = minimumTemp;
            coldestPer = response[i].dt_txt;
            weatherDataObject.coldestPeriod = coldestPer;
        }
    }return weatherDataObject.minTemp, weatherDataObject.coldestPeriod;
}

function avgHumidity(response){
    let averageHum = 0;
    for(let i = 0; i < response.length; i++){
        averageHum += response[i].main.humidity / response.length;
        weatherDataObject.avgHum = averageHum.toFixed(2);
    }return weatherDataObject.avgHum;
}

function maxHumidity(response){
    let maxHum = response[0].main.humidity;
    for(let i = 0; i < response.length; i++){
        if(response[i].main.humidity > maxHum){
           maxHum = response[i].main.humidity;
           weatherDataObject.maximumHum = maxHum;
        }
    }return weatherDataObject.maximumHum;
}


function lowHumidity(response){
    let minHum = response[0].main.humidity;
    for(let i = 0; i < response.length; i++){
        if(response[i].main.humidity < minHum){
            minHum = response[i].main.humidity;
            weatherDataObject.minimumHum = minHum;
        }
    }return weatherDataObject.minimumHum;
}

function printPageOne(elementToPrintIn, response){
    elementToPrintIn.innerHTML = "";
    elementToPrintIn.innerHTML += `
    <h1>Weather Forecast</h1>
    <br />
    <h3>City Name: ${weatherDataObject.cityName}</h3>
    <br />
    <h3>Current temp: ${weatherDataObject.currentTemp} Feels Like: ${weatherDataObject.feelsLike} </h3>
    <br />
    <h3>${weatherDataObject.description}: <img src = "http://openweathermap.org/img/w/${weatherDataObject.icon}.png"></h3> 
    <br />
    <h4>Max temp: ${weatherDataObject.maxTemp}   Max humidity: ${weatherDataObject.maximumHum}</h4>
    <br />
    <h4>Avg temp: ${weatherDataObject.avgTemp}   Avg humidity: ${weatherDataObject.avgHum}</h4>
    <br />
    <h4>Low temp: ${weatherDataObject.minTemp}   Low humidity: ${weatherDataObject.minimumHum}</h4>
    <br />
    <br />
    <br />
    <h3>Warmest time of the period: ${weatherDataObject.warmestPeriod} </h3>
    <br />
    <h3>Coldest time of the period: ${weatherDataObject.coldestPeriod} </h3>
    <br />
    `
}

// PAGE 2


// function printTable(elementToPrintIn, responces){
//     let table = document.createElement("table");
//     elementToPrintIn.innerHTML = "";
//     for(let i = 0; i < responces.list.length; i++){
//         let tableRow = document.createElement("tr");
//         let tableData = document.createElement("td");
//         tableData.innerHTML = `${responces.list[i].weather[0].description}`
//         console.log(responces.list[i]);
//         tableRow.appendChild(tableData);
//         table.appendChild(tableRow);
//     }
//     elementToPrintIn.appendChild(table);
// }

// printTable(tableToPrint)


function printTable(elementToPrintIn, responces){
    console.log("print table res", responces);
    console.log("print table res list", responces.list);
    let newArray = responces.list;
    elementToPrintIn.innerHTML = "<h2>Hourly data</h2>";
    let table = document.createElement("table");
    let tableRow1 = document.createElement("tr");
    let td1 = document.createElement("td");
    //td1.style.padding
    td1.setAttribute("class", "tableTdClass")
    let td2 = document.createElement("td");
    let td3 = document.createElement("td");
    let td4 = document.createElement("td");
    let td5 = document.createElement("td");
    let td6 = document.createElement("td");
    td1.innerHTML = "Icon";
    td2.innerHTML = "Description";
    td3.innerHTML = "Date";
    td4.innerHTML = "Temperature";
    td5.innerHTML = "Humidity";
    td6.innerHTML = "Wind";
    tableRow1.append(td1, td2, td3, td4, td5, td6);
    table.append(tableRow1);
    newArray.forEach((resp) => {
        let tableRow = document.createElement("tr");
        let tableData = document.createElement("td");
        let tableData1 = document.createElement("td");
        let tableData2 = document.createElement("td");
        let tableData3 = document.createElement("td");
        let tableData4 = document.createElement("td");
        let tableData5 = document.createElement("td");
        tableData.innerHTML = `<img src = "http://openweathermap.org/img/w/${resp.weather[0].icon}.png"></img>`
        tableData1.innerHTML = `${resp.weather[0].description}`;
        tableData2.innerHTML = `${resp.dt_txt}`;
        tableData3.innerHTML = `${resp.main.temp}`;
        tableData4.innerHTML = `${resp.main.humidity}`;
        tableData5.innerHTML = `${resp.wind.speed}`;
        tableRow.append(tableData, tableData1, tableData2, tableData3, tableData4, tableData5);
        table.append(tableRow);
    })
    elementToPrintIn.append(table);
}

homeBtn.addEventListener("click", () => {
    weatherForecast()
    myDiv.style.display = "flex";
    myDiv.style.justifyContent = "center";
    myDiv.style.alignItems = "center";
    tableToPrint.style.display = "none";
})

hourlyBtn.addEventListener("click", () => {
    weatherForecast()
    myDiv.style.display = "none";
    tableToPrint.style.display = "flex";
    tableToPrint.style.justifyContent = "center";
    tableToPrint.style.alignItems = "center";
})




