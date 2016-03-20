var app = app || {};

(function (scope) {

    scope.base64EncodePicture = function () {

        $('input[type=file]').change(getFile);

        function getFile() {
            var reader = new FileReader(),
                file = this.files[0];

            reader.addEventListener("load", function () {
                app.uploadNewPicture('5', reader.result);
            }, false);

            if (file) {
                reader.readAsDataURL(file);
            }
        }
    }

})(app);