"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/restauranthub").build();

//document.getElementById("addorder").addEventListener("click", function (event) {
//    var name = document.getElementById("name").value;
//    var phonenumber = document.getElementById("phonenumber").value;
//    var itemid = document.getElementById("itemid").textContent.toString();

//    connection.invoke("NewOrderMessage", name, phonenumber, itemid).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});
//document.getElementById('begin+' + orderid).addEventListener("click", function (event) {
//    connection.invoke("BeginOrder", orderid.toString()).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});


connection.start().catch(function (err) {
    return console.error(err.toString());
});




//skapa en order innan man skickat (ej sparad), och skicka den till vyn med ordernummer
//document.onload

//connection.joingroup' --ta tillbaka order-id't från addordermetoden, joingroup-id, på reload


//connection.on("OrderProgressMessage", function (stage) {
//    if (stage === 1) {
//        document.getElementById("orderprogress").innerText = "We are working on your order";
//    }
//    if (stage === 2) {
//        document.getElementById("orderprogress").innerText = "Your order is finnished and is waiting for pickup";
//    }
//    if (stage === 3) {
//        document.getElementById("orderprogress").innerText = "Your order has left the kitchen";
//    }

//});

//connection.on("OrderReceivedMessage", function () {
//    document.getElementById("orderprogress").innerText = "We have received your order";
//});