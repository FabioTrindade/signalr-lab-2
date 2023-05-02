$(function () {
    Login();
})

function Login() {
    let analystName = GetValue('analyst');
    if (!analystName || analystName == 'null') {
        analystName = prompt('Informe seu nome de analista');
        $('#analyst-name').val(analystName);
        SetValue('analyst', analystName);
    } else {
        $('#analyst-name').val(analystName);
    }
}

function SetValue(key, value) {
    sessionStorage.setItem(key, value);
}

function GetValue(key) {
    const value = sessionStorage.getItem(key);
    return value;
}

function SendRequest(method, url, responseType, params, callbackSuccess, callbackError) {
    if (!url || url.length == 0)
        return;

    axios({
        url: url,
        method: (!method ? 'GET' : method),
        responseType: (!responseType ? 'JSON' : responseType),
        params: (!params ? '' : params)
    }).then(function (response) {
        if (callbackSuccess)
            return callbackSuccess(response)
    }).catch(function (error) {
        if (callbackError)
            return callbackError(error.data, error.status)
    })
}

function CreateComboAjax(elemento, url) {
    let $el = $(`#${elemento}`);
    $el.empty();
    $.ajax({
        dataType: 'json',
        method: 'GET',
        url: url,
        success: (data) => {
            for (let item of data) {
                $el.append('<option value="' + item.value + '">' + item.text + '</option>');
            }
            $el.val($el).trigger("change");
        }
    })
}