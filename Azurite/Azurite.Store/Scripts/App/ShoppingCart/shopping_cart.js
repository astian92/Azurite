function loadShoppingCart() {
    $.ajax({
        url: MVC.ShoppingCart.ProductsFull,
        dataType: 'html',
        success: function (data) {
            $('#cart-products').html(data);
        }
    });
}

$(document).ready(function () {
    loadShoppingCart();
});