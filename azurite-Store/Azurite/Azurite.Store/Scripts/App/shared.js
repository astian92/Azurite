var loader = '<div class="loader">Loading...</div>';

(function () {
    var cookies;

    function readCookie(name, c, C, i) {
        if (cookies) { return cookies[name]; }

        c = document.cookie.split('; ');
        cookies = {};

        for (i = c.length - 1; i >= 0; i--) {
            C = c[i].split('=');
            cookies[C[0]] = C[1];
        }

        return cookies[name];
    }

    window.cookieJar = readCookie; // or expose it however you want


})();

var getUrlParameter = function getUrlParameter(url, sParam) {
    var paramStartIndex = url.indexOf('?');
    var sPageURL = decodeURIComponent(url.substr(paramStartIndex + 1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};