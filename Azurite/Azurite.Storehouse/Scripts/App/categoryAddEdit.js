
$(document).ready(function () {
    createICheck('.iCheck_cb'); //in miscellaneous
    $("#ParentId").select2({
        placeholder: "Избери Родител",
        allowClear: false,
        width: "100%"
    });

    $('#attribute-name-bg').on('input', function () {
        hideAttrValidationError();
    });

    $('#attribute-name-en').on('input', function () {
        hideAttrValidationError();
    });

    $('#ParentId').on('change', function () {
        var val = $(this).val();
        
        if (val == '00000000-0000-0000-0000-000000000000') {
            $('.images-formgroup').css('display', 'block');
        }
        else {
            $('.images-formgroup').css('display', 'none');
            clearSelection();
        }
    });

    var parentVal = $('#ParentId').val();
    if (parentVal != '00000000-0000-0000-0000-000000000000') {
        $('.images-formgroup').css('display', 'none');
        clearSelection();
    }

    
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
                            <input class="iCheck_cb" type="checkbox" name="" value="true" ' + checked + ' />\
                            <input type="hidden" class="attr-id" name="" value="false" />\
                            <label class="iCheck_lb"></label>\
                        </label>\
                    </div>\
                    <div class="col-md-2">\
                        <div class="x-icon-holder" onclick="removeCategoryAttribute(this)">\
                            <a class="fa fa-times x-icon"></a>\
                            <input type="hidden" class="attrId" name="" value="' + guid() + ' " />\
                            <input type="hidden" class="attrNameBg" name="" value="' + attrNameBg + '" />\
                            <input type="hidden" class="attrNameEn" name="" value="' + attrNameEn + '" />\
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
        li.find('.iCheck_cb').attr('name', 'CategoryAttributes[' + i + '].ActiveFilter')
        li.find('.attr-id').attr('name', 'CategoryAttributes[' + i + '].ActiveFilter')
        li.find('.attrId').attr('name', 'CategoryAttributes[' + i + '].Id');
        li.find('.attrNameBg').attr('name', 'CategoryAttributes[' + i + '].AttributeName');
        li.find('.attrNameEn').attr('name', 'CategoryAttributes[' + i + '].AttributeNameEN');
    }
}

function imageSelected(finput, event) {
    if (finput.files[0]) {
        var label = $(finput).parent();

        var img = label.find('.cat-img');
        var reader = new FileReader();
        reader.onload = function (e) {
            $(img).attr('src', e.target.result);
        };
        reader.readAsDataURL(finput.files[0]);

        var icon = label.find('.plusIcon');
        if (icon) {
            icon.removeClass('plusIcon');
            icon.removeClass('fa-plus');
            icon.addClass('remIcon');
            icon.addClass('fa-times');
            icon.prop('title', 'Премахни снимка');

            icon.on('click', function () {
                clearSelection();
                event.preventDefault();
                $('input[name=deleted]').val('true');
                return false;
            })
        }
    }
}

function clearSelection() {
    var finput = $('.cat-photo');
    var img = $('.cat-img');
    var icon = $('.editSpan i.fa');

    $(img).attr('src', '');
    $(finput).val('');

    icon.addClass('plusIcon');
    icon.addClass('fa-plus');
    icon.removeClass('remIcon');
    icon.removeClass('fa-times');
    icon.prop('title', 'Добави снимка');

    icon.off('click');
}

function onInitialEditX(event) {
    clearSelection();
    event.preventDefault();
    $('input[name=deleted]').val('true');
    return false;
}