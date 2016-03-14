(function () {
    $(function () {
        $('#input_apiKey').off();
        $('#input_apiKey').on('change', function () {
            var key = this.value;
            if (key && key.trim() !== '') {
                swaggerUi.api.clientAuthorizations.add("key", new SwaggerClient.ApiKeyAuthorization("X-ApiKey", key, "header"));
            }
        });
        var basicAuthUI =
                '<div class="input"><input placeholder="username" id="input_username" name="username" type="text" size="10"></div>' +
                '<div class="input"><input placeholder="password" id="input_password" name="password" type="password" size="10"></div>';
        $(basicAuthUI).insertBefore('#api_selector div.input:last-child');

        $('#input_username').change(getAuthorization);
        $('#input_password').change(getAuthorization);
    });
    
    function getAuthorization() {
        console.log('addAuthorization');
        var username = $('#input_username').val();
        var password = $('#input_password').val();
        if (username && username.trim() != "" && password && password.trim() != "") {
            getToken(username, password);
        }
    }
    function getToken(user, pass) {
        var creds = "grant_type=password&username=" + user + "&password=" + pass;
        console.log("creds = " + creds);
        var tokenUrl = "http://" + window.location.host + "/token";
        $.ajax({
            type: "POST",
            url: tokenUrl,
            contentType: "application/x-www-form-urlencoded",
            data: creds,
            async: true,
            success: function (data, status, request) {
                console.log('Bearer ' + data['access_token']);
                var token = 'Bearer ' + data['access_token'];
                window.swaggerUi.api.clientAuthorizations.add("key1", new SwaggerClient.ApiKeyAuthorization("Authorization", token, "header"));
            },
            error: function(jqXHR, textStatus, errorThrown){
            console.log('Error getting token');
            }
        }).then(function (data) {
            $('.greeting-id').append(data.id);
            $('.greeting-content').append(data.content);
        });
    }
})();