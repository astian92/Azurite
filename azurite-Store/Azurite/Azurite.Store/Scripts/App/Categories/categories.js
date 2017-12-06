$(document).ready(function () {
    loadBaseCategories();
    loadPromoProducts();
});

function loadBaseCategories() {
    $('#categoriesContainer').html('');
    $.ajax({
        url: MVC.Categories.GetBaseCategoriesFull, // + '?categoryId' + categoryId,
        dataType: 'html',
        success: function (data) {
            $('#categoriesContainer').html(data);
        }
    });
}

function loadPromoProducts() {
    $('#promotionsContainer').html('');
    $.ajax({
        url: MVC.Products.GetPromoProductsFull,
        dataType: 'html',
        success: function (data) {
            $('#promotionsContainer').html(data);
        }
    });
}