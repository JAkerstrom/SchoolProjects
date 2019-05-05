"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/restauranthub").build();



connection.on("OrderProgressMessage", function (stage) {
    if (stage === 1) {
        document.getElementById("orderprogress").innerText = "We are working on your order";
    }
    if (stage === 2) {
        document.getElementById("orderprogress").innerText = "Your order is finnished and is waiting for pickup";
    }
    if (stage === 3) {
        document.getElementById("orderprogress").innerText = "Your order has left the kitchen";
    }

});

connection.start().then(function () {
    var orderid = document.getElementById("orderid").innerText;
    connection.invoke("AddClient", orderid).catch(function (err) {
        return console.error(err.toString());
    })
    }).catch(function (err) {
        return console.error(err.toString());
});






