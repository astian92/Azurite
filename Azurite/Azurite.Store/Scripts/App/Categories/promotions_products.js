var orderBy = 1;
var searchValue = '';

$(document).ready(function () {
    loadSubCategories();
    loadProducts(searchValue, orderBy);

    $('#search').keyup(function (ev) {
        if (ev.which === 13) {
            searchValue = $('#search').val();
            loadProducts(searchValue, orderBy);
        }
    });

    $('#search-products').click(function () {
        searchValue = $('#search').val();
        loadProducts(searchValue, orderBy);
    });

    $('.orderBy').click(function () {
        var txt = $(this).text();
        $('#selected-order').text(txt);

        orderBy = $(this).attr('data-order');
        loadProducts(searchValue, orderBy);
    });
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

function loadProducts(value, orderBy) {
    //$('#productsContainer').html('');
    $.ajax({
        url: MVC.Products.GetAllPromoProductsFull + '?search=' + value + '&orderBy=' + orderBy,
        dataType: 'html',
        success: function (data) {
            $('#productsContainer').html(data);
        }
    });
}