
$(document).ready(function () {
    var token = $('#jwtToken').val();
    var bearerToken = 'Bearer ' + token;
    var baseUrl = $('#ApiBaseUrl').val();

    $('#actionLoader').hide();

    function clearForm() {
        $("#newPassword").val('');
        $("#confirmPassword").val('');
        $("#oldPassword").val('');
    }

    $('#changePasswordForm').submit(function (e) {
        e.preventDefault();
        const oldPassword = $("#oldPassword").val();
        const newPassword = $("#newPassword").val();
        const confirmPassword = $("#confirmPassword").val();

        if (newPassword.length < 8) {
            swal("New Password length cannot be less then 8");
            return false
        }

        if (newPassword != confirmPassword) {
            swal("Password doesn't match");
            $("#newPassword").val('');
            $("#confirmPassword").val('');
            return false
        }

        const data = {
            "OldPassword": oldPassword,
            "NewPassword": newPassword,
            "ConfirmPassword": confirmPassword,
        }

        var applicationUrl = baseUrl + 'Account/change-password';
        $.ajax({
            headers: {
                'Authorization': bearerToken
            },
            url: applicationUrl,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (data) {
                if (data.succeeded) {
                    swal({
                        title: "Successful!",
                        text: data.message,
                        icon: "success",
                        closeOnClickOutside: false,
                        closeOnEsc: false
                    })
                    .then(() => {
                        clearForm();
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
                }
            },
            error: function (xhr) {
                swal("An error ocured");
            }
        });

        return false
    });   
});

$(document).on({
    ajaxStart: function () {
        $('#cover-spin').show(0);
    },
    ajaxStop: function () {
        $('#cover-spin').hide();
    }


});