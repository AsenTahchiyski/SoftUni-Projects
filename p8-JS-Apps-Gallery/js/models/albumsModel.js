var app = app || {};

app.albumsModel = (function () {
    function AlbumsModel(requester) {
        this.requester = requester;
        this.serviceUrl = this.requester.baseUrl + 'appdata/' + this.requester.appId + '/Albums';
    }

    AlbumsModel.prototype.getAllAlbums = function getAllAlbums() {
        return this.requester.get(this.serviceUrl, true)
    };

    AlbumsModel.prototype.getAlbumById = function getAlbumById (id) {
        var requestUrl = this.serviceUrl + '/' + id;
        return this.requester.get(requestUrl, true)
    };

    AlbumsModel.prototype.addNewAlbum = function addAlbum(album) {
        return this.requester.post(this.serviceUrl, album, true)
    };

    AlbumsModel.prototype.updateAlbum = function updateAlbum (id, data) {
        var requestUrl = this.serviceUrl + '/' + id;
        return this.requester.put(requestUrl, data, true)
    };

    return {
        load: function (requester) {
            return new AlbumsModel(requester)
        }
    }
})();
