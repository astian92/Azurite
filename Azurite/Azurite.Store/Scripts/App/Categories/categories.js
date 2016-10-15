$(document).ready(function () {
    loadBaseCategories();
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