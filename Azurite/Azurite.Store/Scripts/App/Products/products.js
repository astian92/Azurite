function AddToCart(id) {
    $.ajax({
        url: MVC.ShoppingCart.AddProductFull + '/' + id,
        dataType: 'html',
        success: function (data) {
            var res = JSON.parse(data);
            alert(res);
        }
    });
}

$(document).ready(function () {


});