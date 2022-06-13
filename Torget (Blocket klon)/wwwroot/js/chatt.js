"use strict";

const connection = new signalR.HubConnectionBuilder().withUrl("/chathub").configureLogging(signalR.LogLevel.Information).build();
connection.start();

var activeChatId = null;
var activeAdId = null;

var chatWindow = document.getElementById("chatWindow");
var referenceBox = document.getElementById("referenceBox");

connection.on("Status", function (status) {
    var cs = document.getElementById("connectionStatus");
    cs.innerHTML = status;
});

if (referenceBox != null) {
    activeAdId = referenceBox.dataset.adid;
}

document.getElementById("chatSend").addEventListener("click", () => {
    var content = document.getElementById("chatInput").value;
    chatWindow.innerHTML += `<p class="right">${content}</p>`

    if (content && activeAdId) {
        connection.invoke("SendMessage", activeAdId, content)
            .catch(function (err) {
            return console.error(err.toString());
            });
        document.getElementById("chatInput").value = "";
    }
    
    event.preventDefault();
});

connection.on("ReceiveMessage", function (message, active) {
    if (activeAdId == active)
        chatWindow.innerHTML += `<p>${message}</p>`
});

document.querySelectorAll(".chat-menu-item").forEach(item => {
    item.addEventListener("click", () => {
        var chatId = item.dataset.chatid;
        var adId = item.dataset.adid;
        var currentUser = item.dataset.currentuser;

        if (activeChatId != chatId) {
            activeChatId = chatId;
            activeAdId = adId;
            chatWindow.innerHTML = "";
            if (referenceBox != null)
                referenceBox.style.display = "none";
            FetchAndRenderMessages(chatId, currentUser);
        }
    });
});

function FetchAndRenderMessages(id, currentUser) {
    connection.invoke("GetMessage", id)
        .then((data) => {
            data.messages.forEach((message) => {
                if (message.userId == currentUser) {
                    chatWindow.innerHTML += `<p class="right">${message.content}</p>`
                } else {
                    chatWindow.innerHTML += `<p>${message.content}</p>`
                }
            });
        })
        .catch((err) => {
            console.error(err.toString());
        });
}