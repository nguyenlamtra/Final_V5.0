$(document).ready(function () {
    var maxImage = 4;
    var currentNumOfImage = $('.image-item-block').length;
    $('#imageEditor').show();
    $('#addImages').click(function () {
        bootbox.alert({
            message: $('#addProductImagesPartial').html()
        });
    });
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
        //imageData = JSON.stringify(imageData);
        //$('#xxx').attr('value', imageData);
    });

    //$('#addMoreImages').click(function () {
    //    var divImageItemBlock = '<div class="col-md-3 image-item-block">' +
    //        '<input name="productImgFiles" type= "hidden" />' +
    //        '<img class="img-thumbnail product-temp-img selected" src="/images/add_file_button.png">' +
    //        '<a class="btn btn-danger remove-img">x</a>'
    //    '</div>';

    //    if (currentNumOfImage < maxImage) {
    //        clearSelectedImg();

    //        $('#imageContainer').append(divImageItemBlock);
    //        currentNumOfImage++;

    //        $('#imageEditor').show();
    //    }
    //});

    $(document).on('click', 'img.product-temp-img', function () {

        clearSelectedImg();

        $(this).addClass('selected');
    });

    //$(document).on('click', 'a.remove-img', function () {
    //    $(this).parent().remove();
    //    currentNumOfImage = $('.image-item-block').length;

    //    if (currentNumOfImage === 0) {
    //        $('#imageEditor').hide();
    //    }
    //});

    function clearSelectedImg() {
        $('#imageContainer .image-item-block img.selected').each(function () {
            $(this).removeClass('selected');
        });
    }

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