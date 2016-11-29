function GetRelatedProducts() {
    var categoryId = $('#categoryId').val();
    $.ajax({
        url: MVC.Products.GetRelatedProductsFull + '?categoryId=' + categoryId,
        dataType: 'html',
        success: function (data) {
            $('#related-products').html(data);
        }
    });
}

$(document).ready(function () {
    $(".img-preview").elevateZoom({
        tint: true, tintColour: '#C7B56F', tintOpacity: 0.5,
        zoomWindowFadeIn: 500,
        zoomWindowFadeOut: 500,
        lensFadeIn: 500,
        lensFadeOut: 500 });

    $(window).resize(function (e) {
        $('.zoomContainer').remove();
        $(".img-preview").elevateZoom({
            tint: true, tintColour: '#C7B56F', tintOpacity: 0.5,
            zoomWindowFadeIn: 500,
            zoomWindowFadeOut: 500,
            lensFadeIn: 500,
            lensFadeOut: 500
        });
    });

    $('.img-thumbnail').hover(function () {
        var imgSrc = $(this).attr('src');
        $('.product-images .img-thumbnail').removeClass('active');
        $(this).addClass('active');
        $('.img-preview').attr('src', imgSrc);
        //$('.img-preview').attr('data-zoom-image', imgSrc);
        $(".img-preview").data('zoom-image', imgSrc).elevateZoom({
            tint: true, tintColour: '#C7B56F', tintOpacity: 0.5, zoomWindowFadeIn: 500,
            zoomWindowFadeOut: 500,
            lensFadeIn: 500,
            lensFadeOut: 500
        });
    });

    GetRelatedProducts()
});