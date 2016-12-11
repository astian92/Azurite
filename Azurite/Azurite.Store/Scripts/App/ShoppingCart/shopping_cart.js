function loadShoppingCart() {
    //$.ajax({
    //    url: MVC.ShoppingCart.ProductsFull,
    //    dataType: 'html',
    //    success: function (data) {
    //        $('#cart-products').html(data);
    //    }
    //});

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