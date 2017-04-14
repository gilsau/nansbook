$(document).ready(function () {
    console.log('$(document).height(): ' + $(document).height());
    $('body > .container-fluid').height($(document).height() + 300);
});