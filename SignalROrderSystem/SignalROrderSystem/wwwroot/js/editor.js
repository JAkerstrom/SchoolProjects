"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/restauranthub").build();


document.getElementById("1").addEventListener("click", function (event) {
    event.target.setAttribute("disabled", true);
    connection.invoke("OutOfStock", '1').catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("2").addEventListener("click", function (event) {
    event.target.setAttribute("disabled", true);
    connection.invoke("OutOfStock", '2').catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("3").addEventListener("click", function (event) {
    event.target.setAttribute("disabled", true);
    connection.invoke("OutOfStock", '3').catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("4").addEventListener("click", function (event) {
    event.target.setAttribute("disabled", true);
    connection.invoke("OutOfStock", '4').catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("5").addEventListener("click", function (event) {
    event.target.setAttribute("disabled", true);
    connection.invoke("OutOfStock", '5').catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("6").addEventListener("click", function (event) {
    event.target.setAttribute("disabled", true);
    connection.invoke("OutOfStock", '6').catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("7").addEventListener("click", function (event) {
    event.target.setAttribute("disabled", true);
    connection.invoke("OutOfStock", '7').catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});


connection.start().catch(function (err) {
    return console.error(err.toString());
});

