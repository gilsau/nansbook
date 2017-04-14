$(document).ready(function () {
    
    //add btn, before modal opens, reset fields in modal
    $('#btn_add').click(function () {
        $('#frmManage').trigger("reset");

        $('#title_header').text('Add ' + $('#title_header').text().replace( $('#title_header').text().split(' ')[0], ''));
        $('#btn_update_modal').hide();
        $('#btn_add_modal').show();
        $('#frmManage').attr('action', '../../service/add');
    });

    //edit btn, before modal opens, fill info in modal
    $('.edit_row').click(function () {
        var btn = $(this);

        //fill data in fields
        $('#hidId').val($(this).attr('data-id'));
        $('#txtName').val(btn.attr('data-name'));
        $('#txtPrice').val(Number(btn.attr('data-price')).toFixed(2));
        $('#txtDescription').val(btn.attr('data-description'));
        
        if (btn.attr('data-parentid') != '') {
            $('#selParent').val(btn.attr('data-parentid'));
        }

        console.log('title_header1: ' + $('#title_header').text());

        $('#title_header').text('Update ' + $('#title_header').text().replace($('#title_header').text().split(' ')[0], ''));

        console.log('title_header2: ' + $('#title_header').text());


        $('#btn_update_modal').show();
        $('#btn_add_modal').hide();
        $('#frmManage').attr('action', '../service/update');
    });

    //add/update button in modal
    $('.manageBtn').click(function (e) {
        var errMsg = $('#errMsg');
        var validated = true;
        var oName = $('#txtName');
        var oPrice = $('#txtPrice');
        
        //validate inputs
        if ($.trim(oName.val()) === '') {
            validated = false;
            errMsg.text('Name is required.');
            oName.select();
        }       
        else if ($.trim(oPrice.val()) != '') {
            if (isNaN(oPrice.val())) {
                validated = false;
                errMsg.text('Price must be numeric.');
                oPrice.select();
            }

            oPrice.select();
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
        $('#frmManage').attr('action', '../../service/delete');

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
        $('#txtName').focus();
    });
});