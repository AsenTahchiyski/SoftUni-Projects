var app = app || {};

(function (scope) {

    // sammy routing
    scope.router = Sammy(function () {
        var requester = scope.requester.config('kid_Z1d1z2oEJ-', '1796d478bcf54ef8b10abddde51bfc45');
        var selector = $('.main-section'),

            albumsModel = scope.albumsModel.load(requester),
            usersModel = scope.userModel.load(requester),
            picturesModel = scope.picturesModel.load(requester),

            albumViewBag = scope.albumViews.load(),
            userViewBag = scope.userViews.load(),
            picturesViewBag = scope.pictureViews.load(),


            albumController = scope.albumController.load(albumsModel, albumViewBag),
            userController = scope.userController.load(usersModel, userViewBag),
            picturesController = scope.pictureController.load(picturesModel, picturesViewBag);

        this.before(function(){
            scope.showHideLogout();
        });

        this.get('#/', function () {
            if (!sessionStorage['sessionAuth']) {
                userController.loadLoginPage(selector);
            } else {
                albumController.showAlbumsByRating()
            }
            scope.changeActiveMenu('home-nav');

        });

        this.get('#/albums', function () {
            albumController.showAlbums();
            scope.changeActiveMenu('albums-nav');
        });

        this.get('#/about', function () {
            $.get('templates/aboutTemplate.html', function (content) {
                selector.html(content);
            });
            scope.changeActiveMenu('about-nav');

        });

        this.get('#/albums/:albumId', function () {
            picturesController.showPictures(this.params['albumId']);
        });

        this.get('#/logout', function () {
            userController.logout();
        });

        this.bind('add-album', function (e, data) {
            albumController.addAlbum(data)
        });

        this.bind('login', function (e, data) {
            userController.login(data);
        });

        this.bind('redirectUrl', function (e, data) {
            this.redirect(data.url)
        });

        this.bind('show-album', function (e, data) {
            picturesController.showPictures(data.album);
        });

        this.bind('register', function (e, data) {
            userController.register(data);
        });


        this.bind('add-picture', function (e, data) {
            var liNumber = $('.gallery-grid').children().length;
            picturesController.addPicture(data);

            if (liNumber==1){
                albumController.updateBackgroundPicture(data);
            }
        });

        this.bind('update-pic-rating', function (e, data) {
            picturesController.updatePicture(data);
        });

        this.bind('update-album-rating', function(e, data){
            albumController.updateAlbumRating(data.albumId)
        });
    });

    scope.router.run('#/');
})(app);



