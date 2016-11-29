$(document).ready(function () {
    loadSubCategories();
    loadProducts();
});

function loadSubCategories() {
    $('#subCategoriesContainer').html('');
    $.ajax({
        url: MVC.Categories.GetCategoryTreeFull,
        dataType: 'html',
        success: function (data) {
            $('#subCategoriesContainer').html(data);
            $('.active').parents('ul').removeClass('collapse');
            $('.active').parents('li').addClass('parent');
        }
    });
}

function loadProducts() {
    $('#productsContainer').html('');
    $.ajax({
        url: MVC.Products.GetAllPromoProductsFull,
        dataType: 'html',
        success: function (data) {
            $('#productsContainer').html(data);
        }
    });
}