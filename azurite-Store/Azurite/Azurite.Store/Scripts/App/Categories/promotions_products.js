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
    var page = window.location.search.slice(1);
    var pageNumber;

    if (page) {
        page = page.split('#')[0];
        var arr = page.split('page');
        pageNumber = arr[1].split('=')[1];
    }

    $.ajax({
        type: 'GET',
        url: MVC.Products.GetAllPromoProductsFull + '?' + page,
        dataType: 'html',
        success: function (data) {
            $('#productsContainer').html(data);
        }
    });
}