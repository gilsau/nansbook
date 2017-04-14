$(document).ready(function () {
    
    //click radio buttons (product/service)
    $('[name="rad_prodsvc"]').click(function () {
        var showList = $(this).val() == 'prod' ? $('#divProd') : $('#divSvc');
        var hideList = $(this).val() == 'prod' ? $('#divSvc') : $('#divProd');

        $('#selProduct,#selService').val('0');
        hideList.hide();
        showList.removeClass('hide').show();
    });

    //when product/service is selected
    $('#selService,#selProduct').change(function () {
        $('#txtPercentage').trigger('keyup');
    });

    //convert percentage to amount
    $('#txtPercentage').keyup(function () {
        var oPct = $(this);
        var oAmt = $('#txtAmount');
        var oProd = $('#selProduct');
        var oSvc = $('#selService');

        //only if percentage is numeric
        if (!isNaN(oPct.val())) {

            //only if product or service is selected
            if(oProd.val() > 0 || oSvc.val() > 0){

                //price of product or service selected
                var price = oProd.val() > 0 ? $("#selProduct option:selected").attr('data-price') : (oSvc.val() > 0 ? $("#selService option:selected").attr('data-price') : 0);
                
                //calculate amount
                var calc = (oPct.val() / 100) * price;
                oAmt.val(calc.toFixed(2));
            }
        }
    });

    //add btn, before modal opens, reset fields in modal
    $('#btn_add').click(function () {
        $('#frmManage').trigger("reset");
        $('#divProd').hide();
        $('#divSvc').hide();

        $('#title_header').text('Add ' + $('#title_header').text().replace( $('#title_header').text().split(' ')[0], ''));
        $('#btn_update_modal').hide();
        $('#btn_add_modal').show();
        $('#frmManage').attr('action', '../../discount/add');
    });

    //edit btn, before modal opens, fill info in modal
    $('.edit_row').click(function () {
        var btn = $(this);
        var oProd = $('#selProduct');
        var oSvc = $('#selService');
        var oRadProdSvc = $('[name="rad_prodsvc"]');

        //fill data in fields
        $('#hidId').val($(this).attr('data-id'));
        $('#txtName').val(btn.attr('data-name'));
        $('#txtAmount').val(Number(btn.attr('data-amount')).toFixed(2));
        $('#txtPercentage').val(Number(btn.attr('data-pct')));
        $('#txtDescription').val(btn.attr('data-description'));

        if (btn.attr('data-parentid') != '') {
            $('#selParent').val(btn.attr('data-parentid'));
        }

        var expDate = new Date(btn.attr('data-expdate'));
        $('#txtExpDate').val(expDate.getMonth()+1 + '/' + expDate.getDate() + '/' + expDate.getFullYear());

        //populate product/service selected
        if (btn.attr('data-productid') != '') {
            oRadProdSvc[0].click();
            oProd.val(btn.attr('data-productid'));
        }
        else if (btn.attr('data-serviceid') != '') {
            oSvc.val(btn.attr('data-serviceid'));
            oRadProdSvc[1].click();
        }
        
        $('#title_header').text('Update ' + $('#title_header').text().replace($('#title_header').text().split(' ')[0], ''));
        $('#btn_update_modal').show();
        $('#btn_add_modal').hide();
        $('#frmManage').attr('action', '../../discount/update');
    });

    //add/update button in modal
    $('.manageBtn').click(function (e) {
        var errMsg = $('#errMsg');
        var validated = true;
        var oProdSvc = $('[name="rad_prodsvc"]');
        var oProd = $('#selProduct');
        var oSvc = $('#selService');
        var oName = $('#txtName');
        var oPct = $('#txtPercentage');
        var oAmt = $('#txtAmount');
        var oExpDate = $('#txtExpDate');

        //validate inputs
        if ($('[name="rad_prodsvc"]:checked').length === 0 || (oProd.val() === '0' && oSvc.val() === '0')) {
            validated = false;
            errMsg.text('Must select a Product or Service.');
            oProdSvc.select();
        }
        else if ($.trim(oName.val()) === '') {
            validated = false;
            errMsg.text('Name of Discount is required.');
            oName.select();
        }       
        else if ($.trim(oPct.val()) === '') {
            validated = false;
            errMsg.text('Percentage is required.');
            oPct.select();
        }
        else if ($.trim(oPct.val()) === '') {
            validated = false;
            errMsg.text('Percentage is required.');
            oPct.select();
        }
        else if ($.trim(oExpDate.val()) === '') {
            validated = false;
            errMsg.text('Expiration Date is required.');
            oExpDate.select();
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
        $('#frmManage').attr('action', '../../discount/delete');

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

    //intialize datepicker
    $('.datepicker').datepicker();
});