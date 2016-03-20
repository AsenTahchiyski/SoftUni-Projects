var app = app || {};


app.galleryPopup = (function () {

    $('.single-picture').on('click', function () {
        var overlay = $('<div>').addClass('gallery-overlay'),
            closeOverlay = $('<span>')
                .addClass('close-overlay')
                .on('click', function () {
                    overlay.hide();
                }),
            closeImage = $('<img>').attr('src', 'img/close_button.png'),
            imageHolder = $('<div>').addClass('single-image_holder');
        var imageSrc = $(this).find('img').attr('src');
        var imagePreview = $('<img>').attr('src', imageSrc).addClass('image-preview');

        var commentSection = $('<div>').addClass('comment-section').text('Comment section here');
        var ratingSection = $('<div>').addClass('picture-rating-popup').text('Like');
        var likeButton = $('<button>').addClass('picture-like-button').html('<img src="img/like.png" alt="Like">');

        closeOverlay.append(closeImage);
        ratingSection.append(likeButton);
        commentSection.append(ratingSection);
        imageHolder.append(imagePreview, closeOverlay, commentSection);
        overlay.append(imageHolder);
        $('.main-section').append(overlay);
    })
});