var app = app||{};

//this file must be refactored, it gets all input elements with attr type=file, and when change get file in base64 string

(function(){
    $('input[type=file]').change(getFile);

    function getFile(){
        var reader = new FileReader(),
            file = this.files[0];
            //albumId = $(this).parent().attr('album-id');

        reader.addEventListener("load", function () {
            app.uploadNewPicture('5', reader.result);
        }, false);

        if (file){
            reader.readAsDataURL(file);
        }
    }
})();




