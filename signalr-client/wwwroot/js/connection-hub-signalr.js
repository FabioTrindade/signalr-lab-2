"use strict";

const URL_BASE = 'http://localhost/case-alerts-hub';

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

            $('select[id="group-alert"]').on('change', function (e) {
                let group = e.target.value;
                let analyst = $('#analyst-name').val();

                connection.invoke("AddToRoom", analyst, group);
            })

            connection.on('CommunicationReceived', function (response) {
                console.log('Chegou a comunicação ...')
                updatedCaseAlert(response);
            })

            console.log('connected!')
        })
        .catch(reconnect);
}

function reconnect() {
    console.log('reconnecting...');
    setTimeout(startConnection, 2000);
}

function updatedCaseAlert(response) {

    const tableCaseAlert = $('#table-case-alert');

    const tableData = tableCaseAlert.bootstrapTable('getData', false);

    const objectIndex = tableData.findIndex((obj => obj.id == response.id))

    if (objectIndex == -1) {
        tableData.push(response)
    }
    else {
        tableData[objectIndex].analyst = response.analyst;
        tableData[objectIndex].updatedAt = response.updatedAt;
    }

    tableCaseAlert.bootstrapTable('load', response);
}