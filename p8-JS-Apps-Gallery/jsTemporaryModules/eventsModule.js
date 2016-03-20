var app = app || {};

(function (scope) {

    var loginNavMenu = $('#login-nav'),
        loginPopupWindow = $('#login-popup'),
        loginButton = $('#login-btn'),
        createAlbumButton = $('#create-album'),
        closeOverlayButton = $('#close-overlay');


    loginNavMenu.on('click', function () {
        loginPopupWindow.fadeToggle();
    });

    createAlbumButton.on('click', function () {
        $('#create-album_overlay').show();
    });

    loginButton.on('click', scope.login);

    closeOverlayButton.on('click', function () {
        $('#create-album_overlay').hide();
    });

}(app));