"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
connection.on("ReceiveMessage", async function (message, ip) {

    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = ` says ${message} ${ip}`;
});

connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

function hello(message) {
    connection.invoke("ConnectPlayer", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
}