console.log("connected")

let numberBtn = document.getElementsByClassName("numberBtn");
// console.log(numberBtn);
let output = document.getElementsByClassName("output");
let allClearBtn = document.getElementById("allClearBtn");
let deleteBtn = document.getElementById("deleteBtn");
let addBtn = document.getElementById("addBtn");
let subtractBtn = document.getElementById("subtractBtn");
let multiplyBtn = document.getElementById("multiplyBtn");
let divideBtn = document.getElementById("divideBtn");
let one = document.getElementById("one");
let numbersBtn = document.getElementById("numbersBtn");
let decimalBtn = document.getElementById("decimalBtn");
let equalBtn = document.getElementById("equalBtn");
let previousOperand = document.getElementById("previousOperand");
let currentOperand = document.getElementById("currentOperand");
let activeOperator = document.getElementById("actualOperator");

// function transformIntoString(arrayOfNumbers, operator){
//     // concNumber = "";
//     // display.innerHTML = "";
//     // if(arrayOfNumbers){
//     // for(let i = 0; i < arrayOfNumbers.length; i++){
//     //     //concatenate numbers to be displayed => display numbers as a string
//     //     display.innerHTML += "" + arrayOfNumbers[i];
//     // }
//     // return display;
// //}
// }
// //     if(operator === "+" || operator === "-" || operator === "*" || operator === "/"){
// //         previousOperand = currentOperand;
// //         let newCurrent =  currentOperand.join(operator);
// //     }
// //   return parseInt(newCurrent)
// // }
// // let array = [2, 3, 5, 6, 7];
// // let operation = "-" ;
// // let result = display(array, operation);
// // console.log(result)

decimalBtn.addEventListener('click', function(event) {

    let decimalClicked  = event.target;
    if(currentOperand.innerHTML.includes(decimalClicked.value) == true){
       return
    }else
    currentOperand.innerHTML += decimalClicked.value;
})

function addEventList (arrayOfButtons){
    for(let i = 0; i < arrayOfButtons.length; i++){
        arrayOfButtons[i].addEventListener("click", function(event){
            event.preventDefault();
            let btn = event.target;
            currentOperand.innerHTML += btn.value;
        })
    }
}

addEventList(numberBtn);

function clear(){
    currentOperand.innerHTML = "";
    activeOperator.innerHTML = "";
    previousOperand.innerHTML = "";

}
allClearBtn.addEventListener("click", function(){
    clear()
})

console.log(currentOperand.value)


function deleteChar(){
    let helpString = currentOperand.innerText;
    helpString = helpString.slice(0, -1); 
    currentOperand.innerText = helpString;
    console.log(helpString);
}

function sumNumbers(){
    
    let numberA = previousOperand.innerText;
    let numberB = currentOperand.innerText;

    if(numberA != '' && numberB != ''){ // ako ima vrednosti izvrsi ja soodvetnata operacija (vo zavisno od prethodno kliknatiot operator)
        calculateNumbers();                       
    }
    else{ // ako e poleto za cuvanje na rezutatot prazno, samo premesti ja vrednosta od poleto za vnes vo poleto za cuvanje na rezultatot
        if(numberA != '')
            previousOperand.innerText = numberA;  
        else
            previousOperand.innerText = numberB;          
    }

    // stavi go za aktiven operator (zosto toj si go kliknal veke)
    activeOperator.innerText = "+";
    // spremi go za nova vrednost
    currentOperand.innerText = "";
          
}

function calculateNumbers(){
    
    let actOp = activeOperator.innerText;
    let sumValue = 0;

    if(actOp == '+')
            sumValue = parseFloat(previousOperand.innerText) + parseFloat(currentOperand.innerText);
        else if(actOp == '-')
            sumValue = parseFloat(previousOperand.innerText) - parseFloat(currentOperand.innerText);
        else if(actOp == '*')
            sumValue = parseFloat(previousOperand.innerText) * parseFloat(currentOperand.innerText);
        else if(actOp == '/')
            sumValue = parseFloat(previousOperand.innerText) / parseFloat(currentOperand.innerText);
        else //if actOp == "="
            sumValue = currentOperand.innerText;

        previousOperand.innerText = sumValue;
}

function substractNumbers(){

    var numberA = previousOperand.innerText;
    var numberB = currentOperand.innerText;
    
    if(numberA != '' && numberB != ''){ // ako ima vrednosti izvrsi ja soodvetnata operacija (vo zavisno od prethodno kliknatiot operator)
        calculateNumbers();                       
    }
    else{ // ako e poleto za cuvanje na rezutatot prazno, samo premesti ja vrednosta od poleto za vnes vo poleto za cuvanje na rezultatot
        if(numberA != '')
            previousOperand.innerText = numberA;  
        else
            previousOperand.innerText = numberB;        
    }
    // stavi go za aktiven operator (poso toj si go kliknal)
    activeOperator.innerText = "-";
    // spremi go za nova vrednost
    currentOperand.innerText = "";
}

function multiplyNumbers(){

    var numberA = previousOperand.innerText;
    var numberB = currentOperand.innerText;
    
    if(numberA != '' && numberB != ''){ // ako ima vrednosti izvrsi ja soodvetnata operacija (vo zavisno od prethodno kliknatiot operator)
        calculateNumbers();                       
    }
    else{ // ako e poleto za cuvanje na rezutatot prazno, samo premesti ja vrednosta od poleto za vnes vo poleto za cuvanje na rezultatot

        if(numberA != '')
            previousOperand.innerText = numberA;  
        else
            previousOperand.innerText = numberB;        
    }

    // stavi go za aktiven operator (poso toj si go kliknal)
    activeOperator.innerText = "*";
    // spremi go za nova vrednost
    currentOperand.innerText = "";
}

function divideNumbers(){

    var numberA = previousOperand.innerText;
    var numberB = currentOperand.innerText;
    
    if(numberA != '' && numberB != ''){ // ako ima vrednosti izvrsi ja soodvetnata operacija (vo zavisno od prethodno kliknatiot operator)
        calculateNumbers();                       
    }
    else{ // ako e poleto za cuvanje na rezutatot prazno, samo premesti ja vrednosta od poleto za vnes vo poleto za cuvanje na rezultatot
        
        if(numberA != '')
            previousOperand.innerText = numberA;  
        else
            previousOperand.innerText = numberB;  
    }

    // stavi go za aktiven operator (poso toj si go kliknal)
    activeOperator.innerText = "/";
    // spremi go za nova vrednost
    currentOperand.innerText = "";
}


function equalOperator(){
    var numberA = previousOperand.innerText;
    var numberB = currentOperand.innerText;
    
    if(numberA != '' && numberB != ''){ // ako ima vrednosti izvrsi ja soodvetnata operacija (vo zavisno od prethodno kliknatiot operator)
        calculateNumbers();                       
    }
    else{ // ako e poleto za cuvanje na rezutatot prazno, samo premesti ja vrednosta od poleto za vnes vo poleto za cuvanje na rezultatot
        
        if(numberA != '')
            previousOperand.innerText = numberA;  
        else
            previousOperand.innerText = numberB;  
    }

    // stavi go za aktiven operator (poso toj si go kliknal)
    activeOperator.innerText = "=";
    // spremi go za nova vrednost
    currentOperand.innerText = "";
}


