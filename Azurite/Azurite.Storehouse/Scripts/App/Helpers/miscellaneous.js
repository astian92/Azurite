
//IMPORTANT - ajax calls simplified
var Ajax = (function () {
    var ajaxCall = function (type, url, data, settings) {

        var ajaxObject = {
            type: type,
            url: url,
            data: data || undefined
        };

        for (var propertyName in settings) {
            ajaxObject[propertyName] = settings[propertyName];
        }

        return $.ajax(ajaxObject);
    }

    var get = function (url, settings) {
        return ajaxCall('GET', url, undefined, settings);
    }

    var post = function (url, data, settings) {
        return ajaxCall('POST', url, data, settings);
    }

    return {
        get: get,
        post: post
    }
}());

function RoundNumber(number, digits) {
    var gear = Math.pow(10, digits);

    return Math.ceil(number * gear) / gear;
}

var guid = (function () {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
                   .toString(16)
                   .substring(1);
    }
    return function () {
        return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
               s4() + '-' + s4() + s4() + s4();
    };
})();

Number.prototype.pad = function (size) {
    var s = String(this);
    while (s.length < (size || 2)) { s = "0" + s; }
    return s;
}

//--1
function css(jqueryElement) {
    var sheets = document.styleSheets;
    var result = {};

    for (var i in sheets) {
        var rules = sheets[i].rules || sheets[i].cssRules;

        for (var rule in rules) {
            if (jqueryElement.is(rules[rule].selectorText)) { //matches to css selector (true)
                //result = $.extend(result, css2json(rules[rule].style), css2json(jqueryElement.attr('style')));
                result = $.extend(result, rules[rule].style, jqueryElement.attr('style'));
            }
        }
    }

    return result;
}

function getCaretPosition(ctrl) {
    var CaretPos = 0;
    // IE Support
    if (document.selection) {
        ctrl.focus();
        var Sel = document.selection.createRange();
        Sel.moveStart('character', -ctrl.value.length);
        CaretPos = Sel.text.length;
    }
        // Firefox support
    else if (ctrl.selectionStart || ctrl.selectionStart == '0') {
        CaretPos = ctrl.selectionStart;
    }

    return (CaretPos);
}

function setCaretPosition(ctrl, pos) {
    if (ctrl.setSelectionRange) {
        ctrl.focus();
        ctrl.setSelectionRange(pos, pos);
    }
    else if (ctrl.createTextRange) {
        var range = ctrl.createTextRange();
        range.collapse(true);
        range.moveEnd('character', pos);
        range.moveStart('character', pos);
        range.select();
    }
}

//Changes decimal hours to hh:mm format
function toHoursMinutes(decimalHour) {
    var sign = "";
    if (decimalHour < 0) {
        sign = '-';
        decimalHour *= -1;
    }
    var minutes = decimalHour % 60;
    var hours = (decimalHour - minutes) / 60;
    //var minutes = (((decimalHour * 100) % 100) / 100) * 60;
    if (hours.length == 1) {
        hours = '0' + hours;
    }
    if (minutes == '0') {
        minutes = '00';
    }
    if (minutes < 0) {
        minutes = minutes + 60;
        hours++;
    }
    return sign + hours + ":" + minutes;
}

//Changes hours from hh:mm format to decimal hours.
function toDecimalHours(hoursMinutes) {
    var isMinus = 1;
    if (hoursMinutes[0] == "-") {
        isMinus = -1;
    }

    if (hoursMinutes.indexOf(':') == -1) {
        return 0;
    }
    var items = hoursMinutes.split(":");
    var hours = parseInt(items[0]) * 60;
    var minutes = items[1];

    return isMinus * (Math.abs(parseInt(hours)) + parseInt(minutes));
}