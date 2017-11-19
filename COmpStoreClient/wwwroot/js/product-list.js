$(document).ready(function () {
    $('.add-to-cart').on('click', function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/Cart/AddToCart',
            data: { productId: id },
            type: 'post',
            success: function (data) {
                if (data) {
                    $('#shop-cart').css('color', '#FE980F');
                    alert('Success');
                } else {
                    console.log('error')
                }
            },
            error: function () {
                console.log('error');
            }
        })
    })
})