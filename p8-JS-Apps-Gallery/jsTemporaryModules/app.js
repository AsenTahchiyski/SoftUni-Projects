var app = app||{};

$('#login').on('click', app.login);
$('#view-pics').on('click', function(){
    app.requester.getCollection('Pictures', showImages, function (err){console.error(err)})
});

function showImages(collection){
    collection.forEach(function(image){
        $('#wrapper').append($('<img>').attr('src', image.data));
    })
}