function sendForm(formId, url, elementToRemoveId) {
    $.ajax({
        url: url,
        type: 'POST',
        dataType: 'html',
        data: $("#" + formId).serialize(),
        success: function (response) {
            insertResponse(url, response, elementToRemoveId, formId);
        },
        error: function (response) {
            alert('Error while sending form')
        }
    });
}
function insertResponse(url, response, elementToRemoveId, formId) {
    if (url == '/Document/AddEmployeeToDocument') {
        $('#addedEmployeesTbody').append(response);
        $('#' + elementToRemoveId).remove();
    }
    if (url == '/Document/AddAccrualToEmployee') {
        /*$('#' + elementToRemoveId).append(response);*/
        appendCreatedAccrualResponse(response, true);
        var ammountInput = document.getElementById('ammountInput');
        ammountInput.parentNode.removeChild(ammountInput);
        var addAccrualForm = document.getElementById(formId);
        var newAmmountInput = document.createElement('input');
        newAmmountInput.id = 'ammountInput';
        newAmmountInput.type = 'number';
        newAmmountInput.name = 'Ammount';
        newAmmountInput.className = 'form-control';
        addAccrualForm.appendChild(newAmmountInput);
        getSumOfAccruals();
    }
    if (url == '/Document/DeleteEmployeeFromDocument') {
        $('#' + elementToRemoveId).remove();
        getSumOfAccruals();
        $('#chooseEmployeeUl').append(response);
    }
    if (url == '/Document/UpdateAccrual') {
        getSumOfAccruals();
        $('#' + elementToRemoveId).append(response);
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
function openCreateAccrualModal(employeeId) {
    $.ajax({
        url: `/Document/CreateAccrual/${employeeId}`,
        method: 'get',
        success: function (response) {
            appendCreatedAccrualResponse(response, false);
        },
        error: function (response) {
            alert('Вадим лютин');
            console.log(response);
        }
    });
}
function appendCreatedAccrualResponse(response, addAccrualToUl) {
    if (!addAccrualToUl) {
        $('#createAccrualModalBody').empty();
        $('#createAccrualModalBody').append(response);
    } else {
        $('#accrualUl').append(response);
    }
    
}

