$(document).ready(function () {

    //set main container's height
    $('body > div > div.container-fluid').height($(window).height() - $('#divFooter').height() - 85);

    //catch menu click, display loading symbol, then navigate to destination url
    $('[data-url]').click(function () {
        var url = $(this).attr('data-url');
        
        ShowLoading();

        //wait a couple seconds, then navigate to url
        setTimeout(function () {
            window.location = url;
        }, 1000);
    });

    //switch stores
    $('.switchstore').click(function () {
        var storeId = $(this).attr('data-storeid');

        ShowLoading();
        
        //wait a couple seconds, then navigate to url
        setTimeout(function () {
            $.get("../../home/switchstore?storeId=" + storeId, function (data) {
                location.reload();
            });
        }, 1000);
    });
});

function HideLoading()
{
    $('#mainLoader').hide();
    $('#backDimmer').hide();
}

function ShowLoading() {
    var loadSign = $('#mainLoader');
    var backDimmer = $('#backDimmer');
    var wsh = window.innerHeight;
    var wsw = window.innerWidth;

    //close any modals opened
    $('.modal').modal('hide');

    //position loading sign
    loadSign.css('top', (wsh / 2) - (loadSign.height() / 2) - 50 + $(window).scrollTop());
    loadSign.css('left', (wsw / 2) - (loadSign.width() / 2) - 25);

    //show dimmed background and loading sign
    backDimmer.show();
    loadSign.show();
}