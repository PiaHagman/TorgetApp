"use strict";

const connection = new signalR.HubConnectionBuilder().withUrl("/chathub").configureLogging(signalR.LogLevel.Information).build();
connection.start();

let activeChatId = null;
let activeAdId = null;

let chatWindow = document.getElementById("chatWindow");
let referenceBox = document.getElementById("referenceBox");

connection.on("Status", function (status) {
    let cs = document.getElementById("connectionStatus");
    cs.innerHTML = status;
});

if (referenceBox != null) {
    activeAdId = referenceBox.dataset.adid;
}

document.getElementById("chatSend").addEventListener("click", () => {
    let content = document.getElementById("chatInput").value;
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
        let chatId = item.dataset.chatid;
        let adId = item.dataset.adid;
        let currentUser = item.dataset.currentuser;

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