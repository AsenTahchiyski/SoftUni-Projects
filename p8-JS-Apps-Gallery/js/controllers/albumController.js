var app = app || {};

app.albumController = (function () {
    function AlbumController(model, viewBag) {
        this._model = model;
        this._viewBag = viewBag;
    }

    AlbumController.prototype.showAlbums = function showAlbums() {
        var _this = this;

        _this._model.getAllAlbums()
            .then(function (albums) {
                _this._viewBag.showAlbums(albums);
            })
    };

    AlbumController.prototype.showAlbumsByRating = function showAlbumsByRating() {
        var _this = this;

        _this._model.getAllAlbums()
            .then(function (albums) {
                albums = albums.sort(function (a, b) {
                    return b.rating - a.rating
                });

                _this._viewBag.showTopAlbums(albums);
            })
    };

    AlbumController.prototype.updateAlbumRating = function updateAlbumRating(albumId){
        var _this = this;
        this._model.getAlbumById(albumId)
            .then(function(success){
                success.rating++;
                _this._model.updateAlbum(albumId, success);
        }).done()
    };

    AlbumController.prototype.updateBackgroundPicture = function updateBackgroundPicture (data){
        var _this = this;
        _this._model.getAlbumById(data.albumId).then(function(album){
                album.isEmpty = false;
                album.backGroundPicture = data.base64data;
                _this._model.updateAlbum(data.albumId, album)
        }).done();
    };

    AlbumController.prototype.addAlbum = function addAlbum(data) {
        var _this = this,
            obj = app.albumInputModel(data.name),
            albumOutputModel = obj.getAlbumInputModel();

        this._model.addNewAlbum(albumOutputModel).then(
            _this._model.getAllAlbums()
                .then(function (albums) {
                    _this.showAlbums(albums);
                })
        );
    };

    return {
        load: function (model, viewBag) {
            return new AlbumController(model, viewBag)
        }
    }
})();
