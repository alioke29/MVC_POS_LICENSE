var color_input_text = '#defafc';
$(document).ready(function () {
    //function auto numeric
    $(".numeric").autoNumeric("init", {
        aSep: ',',
        aDec: '.',
        aPad: false,
    });
    $('.select2').select2();
});