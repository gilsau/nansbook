$(document).ready(function () {

    //update button
    $('#btn_update').click(function () {
        var pass1 = $('#txt_password1');
        var pass2 = $('#txt_password2');
        var firstname = $('#txt_firstname');
        var lastname = $('#txt_lastname');
        var stateId = $('#sel_state');
        var role = $('#sel_role');
        var errMsg = $('#errMsg');
        var validated = true;

        if (pass1.val() != '' || pass2.val() != '') {
            if (pass1.val() == '') {
                validated = false;
                errMsg.text('Please provide a password.');
                pass1.focus();
            }
            else if (pass2.val() == '') {
                validated = false;
                errMsg.text('Please confirm your password.');
                pass2.focus();
            }
            else if (pass1.val() != pass2.val())
            {
                validated = false;
                errMsg.text('Passwords do not match. Try again.');
                pass1.val('');
                pass2.val('');
                pass1.focus();
            }
        }
        else if ($.trim(firstname.val()) == '') {
            validated = false;
            errMsg.text('Please provide a first name.');
            firstname.focus();
        }
        else if ($.trim(lastname.val()) == '') {
            validated = false;
            errMsg.text('Please provide a last name.');
            lastname.focus();
        }
        else if (stateId.val() < 1) {
            validated = false;
            errMsg.text('Please select a state.');
            stateId.focus();
        }
        else if (role.val() == '0') {
            validated = false;
            errMsg.text('Please select a role.');
            role.focus();
        }

        //validate form, then save
        if (validated) {
            //use reg expression, and only keep numbers
            $('#txt_homephone').val($('#txt_homephone').val().replace(/\D/g,''));
            $('#txt_mobilephone').val($('#txt_mobilephone').val().replace(/\D/g, ''));
            $('#txt_socialsec').val($('#txt_socialsec').val().replace(/\D/g, ''));

            ShowLoading();

            //wait, then submit form
            setInterval(function () {
                $('#frmProfile').submit();
            }, 1000);
        }
    });
});