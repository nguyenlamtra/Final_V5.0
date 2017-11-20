$(document).ready(function () {

    // Set the checkbox is checked when user select a row.
    $('#content_table tbody tr').click(function () {
        //$('#btn_uncheck').hide();
        //$(this).siblings().removeClass('active');
        $(this).toggleClass('active');
        //if ($(this).hasClass('active')) {
        //    $('#btn_delete').removeClass('disabled');
        //} else {
        //    $('#btn_delete').addClass('disabled');
        //}

        var numberOfRowSelected = $('#content_table tr.active').length;

        if (numberOfRowSelected > 0) {
            $('#btn_delete').removeClass('disabled');
            $('#btn_uncheck').show();
        }
        else {
            $('#btn_delete').addClass('disabled');
            $('#btn_uncheck').hide();
        }
    });

    // Unselect all selected rows
    $('#btn_uncheck').click(function () {
        $('#content_table tbody tr.active').each(function () {
            $(this).removeClass('active');
        });
        $('#btn_delete').addClass('disabled');
        $(this).hide();
    });


    $('.only-number').keypress(function () {
        return event.charCode >= 48 && event.charCode <= 57;
    })
});
