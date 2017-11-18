$(document).ready(function () {

    $('.image-editor').cropit({
        exportZoom: 1,
        width: 268,
        height: 249,
    });


    $('.rotate-cw').click(function () {
        $('.image-editor').cropit('rotateCW');
    });
    $('.rotate-ccw').click(function () {
        $('.image-editor').cropit('rotateCCW');
    });

    $('.export').click(function () {
        var imageData = $('.image-editor').cropit('export');
        $('#imageContainer .image-item-block img.selected').attr('src', imageData);
        $('#imageContainer .image-item-block img.selected').siblings('input').attr('value', imageData);
    });

    $(document).ready(function () {
        // Delete selected rows
        $('#btn_delete').click(function () {
            bootbox.confirm({
                message: "Thao tác này sẽ xóa các sản phẩm và dữ liệu có liên quan và không thể khôi phục, bạn có chắc chắn muốn xóa?",
                buttons: {
                    confirm: {
                        label: 'Có',
                        className: 'btn-danger'
                    },
                    cancel: {
                        label: 'Không',
                        className: 'btn-default'
                    }
                },
                callback: function (result) {
                    if (result) {
                        var ids = [];
                        $('#content_table tbody tr.active').each(function () {
                            ids.push($(this).data('id'));
                        });
                        $.ajax({
                            url: '/AdminProduct/Delete',
                            type: 'post',
                            data: { 'ids': ids },
                            success: function (result) {
                                if (result === true) {
                                    $('#content_table tbody tr.active').each(function () {
                                        $(this).hide(600).promise().done(function () {
                                            $(this).remove();
                                        });
                                    });
                                    $('#btn_delete').addClass('disabled');
                                    $('#btn_uncheck').hide();
                                }
                                else {
                                    bootbox.alert("Có lỗi xảy ra. Liên hệ quản trị viên để biết thêm chi tiết!");
                                }
                            }
                        });
                    }
                }
            });
        });
    });
});