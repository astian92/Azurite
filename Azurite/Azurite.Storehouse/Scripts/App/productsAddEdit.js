
function initAdd() {
    prepare();

    var categoryId = $('#CategoryId').val();
    getCategoryAttributes(categoryId);
}

function initEdit() {
    prepare();
}

function prepare() {
    $("#CategoryId").select2({
        placeholder: "Избери Категория",
        allowClear: false,
        width: "100%"
    });

    $("#CategoryId").on('change', function () {
        var id = $("#CategoryId").val();
        getCategoryAttributes(id);
    });

    createICheck('.iCheck_cb');
}

function getCategoryAttributes(categoryId) {
    $('.attributes-holder').empty();
    var promise = Ajax.get(MVC.Products.GetCategoryAttributes + "?categoryId=" + categoryId);
    
    promise.done(function (attributes) {
        console.log(attributes);
        generateAttributesUI(attributes);
    });
}

function generateAttributesUI(attributes) {
    var fragment = $(new DocumentFragment());
    for (var i = 0; i < attributes.length; i++) {
        var attribute = attributes[i];

        var uiElement = $('<div>\
                            <input type="hidden" name="ProductAttributes[' + i + '].AttributeId" value="' + attribute.Id + '" />\
                            <div class="form-group">\
                                <label class="control-label">' + attribute.AttributeName + ' / ' + attribute.AttributeNameEN + '</label>\
                            </div>\
                            <input name="ProductAttributes[' + i + '].Value" type="text" class="form-control" placeholder="BG" \
                                data-val="true" data-val-required="Полето е задължително!" />\
                            <span class="field-validation-valid text-danger" data-valmsg-for="ProductAttributes[' + i + '].Value" data-valmsg-replace="true"></span>\
                            <input name="ProductAttributes[' + i + '].ValueEN" type="text" class="form-control m-t-10" placeholder="EN" \
                                data-val="true" data-val-required="Полето е задължително!" />\
                            <span class="field-validation-valid text-danger" data-valmsg-for="ProductAttributes[' + i + '].ValueEN" data-valmsg-replace="true"></span>\
                        </div>')
        
        fragment.append(uiElement);
    }

    $('.attributes-holder').append(fragment);

    $("#addProductForm")
    .removeData("validator")
    .removeData("unobtrusiveValidation");

    //Parse the form again
    $.validator
        .unobtrusive
        .parse("#addProductForm");
}

function imageSelected(finput) {
    if (finput.files[0]) {
        var adder = $('<div class="col-md-55">\
                            <div class="image-btn-holder">\
                                <label class="image-btn" title="Добави снимка">\
                                    <input type="file" name="photos" class="hidden" accept=".jpg, .png, .jpeg" onchange="imageSelected(this)" />\
                                    <i class="fa fa-plus plusIcon"></i>\
                                    <img class="newImage" src="" />\
                                </label>\
                            </div>\
                        </div>');

        var holder = $(finput).parent().parent();
        holder.removeClass('image-btn-holder');
        holder.addClass('image-container');
        var img = holder.find('img');
        var icon = holder.find('.plusIcon');
        icon.remove();
        var label = holder.find('label.image-btn')[0].style.pointerEvents = 'none';
        holder.prepend($('<span class="remove-file" title="Премахни" onclick="removeFile(this)">\
                                    <i class="fa fa-times"></i>\
                                </span>'));

        var reader = new FileReader();

        reader.onload = function (e) {
            $(img).attr('src', e.target.result);
        };

        reader.readAsDataURL(finput.files[0]);

        $('.images-holder').append(adder);
    }
}

function removeFile(span) {
    $(span).parent().parent().remove();
}