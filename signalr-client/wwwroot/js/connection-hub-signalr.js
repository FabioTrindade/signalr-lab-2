"use strict";

const URL_BASE = 'http://localhost:5080/case-alerts-hub';

const connection = new signalR.HubConnectionBuilder()
    .withUrl(URL_BASE, {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
    }).build();

connection.onclose(reconnect);
startConnection();

function startConnection() {
    console.log('Started connecting...');
    connection.start()
        .then(() => {

            $('#btn-alert').on('click', function () {
                let group = $('select[id="group-alert"]').val();
                connection.invoke("CommunicateGroup", group);
            })

            $('select[id="group-alert"]').on('change', function (e) {
                let group = e.target.value;
                let analyst = $('#analyst-name').val();

                connection.invoke("AddToRoom", analyst, group);
            })

            connection.on('CommunicationReceived', function (response) {
                console.log(response);
                $('#table-case-alert').bootstrapTable('load', response)
            })

            console.log('connected!')
        })
        .catch(reconnect);
}

function reconnect() {
    console.log('reconnecting...');
    setTimeout(startConnection, 2000);
}
