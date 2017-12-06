function loadShoppingCart() {
    $('#cart-summary').html('');
    $.ajax({
        url: MVC.ShoppingCart.CartSummaryFull,
        dataType: 'html',
        success: function (data) {
            $('#cart-summary').html(data);
        }
    });
}

$(document).ready(function () {
    loadShoppingCart();
});