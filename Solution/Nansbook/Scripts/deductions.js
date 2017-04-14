$(document).ready(function () {

    //add btn, before modal opens, reset fields in modal
    $('#btn_add').click(function () {
        $('#frmManage').trigger("reset");

        $('#title_header').text('Add ' + $('#title_header').text().replace($('#title_header').text().split(' ')[0], ''));
        $('#btn_update_modal').hide();
        $('#btn_add_modal').show();
        $('#frmManage').attr('action', '../../deduction/add');
    });

    //edit btn, before modal opens, fill info in modal
    $('.edit_row').click(function () {
        var btn = $(this);

        //fill data in fields
        $('#hidId').val($(this).attr('data-id'));
        $('#modal_storename').text(btn.attr('data-storename'));
        $('#selStore').val(btn.attr('data-userstoreid'));

        $('#txtFixedAmtPerSvcSale').val(Number(btn.attr('data-fixedamtpersvcsale')).toFixed(2));
        $('#txtPctPerSvcSale').val(btn.attr('data-pctpersvcsale'));
        $('#txtPctTotallSvcSales').val(btn.attr('data-pcttotallsvcsales'));
        $('#txtDailyAmt').val(Number(btn.attr('data-dailyamt')).toFixed(2));
        $('#txtDailyCleaningExp').val(btn.attr('data-dailycleaningexp'));
        $('#txtDailyCleaningExpWkDay').val(btn.attr('data-dailycleaningexpwkday'));
        $('#txtTipProcessingPct').val(btn.attr('data-tipprocessingpct'));

        $('#title_header').text('Update ' + $('#title_header').text().replace($('#title_header').text().split(' ')[0], ''));
        $('#modal_storename').text(btn.attr('data-storename'));
        $('#btn_update_modal').show();
        $('#btn_add_modal').hide();
        $('#frmManage').attr('action', '../../deduction/update');
    });

    //add/update button in modal
    $('.manageBtn').click(function (e) {dfa
        var errMsg = $('#errMsg');
        var validated = true;
        var oStore = $('#selStore');
        var oFAPSS = $('#txtFixedAmtPerSvcSale');
        var oPPSS = $('#txtPctPerSvcSale');
        var oPTASS = $('#txtPctTotAllSvcSales');
        var oDA = $('#txtDailyAmt');
        var oDCE = $('#txtDailyCleaningExp');
        var oDCEWD = $('[name="DailyCleaningExpWkDay"]');

        //validate inputs
        if (oStore.val() === '0') {
            validated = false;
            errMsg.text('Store is required.');
            oStore.select();
        }
        
        //need at least one sales deduction
        else if ($.trim(oFAPSS.val()) === '' && $.trim(oPPSS.val()) === '' && $.trim(oPTASS.val()) === '' && $.trim(oDA.val()) === '')
        {
            validated = false;
            errMsg.text('Service Sales Deduction is required.');
            oFAPSS.click();
            oFAPSS.focus();
        }

        //daily cleaning (if deduction exists, must select weekday)
        else if($.trim(oDCE) != ''){
            if (oDCEWD.length === 0) {
                validated = false;
                errMsg.text('Must select weekday for Daily Cleaning Deduction.');
                oDCEWD.focus();
            }
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
        $('#frmManage').attr('action', '../../deduction/delete');

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
        $('#selStore').focus();
    });

    //radio buttons for txt boxes
    $('.txt50').click(function () {
        $($(this).attr('data-rad')).trigger('click');
    });

    //clear boxes, when radio btns are clicked
    $('#rad1,#rad2,#rad3,#rad4').click(function () {
        $('.txt50').val('');
    });
});