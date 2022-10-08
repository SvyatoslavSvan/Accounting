function sendForm(formId, url, elementToRemoveId) {
    $.ajax({
        url: url,
        type: 'POST',
        dataType: 'html',
        data: $("#" + formId).serialize(),
        success: function (response) {
            insertResponse(url, response, elementToRemoveId);
        },
        error: function (response) {
            alert('Error while sending form')
        }
    });
}

function insertResponse(url, response, elementToRemoveId) {
    if (url == '/Document/AddEmployeeToDocument') {
        $('#addedEmployeesTbody').append(response);
        $('#' + elementToRemoveId).remove();
    }
    if (url == '/Document/AddAccrualToEmployee') {
        $('#' + elementToRemoveId).empty();
        $('#' + elementToRemoveId).append(response);
        getSumOfAccruals();
    }
    if (url == '/Document/DeleteEmployeeFromDocument') {
        $('#' + elementToRemoveId).remove();
        getSumOfAccruals();
        $('#chooseEmployeeUl').append(response);
    }
    if (url == '/Document/UpdateAccrual') {
        getSumOfAccruals();
        $('#' + elementToRemoveId).html(response);
    }
}
function getSumOfAccruals() {
    $.ajax({
        url: '/Document/GetSumOfAccruals',
        method: 'get', 
        success: function (response) {
            $('#sumAccrual').html(response);
        },
        error: function (response) {
            
        }
    });
}