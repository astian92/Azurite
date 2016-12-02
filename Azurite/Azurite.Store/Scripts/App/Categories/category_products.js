var orderBy = 1;
var searchValue = '';

$(document).ready(function () {
    var categoryId = $('#categoryId').val();

    loadSubCategories(categoryId);
    loadCategoryAttr(categoryId);
    loadProducts(categoryId, searchValue, orderBy);

    $('#search').keyup(function(ev) {
        if (ev.which === 13) {
            searchValue = $('#search').val();
            loadProducts(categoryId, searchValue, orderBy);
        }
    });

    $('#search-products').click(function () {
        searchValue = $('#search').val();
        loadProducts(categoryId, searchValue, orderBy);
    });

    $('.orderBy').click(function () {
        var txt = $(this).text();
        $('#selected-order').text(txt);

        orderBy = $(this).attr('data-order');
        loadProducts(categoryId, searchValue, orderBy);
    });
});

function loadSubCategories(categoryId) {
    $('#subCategoriesContainer').html('');
    $.ajax({
        url: MVC.Categories.GetCategoryTreeFull + '?categoryId=' + categoryId,
        dataType: 'html',
        success: function (data) {
            $('#subCategoriesContainer').html(data);
            $('.active').parents('ul').removeClass('collapse');
            $('.active').parents('li').addClass('parent');
        }
    });
}

function loadCategoryAttr(categoryId) {
    $('#categoriesAttrContainer').html('');
    $.ajax({
        url: MVC.Categories.GetCategoryAttrFull + '?categoryId=' + categoryId,
        dataType: 'html',
        success: function (data) {
            $('#categoriesAttrContainer').html(data);
        }
    });
}

function loadProducts(categoryId, value, orderBy) {
    //$('#productsContainer').html('');
    $.ajax({
        url: MVC.Products.GetCategoryProductsFull + '?categoryId=' + categoryId + '&search=' + value + '&orderBy=' + orderBy,
        dataType: 'html',
        success: function (data) {
            $('#productsContainer').html(data);
        }
    });
}