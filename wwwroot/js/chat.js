"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var message;
var receiverId;
var receiverName;
var senderId;
var senderName;

document.getElementById("messageInput").disabled = true;

connection.on("ReceiveMessage", function (user, msg) {
    if (user == senderName) {
        document.getElementById("messagesList").innerHTML = document.getElementById("messagesList").innerHTML + "<div class='card d-flex align-items-end border-0'><div class='card mb-4 mt-3 mr-1 ml-1'><div class='card-header'>" + user + "</div><div class='card-body'>" + msg + "</div></div></div>";
    }
    else {
        document.getElementById("messagesList").innerHTML = document.getElementById("messagesList").innerHTML + "<div class='card d-flex align-items-start border-0'><div class='card mb-4 mt-3 mr-1 ml-1'><div class='card-header'>" + user + "</div><div class='card-body'>" + msg + "</div></div></div>";
    }
});

connection.start().then(function () {
    document.getElementById("messageInput").disabled = false;
    receiverId = document.getElementById("receiverId").textContent;
    receiverName = document.getElementById("receiverName").textContent;
    senderId = document.getElementById("senderId").textContent;
    senderName = document.getElementById("senderName").textContent;

}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("messageInput").addEventListener("keyup", function (event) {
    if (event.keyCode == 13) {
        sendMessage();
        event.preventDefault();
    }
});
document.getElementById("sendButton").addEventListener("click", function (event) {
    sendMessage();
    event.preventDefault();
});

function sendMessage() {
    message = document.getElementById("messageInput").value;
    document.getElementById("messageInput").value = "";
    connection.invoke("SendMessage", receiverId, receiverName, senderId, senderName, message).catch(function (err) {
        return console.error(err.toString());
    });
}