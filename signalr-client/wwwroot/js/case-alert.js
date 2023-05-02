$(function () {
    CreateComboAjax('group-alert', '/get-group-alerts');
    CreateTableCaseAlert();
})

function CreateTableCaseAlert() {
    $('#table-case-alert').bootstrapTable({
        toolbar: '#toolbar',
        classe: 'table table-hover',
        pagination: true,
        rowStyle: rowStyle,
        columns: [
            {
                field: 'operate', title: 'Acao', align: 'center',
                formatter: function (value, row, index) {

                    let btnicon = !row.analyst ? 'fa-clock-rotate-left' : 'fa-user-clock';
                    let btnclass = !row.analyst ? 'btn-warning' : 'btn-success';
                    let btnfunction = !row.analyst ? `onclick="StartInvestigation('${row.id}');` : '';
                    let btndisabled = !row.analyst ? '' : 'disabled="true"';

                    return [
                        `<button class="btn ${btnclass} assing-analyst" id="${row.id}" ${btnfunction} ${btndisabled}">
                        <i class="fas ${btnicon}" >
                        </i>
                    </button>`
                    ].join('');
                }
            },
            { field: 'name', title: 'Name' },
            { field: 'description', title: 'Description' },
            { field: 'isActive', title: 'IsActive', align: 'center', },
            {
                field: 'createdAt', title: 'CreateAt', align: 'center',
                formatter: function valueFormatter(value, row, index) {
                    return (!value ? '-' : moment(row.createAt).format("DD/MM/YYYY HH:mm:ss"))
                }
            },
            {
                field: 'updatedAt', title: 'UpdateAt', align: 'center',
                formatter: function valueFormatter(value, row, index) {
                    return (!value ? '-' : moment(row.updateAt).format("DD/MM/YYYY HH:mm:ss"))
                }
            },
            { field: 'analyst', title: 'Analyst', align: 'center', },
        ]
    })
}

function LoadTableCaseAlert(analyst, group) {
    $('#table-case-alert').bootstrapTable('refreshOptions',{
        url: `/get-case-alerts?analyst=${analyst}&group=${group}`
    })
}

function StartInvestigation(id) {

    let command = {
        analyst: $('#analyst-name').val()
    }

    SendRequest('PATCH', `/update-analyst-case-alert/${id}`, 'JSON', command, (response) => {
        console.log('Alerta atribuido');
    }, (error) => {
        alert(`Não foi possivel atribuir o alerta devido: ${error}`);
    })
}

function rowStyle(row, index) {
    if (!!row.updateAt) {
        return { classes: 'bg-opacity-10 bg-success' }
    }
    return {
        css: { color: '' }
    }
}