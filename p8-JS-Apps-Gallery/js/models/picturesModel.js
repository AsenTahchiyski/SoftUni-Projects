var app = app || {};

app.picturesModel = (function () {
    function PicturesModel(requester) {
        this.requester = requester;
        this.serviceUrl = this.requester.baseUrl + 'appdata/' + this.requester.appId + '/Pictures';
    }

    PicturesModel.prototype.getAllPictures = function (albumId) {
        var requestId = this.serviceUrl + '/?query={"albumId":"' + albumId + '"}';
        return this.requester.get(requestId, true)
    };

    PicturesModel.prototype.addNewPicture = function (picture) {
        return this.requester.post(this.serviceUrl, picture, true)
    };

    PicturesModel.prototype.updatePicture = function (picture) {
        return this.requester.put(this.serviceUrl + '/' + picture._id, picture, true);
    };

    return {
        load: function (requester) {
            return new PicturesModel(requester)
        }
    }
})();
