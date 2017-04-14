$(document).ready(function () {

    //validation
    $('#frmSignup').submit(function (e) {
        var fname = $("[name='FirstName']");
        var lname = $("[name='LastName']");
        var email = $("[name='Email']");
        var pass1 = $("[name='Password']");
        var pass2 = $("[name='Password2']");
        var bValid = true;

        if ($.trim(fname.val()).length < 1) {
            $('#msgBox').html('<span class="red bold">Please provide your first name.</span>');
            fname.focus();
            bValid = false;
        }
        else if ($.trim(lname.val()).length < 1) {
            $('#msgBox').html('<span class="red bold">Please provide your last name.</span>');
            lname.focus();
            bValid = false;
        }
        else if ($.trim(email.val()).length < 1) {
            $('#msgBox').html('<span class="red bold">Please provide your email address.</span>');
            email.focus();
            bValid = false;
        }
        else if ($.trim(pass1.val()).length < 1) {
            $('#msgBox').html('<span class="red bold">Please provide a password.</span>');
            pass1.focus();
            bValid = false;
        }
        else if ($.trim(pass2.val()).length < 1) {
            $('#msgBox').html('<span class="red bold">Please provide confirm your password.</span>');
            pass2.focus();
            bValid = false;
        }
        else if ($.trim(pass1.val()) != $.trim(pass2.val())) {
            $('#msgBox').html('<span class="red bold">Passwords do not match! Please re-enter passwords.</span>');
            pass1.text = '';
            pass2.text = '';
            pass1.focus();
            bValid = false;
        }

        //prevent form submission
        if (!bValid) {
            e.preventDefault();
            return false;
        }

        /*
        console.log('fname: ' + fname.val());
        console.log('lname: ' + lname.val());
        console.log('email: ' + email.val());
        console.log('pass1: ' + pass1.val());
        console.log('pass2: ' + pass2.val());
        
        e.preventDefault();
        return false;
        */
    });

    //set focus
    $("[name='FirstName']").focus();
});