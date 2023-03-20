function sendForm(formId, url, elementToRemoveId, type = 'POST') {
    $.ajax({
        url: url,
        type: type,
        dataType: 'html',
        data: $("#" + formId).serialize(),
        success: function (response) {
            insertResponse(url, response, elementToRemoveId, formId);
            return response;
        },
        error: function (response) {
            alert('Error while sending form')
        }
    });
}

function appendEmployee(elementToRemoveId, response) {
    $('#addedEmployeesTbody').append(response);
}

function removeEmployee(elementToRemoveId, response) {
    $('#' + elementToRemoveId).remove();
    getSumOfAccruals();
}

function insertResponse(url, response, elementToRemoveId, formId) {
    if (url == '/Document/AddEmployee') {
        appendEmployee(elementToRemoveId, response);
    }
    if (url == '/Payout/CreatePayout') {
        getSumOfAccruals();
        let formTr = document.getElementById(elementToRemoveId);
        while (formTr.firstChild) {
            formTr.removeChild(formTr.firstChild)
        }
        formTr.innerHTML += response;
    }
    if (url == '/Document/DeleteEmployee') {
        removeEmployee(elementToRemoveId, response);
    }
    if (url == '/Payout/Update') {
        getSumOfAccruals();
        var ammountInput = document.getElementById(elementToRemoveId);
        ammountInput.value = response;
    }
    if (url == '/Payout/Delete') {
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
    if (url == '/Document/GetSearchEmployeesAddToDocument' || url == '/Document/GetEmployeesAddToDocument') {
        $('#' + elementToRemoveId).empty();
        $('#' + elementToRemoveId).append(response);
    }
    if (url == '/Group/Create') {
        $('#' + elementToRemoveId).append(response);
    }
    if (url == '/Group/Delete') {
        $('#' + elementToRemoveId).remove();
    }
    
}
function addEmployeeToDocument(formId, elementToRemove, inputPayoutId) {
    sendForm(formId, '/Document/AddEmployee', elementToRemove);
    var modal = document.getElementById('addEmployeeToDocumentModal');
    var modalInstance = bootstrap.Modal.getInstance(modal);
    modalInstance.hide();
}


function getSumOfAccruals() {
    $.ajax({
        url: '/Payout/GetSumOfPayouts',
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
        url: `/Payout/CreatePayout/${employeeId}`,
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

function onAmmountInputChanged(updateAccrualFormId) {
    sendForm(updateAccrualFormId, '/Payout/Update');
}
function insertFoundDocuments(response, TbodyId) {
    $('#' + TbodyId).html(response);
}

function createEmployee(createEmployeeFormId) {
    sendForm(createEmployeeFormId, '/Employee/Create', 'employeesUl');
}

function onDocumentTypeInputChange() {
    var documentTypeInput = document.getElementById('documentTypeInput');
    if (documentTypeInput.value == 1) {
        var documentLabel = document.getElementById('documentLabel');
        documentLabel.classList.remove('text-succes');
        documentLabel.classList.add('text-danger');
        documentLabel.innerHTML = 'Створити документ утримання';
    }
    else {
        var documentLabel = document.getElementById('documentLabel');
        documentLabel.classList.remove('text-danger');
        documentLabel.classList.add('text-succes');
        documentLabel.innerHTML = 'Створити документ нарахування';
    }
    
}    
function submitSearchEmployeesAddToDocumentForm() {
    sendForm('searchEmployeesAddToDocumentfrm', '/Document/GetSearchEmployeesAddToDocument', 'chooseEmployeeUl', 'GET');
}
function getEmployeesAddToDocument() {
    sendForm('searchEmployeesAddToDocumentfrm', '/Document/GetEmployeesAddToDocument', 'chooseEmployeeUl', 'GET');
}
function setFocus(elementId) {
    document.getElementById(elementId).focus();
}