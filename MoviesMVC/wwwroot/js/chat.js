$(document).ready(function () {
    $('#messageCard').scrollTop($(this).height());
    var messageCard = document.getElementById("messageCard")
    messageCard.scrollTop = messageCard.scrollHeight - messageCard.getBoundingClientRect().height
});

var connectionChat = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/chat")
    .withAutomaticReconnect()
    .build();

connectionChat.on("NewMessage", function (senderEmail, message, recipientEmail, sendAt) {
    addMessage(senderEmail, message);
});

// connectionChat.on("NewMessage", function (senderEmail, message, recipientEmail, sendAt) {
//     addRowIntoTable("tblUnreadMessages", senderEmail, message, sendAt);
// });

function addMessage(senderEmail, msg) {
    if (msg == null && msg == "") {
        return;
    }
    let ui = document.getElementById("messagesList");
    let li = document.createElement("li");
    li.setAttribute("style", "margin-bottom: 15px; padding-bottom: 10px; border-bottom: 1px dotted #b3a9a9;")
    li.innerHTML = `
    <div style="font-size: 20px;">
        <p>${senderEmail} says: <span style="margin-left: 10px; color: blue;">${msg}</span> </p>
    </div>
    `
    ui.appendChild(li);
    var messageCard = document.getElementById("messageCard")
    messageCard.scrollTop = messageCard.scrollHeight - messageCard.getBoundingClientRect().height
}

connectionChat.start();


function sendMessage(senderEmail, recipientEmail) {
    let inputMsg = document.getElementById("inputMessage");
    var message = inputMsg.value;

    if (message && message != '' && senderEmail && recipientEmail) {
        connectionChat.send("SendMessage", senderEmail, message, recipientEmail);
        inputMsg.value = "";
    }
}

// function addRowIntoTable(tableId, senderEmail, message, sendAt) {
//     if (senderEmail === 'admin@test.com') return;
//     var table = document.getElementById(tableId);
//     var row = table.insertRow(1);
//     var cell1 = row.insertCell(0);
//     var cell2 = row.insertCell(1);
//     var cell3 = row.insertCell(2);
//     var cell4 = row.insertCell(3);
//     cell1.innerHTML = senderEmail;
//     cell2.innerHTML = sendAt;
//     cell3.innerHTML = message;
//     cell4.innerHTML = `
//         <a href="/Messages/Chat?userEmail=${senderEmail}" class="btn btn-default">Go To Chat</a>
//     `;
// }