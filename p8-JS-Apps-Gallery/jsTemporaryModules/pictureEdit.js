//var samplePicture = {
//    album: 'albumId',
//    pictureName: 'name',
//    data: '4g6a46g4a4gg88588weegaga',
//    rating: 'some Number',
//    comments: {
//        //username as key
//        Pesho: 'comment',
//        Ivan: 'comment',
//        Asen: 'comment'
//    }
//};

var app = app||{};

(function(scope){
    function updatePicture(id, changedProperties){
        var updated;
        scope.requester.getItemById(
            'Pictures',
            id,
            function(picture){
                picture['comments'] = changedProperties['comments'];
                picture['rating'] = changedProperties['rating'];
                updated = picture;
            },
            function(err){console.error(err)}
        );



        scope.requester.editItem('Pictures', updated._id, updated, function(){}, function(){});
    }

    scope.updatePicture = updatePicture;
})(app);



