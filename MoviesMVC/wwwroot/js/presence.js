var connectionChat = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/presence")
    .withAutomaticReconnect()
    .build();

connectionChat.on("NotifyNewMessage", function (senderEmail) {
    toastr.success(`${senderEmail} has send a message to you.`,
        "New Message", { positionClass: 'toast-bottom-right' })
});

connectionChat.start();