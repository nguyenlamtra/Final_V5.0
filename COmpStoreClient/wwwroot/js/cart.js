$(document).ready(function () {
    $('.number-input').on('keypress', function () {
        return event.charCode >= 48 && event.charCode <= 57;
    })

    $('.cart_quantity_down').on('click', function () {
        var $quantity = $(this).siblings('.cart_quantity_input');
        var quantity = $quantity.val();
        var productId = $(this).siblings('.cart_product_id').val();
        if (--quantity > 0) {
            $quantity.val(quantity);
            updateTotal($(this).parents('.each-cart'));
            changeQuantity(quantity, productId);
        }
        finalPrice();
    })

    $('.cart_quantity_up').on('click', function () {
        var $quantity = $(this).siblings('.cart_quantity_input');
        var quantity = $quantity.val();
        var productId = $(this).siblings('.cart_product_id').val();
        if (++quantity <= $(this).data('max')) {
            $quantity.val(quantity);
            updateTotal($(this).parents('.each-cart'));
            changeQuantity(quantity, productId);
        }
        finalPrice();
    })
    finalPrice();


    $('.cart_quantity_delete').on('click', function () {
        var id = $(this).data('id');
        var $eachCart = $(this).parents('.each-cart');
        $.ajax({
            url: '/Cart/RemoveProductFromCart',
            data: { "productId": id },
            type: 'get',
            success: function (data) {
                if (data == true) {
                    console.log('success');
                    $eachCart.fadeOut(300, function () {
                        $(this).remove();
                        finalPrice();
                    })

                } else {
                    console.log('falied');
                }
            },
            error: function () {
                console.log('faliedd');
            }
        })
    })

})

function updateTotal(eachCart) {
    var quantity = eachCart.find('.cart_quantity_input').val();
    var total = eachCart.find('.cart_total_price');
    var price = eachCart.find('.cart_price').data('price');
    total.data('total', quantity * price);
    total.text(numberWithCommas(quantity * price));
}

function finalPrice() {
    var finalPrice = 0;
    $('.cart_total_price').each(function () {
        finalPrice += parseInt($(this).data('total'));
    });
    $('.final_total').text(numberWithCommas(finalPrice));
}

function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function changeQuantity(quantity, productId)
{
    var headers = new Headers();
    headers.append('Content-Type', 'application/json');
    var selectedProduct = { productId: productId, quantity: quantity };
    $.ajax({
        url: '/Cart/ChangeQuantity',
        data: JSON.stringify(selectedProduct),
        type: 'post',
        contentType: 'application/json',
        success: function (data) {
            data ? console.log('success') : console.log('error');
        },
        error: function () {
            console.log('error')
        }
    })

}