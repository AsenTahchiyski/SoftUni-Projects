var app = app || {};

app.userViews = (function () {
    function showLoginPage(selector) {
        $.get('templates/loginTemplate.html', function (templ) {
            $(selector).html(templ);
            $('#login-button').on('click', function () {

                var username = $('#login-username').val(),
                    password = $('#login-password').val();

                $.sammy(function () {
                    this.trigger('login', {username: username, password: password});
                });
            });

            $('#register-button').on('click', function() {

                var regUsername = $('#register-username').val(),
                    regPassword = $('#register-password').val();

                $.sammy(function() {
                    this.trigger('register', {username: regUsername, password: regPassword})
                })
            })
        })
    }

    return {
        load: function () {
            return {
                showLoginPage: showLoginPage
            }
        }
    }
}());