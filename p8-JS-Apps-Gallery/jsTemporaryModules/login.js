var app = app || {};

(function (scope) {
    function login() {
        var username = $('#username').val(),
            password = $('#password').val(),
            currentUser = {
                "username": username,
                "password": password
            };

        $.ajax({
            type: "POST",
            url: "https://baas.kinvey.com/user/kid_Z1d1z2oEJ-/login",
            data: currentUser,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Basic " + btoa(currentUser.username + ':' + currentUser.password));
            },
            success: function (data) {
                sessionStorage.authToken = data._kmd.authtoken;
                $('#login-popup').hide();
                console.log('User logged');
            },
            error: function (error) {
                console.error(error);
                alert('Invalid username or password');
            }
        })
    }

    scope.login = login;
})(app);
