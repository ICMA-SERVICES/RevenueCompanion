
$(document).ready(function () {
    $('#actionLoader').hide();
    var that = this;
    //domainName = document.querySelector("#domainNameId").value;

    $('#btnLogin').on('click', function () {
        $('#btnLogin').attr('disabled', 'disabled');
        var isValid = true;
        let email = document.getElementById("Email");
        let password = document.getElementById("Password");
        const isValidEmail = email.checkValidity();
        const isValidPassword = password.checkValidity();

        if (isValidEmail == false) {
            $('#Email').css('border-color', 'Red');
            $('#Email').focus();
            $('#btnLogin').removeAttr('disabled');
           // ShowMessagePopup("Oops!", "Please ensure you fill the email field correctly", "warning");

            return false;
        }
        else if (isValidPassword == false) {
            $('#Password').css('border-color', 'Red');
            ($('#Password').focus());
            $('#btnLogin').removeAttr('disabled');
           // ShowMessagePopup("Oops!", "Please ensure you fill the password field correctly", "warning");


            return false;
        }
        else {
           // $('#ibox1').children('.ibox-content').toggleClass('sk-loading');
            document.forms["LoginForm"].submit();
        }
       
    })


    $('#btnAddPassword').on('click', function () {
        $('#btnAddPassword').attr('disabled', 'disabled');
        var isValid = true;
        let confirmPassword = document.getElementById("ConfirmPassword").value;
        let password = document.getElementById("Password").value;

        if (password == null || password == "") {
            $('#Password').css('border-color', 'Red');
            $('#Password').focus();
            $('#btnAddPassword').removeAttr('disabled');
           // ShowMessagePopup("Oops!", "Please ensure you fill the email field correctly", "warning")
            return false;
        }
        if (confirmPassword == null || confirmPassword == "") {
            $('#ConfirmPassword').css('border-color', 'Red');
            $('#ConfirmPassword').focus();
            $('#btnAddPassword').removeAttr('disabled');
           // ShowMessagePopup("Oops!", "Please ensure you fill the email field correctly", "warning")
            return false;
        }
        if (password != confirmPassword) {
            
            $('#btnAddPassword').removeAttr('disabled');
           ShowMessagePopup("Oops!", "Password and Confirm Password does not match", "warning")
           return false;
        }
        else {
           // $('#ibox1').children('.ibox-content').toggleClass('sk-loading');
            document.forms["AddPasswordForm"].submit();
        }
       
    })




});

