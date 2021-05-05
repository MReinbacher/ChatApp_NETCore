"use strict";
// Creating a connection to SignalR Hub
var connection = new signalR.HubConnectionBuilder().withUrl("/signalr-server").build();

// Starting the connection with server
connection.start().then(function () { }).catch(function (err) {
    return console.error(err.toString());
});

// Sending the message from Client
document.getElementById("sendBtn").addEventListener("click", async function (event) {
    event.preventDefault();

    var username = await getUser();
    var message = document.getElementById("msg").value;
    connection.invoke("SendMessage", username, message).catch(function (err) {
        return console.error(err.toString());
    });
});

// Subscribing to the messages broadcasted by Server every time when a new message is pushed to it
connection.on("BroadcastMessage", function (msg) {
    console.log(msg);
    add_message(msg);
});

async function add_message(msg) {
    var local_user = await getUser();
    var content = ``;
    if (local_user == msg.user) {
        content = `
        <div class="message-own">
            <div class="font-weight-bold">`+ msg.user + `</div>
            <div>`+ msg.message + `</div>
            <div class="message-time">`+ formatDateTime(msg.timestamp) + `</div>
        </div>
        `;
    } else {
        content = `
        <div class="message">
            <div class="font-weight-bold text-right">`+ msg.user + `</div>
            <div>`+ msg.message + `</div>
            <div class="message-time text-right">`+ formatDateTime(msg.timestamp) + `</div>
         </div>
        `;
    }
    document.getElementById("messages").innerHTML += content;
}

async function getUser() {
    return await fetch("/chat/username")
        .then(async function (response) {
            return await response.json();
        })
        .then(function (json) {
            return json.username;
        });
}

function formatDateTime(timestamp) {
    var x = new Date(timestamp);
    var day = prependZero(x.getDate());
    var month = prependZero(x.getMonth() + 1);
    var year = x.getFullYear();
    var hour = prependZero(x.getHours());
    var minute = prependZero(x.getMinutes());
    var second = prependZero(x.getSeconds());
    return year + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second;
}

function prependZero(x) {
    if (x < 10) {
        x = "0" + x;
    }
    return x;
}