function sendForm(formId, url) {
    $.ajax({
        url: url,
        type: 'POST',
        dataType: 'html',
        data: $("#" + formId).serialize(),
        success: function (response) {
            $('#addedEmployeesTbody').append(response);
        },
        error: function (response) {
            alert('Error while sending form')
        }
    });
}
