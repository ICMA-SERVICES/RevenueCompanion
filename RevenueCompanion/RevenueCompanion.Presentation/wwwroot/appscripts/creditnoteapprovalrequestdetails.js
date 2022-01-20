
$(function () {

    var baseUrl = $('#ApiBaseUrl').val();
    var creditNoteRequestId = $('#creditNoteRequestId').val();
    var token = $('#jwtToken').val();
    var bearerToken = 'Bearer ' + token;

    $('#btn_approve').on('click', function () {
        $('#btn_approve').attr('disabled', 'disabled');
        let reason = document.getElementById("approvalReason").value;

        if (reason == "" || reason == null) {
            $('#approvalReason').css('border-color', 'Red');
            $('#approvalReason').focus();
            $('#btn_approve').removeAttr('disabled');
            ShowMessagePopup("Oops!", "Please enter a reason for this descision", "warning");

            return false;
        }
        ApproveCreditNote(reason);
    });
    $('#btn_disapprove').on('click', function () {
        $('#btn_disapprove').attr('disabled', 'disabled');
        let reason = document.getElementById("disapprovalReason").value;

        if (reason == "" || reason == null) {
            $('#disapprovalReason').css('border-color', 'Red');
            $('#disapprovalReason').focus();
            $('#btn_disapprove').removeAttr('disabled');
            ShowMessagePopup("Oops!", "Please enter a reason for this descision", "warning");

            return false;
        }
        DisapproveCreditNote(reason);
    });

    function ApproveCreditNote(reason) {

        var approvalRequestObj = {
            CreditRequestId: parseInt(creditNoteRequestId),
            Comment: reason,
            IsApproved: true,
        };



        var applicationUrl = baseUrl + 'v1.0/CreditNote/approve';
        $.ajax({
            headers: {
                'Authorization': bearerToken
            },
            url: applicationUrl,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(approvalRequestObj),
            success: function (data) {
                if (data.responseCode == "00") {
                    swal({
                        title: "Successful!",
                        text: "Approval descision recorded succesfully",
                        icon: "success",
                        closeOnClickOutside: false,
                        closeOnEsc: false
                    })
                        .then(() => {
                            window.history.go(-1);
                        });
                }

                else {
                    swal({
                        title: "Failed!",
                        text: data.message,
                        icon: "warning",
                        closeOnClickOutside: false,
                        closeOnEsc: false
                    })
                        .then(() => {
                            window.location.reload();
                        });
                }

            },
            error: function (xhr) {
                window.location.reload();
                //alert('Woow something went wrong');
            }
        });
    }

    function DisapproveCreditNote(reason) {

        var disapprovalRequestObj = {
            CreditRequestId: parseInt(creditNoteRequestId),
            Comment: reason,
        };



        var applicationUrl = baseUrl + 'v1.0/CreditNote/disapprove';
        $.ajax({
            headers: {
                'Authorization': bearerToken
            },
            url: applicationUrl,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(disapprovalRequestObj),
            success: function (data) {
                if (data.responseCode == "00") {
                    swal({
                        title: "Successful!",
                        text: "Dis-Approval descision recorded succesfully",
                        icon: "success",
                        closeOnClickOutside: false,
                        closeOnEsc: false
                    })
                        .then(() => {
                            window.history.go(-1);
                        });
                }

                else {
                    swal({
                        title: "Failed!",
                        text: data.message,
                        icon: "warning",
                        closeOnClickOutside: false,
                        closeOnEsc: false
                    })
                        .then(() => {
                            window.location.reload();
                        });
                }

            },
            error: function (xhr) {
                window.location.reload();
                //alert('Woow something went wrong');
            }
        });
    }
});

$(document).on({
    ajaxStart: function () {
        $('#cover-spin').show(0);
    },
    ajaxStop: function () {
        $('#cover-spin').hide(0);
    }
});
