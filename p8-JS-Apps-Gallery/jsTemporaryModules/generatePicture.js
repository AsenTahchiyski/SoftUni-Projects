var app = app || {};

(function(scope){
    function uploadNewPicture(albumId, picData) {
        var picture = {
            album: albumId,
            data: picData,
            rating: 0,
            comments: {}
        };

        scope.requester.addItemToCollection('Pictures', picture, function(){}, function(){});
    }

    scope.uploadNewPicture = uploadNewPicture;
})(app);

