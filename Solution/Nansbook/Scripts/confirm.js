$(document).ready(function () {
    $('#frmConfirm').submit(function (e) {

        var ques1 = $('#sel_secQuestion1');
        var ques2 = $('#sel_secQuestion2');
        var ques3 = $('#sel_secQuestion3');
        var ans1 = $('#txt_secAnswer1');
        var ans2 = $('#txt_secAnswer2');
        var ans3 = $('#txt_secAnswer3');
        var errMsg = $('#errMsg');
        var submit = true;

        if (ques1.val() == '0')
        {
            errMsg.text("Please select your first security question.");
            ques1.focus();
            submit = false;
        }
        else if ($.trim(ans1.val()) == '') {
            errMsg.text("Please answer your first security question.");
            ans1.focus();
            submit = false;
        }
        else if (ques2.val() == '0') {
            errMsg.text("Please select your second security question.");
            ques2.focus();
            submit = false;
        }
        else if ($.trim(ans2.val()) == '') {
            errMsg.text("Please answer your second security question.");
            ans2.focus();
            submit = false;
        }
        else if (ques3.val() == '0') {
            errMsg.text("Please select your third security question.");
            ques3.focus();
            submit = false;
        }
        else if ($.trim(ans3.val()) == '') {
            errMsg.text("Please answer your third security question.");
            ans3.focus();
            submit = false;
        }

        if (!submit) {
            e.preventDefault();
            return false;
        }
    });
});