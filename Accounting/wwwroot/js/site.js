function sendForm(formId, url, elementToRemoveId, type = 'POST') {
    $.ajax({
        url: url,
        type: type,
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

function appendEmployee(elementToRemoveId, response) {
    $('#addedEmployeesTbody').append(response);
    $('#' + elementToRemoveId).remove();
}
function removeEmployee(elementToRemoveId, response) {
    $('#' + elementToRemoveId).remove();
    getSumOfAccruals();
    $('#chooseEmployeeUl').append(response);
}
function insertResponse(url, response, elementToRemoveId, formId) {
    if (url == '/Document/AddEmployeeToDocument') {
        appendEmployee(elementToRemoveId, response);
    }
    if (url == '/DeducationDocument/AddEmployee') {
        appendEmployee(elementToRemoveId, response);
    }
    if (url == '/Deducation/CreateDeducation') {
        appendCreatedAccrualResponse(response, true);
        getSumOfDeducations();
    }
    if (url == '/Deducation/DeleteDeducation') {
        $('#' + elementToRemoveId).remove();
        getSumOfDeducations();
    }
    if (url == '/Deducation/UpdateDeducation') {
        var ammountInput = document.getElementById(elementToRemoveId);
        ammountInput.value = response;
        getSumOfDeducations();
    }
    if (url == '/Document/AddAccrualToEmployee') {
        /*$('#' + elementToRemoveId).append(response);*/
        appendCreatedAccrualResponse(response, true);
        var ammountInput = document.getElementById('ammountInput');
        ammountInput.parentNode.removeChild(ammountInput);
        var addAccrualForm = document.getElementById(formId);
        var newAmmountInput = document.createElement('input');
        newAmmountInput.id = 'ammountInput';
        newAmmountInput.type = 'text';
        newAmmountInput.name = 'Ammount';
        newAmmountInput.className = 'form-control';
        addAccrualForm.appendChild(newAmmountInput);
        getSumOfAccruals();
    }
    if (url == '/Document/DeleteEmployeeFromDocument') {
        removeEmployee(elementToRemoveId, response);
    }
    if (url == '/DeducationDocument/DeleteEmployee') {
        getSumOfDeducations();
        $('#' + elementToRemoveId).remove();
        $('#chooseEmployeeUl').append(response);
        
    }
    if (url == '/Document/UpdateAccrual') {
        getSumOfAccruals();
        var ammountInput = document.getElementById(elementToRemoveId);
        ammountInput.value = response;
    }
    if (url == '/Document/DeleteAccrual') {
        getSumOfAccruals();
        $('#' + elementToRemoveId).remove();
    }
    if (url == '/Document/Delete') {
        $('#' + elementToRemoveId).remove();
    }
    if (url == '/Document/GetSearch') {
        insertFoundDocuments(response, elementToRemoveId);
    }
    if (url == '/Employee/Create') {
        document.getElementById('createEmployee').reset();
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

function getSumOfDeducations() {
    $.ajax({
        url: '/DeducationDocument/GetSumOfDeducations',
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

function openCreateDeducationModal(employeeId) {
    $.ajax({
        url: `/Deducation/CreateDeducation/${employeeId}`,
        method: 'get',
        success: function (response) {
            appendCreatedDeducationResponse(response, false);
        },
        error: function (response) {
            alert('Вадим лютин');
            console.log(response);
        }
    });
}

function appendCreatedDeducationResponse(response, addAccrualToUl) {
    if (!addAccrualToUl) {
        $('#createDeducationModalBody').empty();
        $('#createDeducationModalBody').append(response);
    } else {
        $('#DeducationUl').append(response);
    }

}

function appendCreatedAccrualResponse(response, addAccrualToUl) {
    if (!addAccrualToUl) {
        $('#createAccrualModalBody').empty();
        $('#createAccrualModalBody').append(response);
    } else {
        $('#accrualUl').append(response);
    }
    
}

function onAmmountInputChanged(updateAccrualFormId) {
    sendForm(updateAccrualFormId, '/Document/UpdateAccrual');
}
function insertFoundDocuments(response, TbodyId) {
    $('#' + TbodyId).html(response);
}

function createEmployee(createEmployeeFormId) {
    sendForm(createEmployeeFormId, '/Employee/Create', 'employeesUl');
}
    

