$(document).ready(function () {
    
    //add btn, before modal opens, reset fields in modal
    $('#btn_add').click(function () {
        $('#frmManage').trigger("reset");

        $('#title_header').text('Add ' + $('#title_header').text().replace( $('#title_header').text().split(' ')[0], ''));
        $('#btn_update_modal').hide();
        $('#btn_add_modal').show();
        $('#frmManage').attr('action', '../../salary/add');
    });

    //edit btn, before modal opens, fill info in modal
    $('.edit_row').click(function () {
        var btn = $(this);

        //fill data in fields
        $('#hidId').val($(this).attr('data-id'));
        $('#modal_storename').text(btn.attr('data-storename'));
        $('#selStore').val(btn.attr('data-userstoreid'));
        $('#txtAmount').val(Number(btn.attr('data-amount')).toFixed(2));
        $('#txtExpDate').val(btn.attr('data-expdate'));

        $('#title_header').text('Update ' + $('#title_header').text().replace($('#title_header').text().split(' ')[0], ''));
        $('#btn_update_modal').show();
        $('#btn_add_modal').hide();
        $('#frmManage').attr('action', '../../salary/update');
    });

    //add/update button in modal
    $('.manageBtn').click(function (e) {
        var errMsg = $('#errMsg');
        var validated = true;
        var oStore = $('#selStore');
        var oAmt = $('#txtAmount');
        
        //validate inputs
        if (oStore.val() === '0') {
            validated = false;
            errMsg.text('Store is required.');
            oStore.select();
        }
        else if ($.trim(oAmt.val()) === '') {
            validated = false;
            errMsg.text('Amount is required.');
            oAmt.select();
        }       
        else if (isNaN(oAmt.val())) {
            validated = false;
            errMsg.text('Amount must be numeric.');
            oAmt.select();
        }
                
        if (validated) {

            //loading sign
            ShowLoading();

            //wait, then submit form
            setTimeout(function () {
                $('#frmManage').submit();
            }, 1000);
        }

        e.preventDefault();
        return false;

    });

    //delete btn, before modal opens, prepare vars for deletion
    $('.rem_row').click(function () {
        
        //get id to delete
        $('#hidId').val($(this).attr('data-id'));

        //set form action
        $('#frmManage').attr('action', '../../salary/delete');

        //set title for modal
        $('.del_name').text($(this).attr('data-name'));
    });

    //proceed btn in modal, confirm delete and submit form
    $('#del_confirm_proceed').click(function () {

        ShowLoading();
        
        //wait, then submit form
        setTimeout(function () {
            $('#frmManage').submit();
        }, 1000);
    });

    //modal is open
    $('#modal_manage').on('shown.bs.modal', function () {
        $('#txtStoreCommPct').focus();
    });

    //datepicker
    $('.datepicker').datepicker();
});