// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var spusteno = false;
var casovac;
var sekundy = 0;
var minuty = 0;
var hodiny = 0;

function startStop() {

    var pocatecniHodinyElement = document.getElementById("hodiny");
    hodiny = pocatecniHodinyElement.innerHTML;
    var pocatecniHodinyElement = document.getElementById("minuty");
    minuty = pocatecniHodinyElement.innerHTML;
    var pocatecniHodinyElement = document.getElementById("sekundy");
    sekundy = pocatecniHodinyElement.innerHTML;

    if (spusteno) {
        spusteno = false;
        clearInterval(casovac);
    } else {
        spusteno = true;
        casovac = setInterval(aktualizovatCas, 1000);
    }
}

function aktualizovatCas() {
    sekundy++;
    if (sekundy === 60) {
        sekundy = 0;
        minuty++;
        if (minuty === 60) {
            minuty = 0;
            hodiny++;
            if (hodiny === 24) {
                sekundy = 0;
                minuty = 0;
                hodiny = 0;
            }
        }
    }
    document.getElementById("hodiny").innerHTML = formatujCas(hodiny);
    document.getElementById("minuty").innerHTML = formatujCas(minuty);
    document.getElementById("sekundy").innerHTML = formatujCas(sekundy);

    var inputElement = document.getElementById("stopkyHodiny");
    inputElement.value = hodiny;
    inputElement = document.getElementById("stopkyMinuty");
    inputElement.value = minuty;
    inputElement = document.getElementById("stopkySekundy");
    inputElement.value = sekundy;
}

function formatujCas(cas) {
    if (cas == 00) {
        return cas;
    }
    return cas < 10 ? "0" + cas : cas;
}

document.getElementById("startStopButton").addEventListener("click", startStop);