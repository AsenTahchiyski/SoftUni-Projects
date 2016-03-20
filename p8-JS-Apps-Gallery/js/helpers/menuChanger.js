var app = app || {};

(function (scope) {

    scope.changeActiveMenu = function (newActive) {
        var currentActive = $('.active-nav').toggleClass('active-nav');
        $('.' + newActive).toggleClass('active-nav');
    };

    scope.showHideLogout = function () {
        if (sessionStorage['sessionAuth']) {
            $('.logout-nav').show();
        } else {
            $('.logout-nav').hide();
        }
    }

})(app);