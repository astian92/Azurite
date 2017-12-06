function AddToCart(id, quantity) {
    $.ajax({
        url: MVC.ShoppingCart.AddProductFull + '?id=' + id + '&quantity=' + quantity,
        dataType: 'html',
        success: function (data) {
            var res = JSON.parse(data);
            if (res === 'ok') {
                var txt = 'Добавен';
                var cookieVal = window.cookieJar("Language");
                if (cookieVal === 'en') {
                    txt = 'ADDED';
                }

                $('button[productId=' + id + ']').text(txt);
            }
        }
    });
}

$(document).ready(function () {
    $('.btn-cart').click(function () {
        var id = $(this).attr('productId');
        var quantity = $('#qty_' + id).val();

        AddToCart(id, quantity);
    });
});