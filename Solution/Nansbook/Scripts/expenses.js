var pgTopic = "EXPENSE";
$(document).ready(function () {

    //add btn, reset fields in modal
    $('#btn_add').click(function () {
        $('#frmManage input[type="text"]').val('');
        $('#frmManage select').val('0');
        
        $('#hid_id').val('');
        $('#title_add').text('ADD ' + pgTopic);
        $('#btn_update_modal').hide();
        $('#btn_add_modal').show();
        $('#frmManage').attr('action', 'add' + pgTopic);
    });

    //edit btn, fill info in modal
    $('.edit_row').click(function () {
        $('#hid_id').val($(this).attr('data-id'));
        $('#sel_parent_cat').val($(this).attr('data-parentid'));
        $('#txt_name').val($(this).attr('data-name'));
        $('#txt_description').val($(this).attr('data-description'));
        $('#txt_price').val($(this).attr('data-price'));
        
        $('#title_add').text('UPDATE ' + pgTopic);
        $('#btn_update_modal').show();
        $('#btn_add_modal').hide();
        $('#frmManage').attr('action', 'update' + pgTopic);
    });

    //add/update button in modal
    $('#btn_add_modal, #btn_update_modal').click(function () {
        var name = $('#txt_name');
        var desc = $('#txt_description');
        var errMsg = $('#errMsg');
        var validated = true;

        if ($.trim(name.val()) == '') {
            validated = false;
            errMsg.text('Please provide a name.');
            name.focus();
        }
        else if ($.trim(desc.val()) == '') {
            validated = false;
            errMsg.text('Please provide a description.');
            desc.focus();
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
        $('#frmManage').attr('action', 'del' + pgTopic);

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
        $("#txt_name").focus();
    })
});