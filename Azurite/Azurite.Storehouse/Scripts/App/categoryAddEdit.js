
$(document).ready(function () {
    createICheck('.iCheck_cb'); //in miscellaneous

    $('#cat-img').on('change', function () {
        var fileName = this.files[0].name
        $('.selected-file-name').text(fileName);
    });

    $('#attribute-name-bg').on('input', function () {
        hideAttrValidationError();
    });

    $('#attribute-name-en').on('input', function () {
        hideAttrValidationError();
    });

});

function createCategoryAttribute() {
    var attrNameBg = $('#attribute-name-bg').val();
    var attrNameEn = $('#attribute-name-en').val();
    var isActive = $('#isAttrActive').prop('checked');

    if (attrNameBg == '' || attrNameBg == undefined || attrNameBg == null || 
        attrNameEn == '' || attrNameEn == undefined || attrNameEn == null) {
        $('#attr-validation-error').css('display', 'inline');
        return;
    }

    $('#attribute-name-bg').val('');
    $('#attribute-name-en').val('');

    var liItem = createAttributeHtml(attrNameBg, attrNameEn, isActive);
    $('#attributes-list').append(liItem);
    createICheck('.iCheck_cb');

    updateAttributeIndexes();
}

function createAttributeHtml(attrNameBg, attrNameEn, isActive) {
    var checked = '';
    if (isActive) {
        checked = 'checked';
    }
    var html = $('<div class="cat-attribute row">\
                    <div class="col-md-4">\
                        <label class="control-label">' + attrNameBg + '</label>\
                    </div>\
                    <div class="col-md-4">\
                        <label class="control-label">' + attrNameEn + '</label>\
                    </div>\
                    <div class="col-md-2">\
                        <label class="control-label">\
                            <input id="isAttrActive" class="iCheck_cb" type="checkbox" ' + checked + ' disabled />\
                            <label for="isAttrActive" class="iCheck_lb"></label>\
                        </label>\
                    </div>\
                    <div class="col-md-2">\
                        <div class="x-icon-holder" onclick="removeCategoryAttribute(this)">\
                            <a class="fa fa-times x-icon"></a>\
                            <input type="hidden" class="attrId" name="" value="' + guid() + ' " />\
                            <input type="hidden" class="attrNameBg" name="CategoryAttributes[0].AttributeName" value="' + attrNameBg + '" />\
                            <input type="hidden" class="attrNameEn" name="CategoryAttributes[0].AttributeNameEN" value="' + attrNameEn + '" />\
                        </div>\
                    </div>\
                </div>');

    var li = $('<li></li>');
    li.append(html);
    
    return li;
}

function removeCategoryAttribute(icon_holder) {
    $(icon_holder).parent().parent().parent().remove();
    updateAttributeIndexes();
}

function hideAttrValidationError() {
    $('#attr-validation-error').css('display', 'none');
}

function updateAttributeIndexes() {
    var attributes = $('#attributes-list').children();

    for (var i = 0; i < attributes.length; i++) {
        var li = $(attributes[i]);
        li.find('.attrId').attr('name', 'CategoryAttributes[' + i + '].Id');
        li.find('.attrNameBg').attr('name', 'CategoryAttributes[' + i + '].AttributeName');
        li.find('.attrNameEn').attr('name', 'CategoryAttributes[' + i + '].AttributeNameEN');
    }
}