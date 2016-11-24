function removeProduct(id) {
    $.ajax({
        url: MVC.ShoppingCart.RemoveProductFull + '/' + id,
        dataType: 'html',
        success: function (data) {
            $('.cart-item[productId=' + id + ']').remove();
        }
    });
}

$(document).ready(function () {
    $('.btn-cart-remove').click(function () {
        var productId = $('.btn-cart-remove').attr('productId');
        removeProduct(productId);
    });
});