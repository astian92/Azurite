var categoryId = '';
var orderBy = 1;
var searchValue = '';
var productAttrValues = [];

$(document).ready(function () {
    categoryId = $('#categoryId').val();

    loadSubCategories(categoryId);
    loadCategoryAttr(categoryId);
    loadProducts(categoryId, productAttrValues, searchValue, orderBy);

    $('#search').keyup(function(ev) {
        if (ev.which === 13) {
            searchValue = $('#search').val();
            loadProducts(categoryId, productAttrValues, searchValue, orderBy);
        }
    });

    $('#search-products').click(function () {
        searchValue = $('#search').val();
        loadProducts(categoryId, productAttrValues, searchValue, orderBy);
    });

    $('.orderBy').click(function () {
        var txt = $(this).text();
        $('#selected-order').text(txt);

        orderBy = $(this).attr('data-order');
        loadProducts(categoryId, productAttrValues, searchValue, orderBy);
    });
});

function changeAttrFilters() {
    productAttrValues = [];

    $('input[type=checkbox]:checked').each(function (index, el) {
        productAttrValues[index] = $(this).val();
    });

    loadProducts(categoryId, productAttrValues, searchValue, orderBy);
}

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

function loadProducts(categoryId, productAttrValues, value, orderBy) {
    $('#productsContainer').html(loader);
    $.ajax({
        type: 'POST',
        url: MVC.Products.GetCategoryProductsFull,
        dataType: 'html',
        data: {
            categoryId: categoryId,
            productAttrValues: productAttrValues,
            search: value,
            orderBy: orderBy
        },
        success: function (data) {
            $('#productsContainer').html(data);
        }
    });
}