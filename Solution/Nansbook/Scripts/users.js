var pgTopic = "USER";
$(document).ready(function () {

    //add btn, reset fields in modal
    $('#btn_add').click(function () {
        $('#frmManage input[type="text"]').val('');
        $('#frmManage select').val('0');
        $('#img-photo').attr('src', '/profilepics/default.jpg');

        $('#hid_id').val('');
        $('#title_add').text('ADD '+pgTopic);
        $('#btn_update_modal').hide();
        $('#btn_add_modal').show();
        $('#frmManage').attr('action', 'add'+pgTopic);
    });

    //edit btn, fill info in modal
    $('.edit_row').click(function () {
        $('#hid_id').val($(this).attr('data-id'));
        $('#txt_username').val($(this).attr('data-username'));
        $('#txt_firstname').val($(this).attr('data-firstname'));
        $('#txt_middlename').val($(this).attr('data-middlename'));
        $('#txt_lastname').val($(this).attr('data-lastname'));
        $('#txt_nickname').val($(this).attr('data-nickname'));
        $('#txt_address1').val($(this).attr('data-address1'));
        $('#txt_address2').val($(this).attr('data-address2'));
        $('#txt_city').val($(this).attr('data-city'));
        $('#sel_state').val($(this).attr('data-state'));
        $('#txt_homephone').val($(this).attr('data-homephone'));
        $('#txt_mobilephone').val($(this).attr('data-mobilephone'));
        $('#txt_socialsecurity').val($(this).attr('data-socialsecurity'));
        $('#sel_role').val($(this).attr('data-role'));
        $('#img_photo').attr('src', '../profilepics/' + $(this).attr('data-photo'));

        $('#title_add').text('UPDATE '+pgTopic);
        $('#btn_update_modal').show();
        $('#btn_add_modal').hide();
        $('#frmManage').attr('action', 'update'+pgTopic);
    });

    //add/update button in modal
    $('#btn_add_modal, #btn_update_modal').click(function () {
        var username = $('#txt_username');
        var firstname = $('#txt_firstname');
        var lastname = $('#txt_lastname');
        var stateId = $('#sel_state');
        var role = $('#sel_role');
        var errMsg = $('#errMsg');
        var validated = true;

        if ($.trim(username.val()) == '')
        {
            validated = false;
            errMsg.text('Please provide a username.');
            username.focus();
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

        
        //wait, then submit form
        if (validated) {
            //loading sign
            $(this).html('<img src="../images/loading_spinner.gif" style="height:20px;" />');

            setInterval(function () {
                $('#frmManage').submit();
            }, 2000);
        }
    });

    //delete multiple records, save id in hidden var (btn opens confirm modal while this code is ran)
    $('#delSelected').click(function () {
        var selected = $("input[name='chkRow']:checked");
        var hid_del = $('#hid_del');

        //clear form field
        hid_del.val('');

        //get all selected checkboxes
        if (selected.length > 0) {
            selected.each(function () {
                hid_del.val(hid_del.val() + ',' + $(this).attr('data-id'));
            });
        }
        if (hid_del.val().length > 0) hid_del.val(hid_del.val().substr(1));
    });

    //delete single record, save id in hidden var (btn opens confirm modal while this code is ran)
    $('.rem_row').click(function () {
        var hid_del = $('#hid_del');

        //clear form field
        hid_del.val('');

        //get all selected checkboxes
        hid_del.val($(this).attr('data-id'));
    });

    //Confirm delete and submit form
    $('#del_confirm_proceed').click(function () {

        //loading sign
        $(this).html('<img src="../images/loading_spinner.gif" style="height:20px;" />');

        //change controller
        $('#frmManage').attr('action', 'del'+pgTopic);

        //wait, then submit form
        setInterval(function () {
            $('#frmManage').submit();
        }, 2000);
    });

    //select all checkboxes
    $("#chkAll").change(function () {
        $("input:checkbox").prop('checked', $(this).prop("checked"));
    });

    //modal is open
    $('#modal_manage').on('shown.bs.modal', function () {
        $("#txt_username").focus();
    })
});