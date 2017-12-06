function removeProduct(id) {
    $.ajax({
        url: MVC.ShoppingCart.RemoveProductFull + '/' + id,
        dataType: 'html',
        success: function (data) {
            $('#cart-item-' + id).remove();
        }
    });
}

function modifyProductQty(id, qty) {
    $.ajax({
        url: MVC.ShoppingCart.ModifyProductQtyFull + '?id=' + id + '&quantity=' + qty,
        dataType: 'html',
        success: function (data) {
            loadShoppingCart();
        }
    });
}

$(document).ready(function () {
    $('.cart-remove').click(function () {
        var productId = $(this).attr('productId');
        removeProduct(productId);
    });

    $('input[name=Quantity]').focusout(function () {
        var productId = $(this).attr('productId');
        var qty = $(this).val();
        modifyProductQty(productId, qty);
    });
});