$(document).ready(function () {

    //fields sent from MVC Controller (server-side), parse JSON string to objects
    htmlElementsJSONStr = htmlElementsJSONStr.replace(/&quot;/g, '"').replace(/&#39;/g, "'");
    htmlElementsJSON = JSON.parse(htmlElementsJSONStr).Data;

    //initialize datepickers
    $('.datepicker').datepicker();

    //add btn, before modal opens, reset fields in modal
    $('#btn_add').click(function () {
        $('#frmManage input[type="text"]').val('');
        $('#frmManage select').val('0');
        
        //TODO: empty all fields
        $('#frmManage').trigger("reset");

        $('#title_header').text('Add ' + $('#title_header').text().replace( $('#title_header').text().split(' ')[0], ''));
        $('#btn_update_modal').hide();
        $('#btn_add_modal').show();
        $('#frmManage').attr('action', '../category/add');
    });

    //edit btn, before modal opens, fill info in modal
    $('.edit_row').click(function () {
        var btnEdit = $(this);

        //fill data in fields
        for (var idx in htmlElementsJSON)
        {
            var elem = htmlElementsJSON[idx];

            //only elements that have an id
            if (elem.ElemId && elem.ElemName) {
                var elemId = elem.ElemId.toLowerCase();
                var data = btnEdit.attr('data-' + elem.ElemName.toLowerCase());
                var svcProdLstId = (elem.ElemName == "ServiceId" && data != '') ? 'sel_service' : ((elem.ElemName == "ProductId" && data != '') ? 'sel_product' : null);

                if (elem.ValidationPattern === 'money') {
                    data = new Number(data).toFixed(2);
                }

                if (elem.ValidationPattern === 'datetime') {
                    var temp = new Date(data);
                    data = (temp.getMonth() + 1) + '/' + temp.getDate() + '/' + temp.getFullYear();
                }

                //show product/service list
                if (svcProdLstId != null) {
                    $('#sel_product').hide();
                    $('#sel_service').hide();
                    $('#' + svcProdLstId).show();
                }

                //fill data in html control
                $('#' + elemId).val(data);
            }
        }

        $('#title_header').text('Update ' + $('#title_header').text().replace($('#title_header').text().split(' ')[0], ''));
        $('#btn_update_modal').show();
        $('#btn_add_modal').hide();
        $('#frmManage').attr('action', '../category/update');
    });

    //add/update button in modal
    $('.manageBtn').click(function (e) {
        var errMsg = $('#errMsg');
        var validated = true;
        
        //loop thru fields, and validate req fields
        for (var idx in htmlElementsJSON) {
            var elem = htmlElementsJSON[idx];

            //validate ONLY required fields
            if (elem.Required) {
                var elemId = elem.ElemId.toLowerCase();
                var ctrl = $('#' + elemId);
                var data = ctrl.val();

                //check for empty field
                if ($.trim(ctrl.val()) === '') {
                    validated = false;
                    errMsg.text(elem.Label + ' is required.');
                    ctrl.select();
                    break;
                }

                //check for numeric field
                else if (elem.ValidationPattern === 'money') {
                    if (isNaN(ctrl.val())) {
                        validated = false;
                        errMsg.text('Please provide a numeric value for ' + elem.Label + '.');
                        ctrl.select();
                        break;
                    }
                }

                //check for valid date
                else if (elem.ValidationPattern === 'datetime') {
                    if (!(/^\d{2}\/\d{2}\/\d{4}$/.test(ctrl.val()))){
                        validated = false;
                        errMsg.text('Please provide a valid date for ' + elem.Label + '.');
                        ctrl.select();
                        break;
                    }
                }
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
        var hid_id = $('#hid_id');

        //clear form field
        hid_id.val('');

        //get id to delete
        hid_id.val($(this).attr('data-id'));

        //set form action
        $('#frmManage').attr('action', '../category/delete');

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
        $(':input:enabled:visible:first').focus();
    })

    //OnClick event for Service/Product radio buttons
    $('#rad_service,#rad_product').click(function () {
        var btn = $(this);
        var targetshow = $(btn.attr('data-targetshow'));
        var targethide = $(btn.attr('data-targethide'));

        console.log('targetshow');
        console.log(targetshow);

        targethide.parent().css('display', 'none').addClass('hide');
        targetshow.parent().css('display', 'block').removeClass('hide');
    });
});