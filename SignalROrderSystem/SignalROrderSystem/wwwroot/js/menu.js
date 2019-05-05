"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/restauranthub").build();

connection.on("OutOfStock", function (id) {
    var buttonsParent = document.getElementById(id);
    var button = buttonsParent.childNodes.item(1);
    button.setAttribute("disabled", true);
    button.classList.add("btn");
    button.classList.add("btn-default");
    button.textContent = 'slut';
});


connection.start().catch(function (err) {
    return console.error(err.toString());
});