$(document).ready(function () {
    verificarPergunta();

    function verificarPergunta() {
        var lUrl = $('.div-loading').data('ajax-url');

        setInterval(function () {
            $.post(lUrl, function (response) {
                if (!response.success && response.sessionClosed) {
                    M.toast({ html: response.responseText, classes: 'rounded red' });
                    window.setTimeout(function () { window.location.href = response.url; }, 3000)
                }
                else if (response.success) {
                    window.location.href = response.url;
                }

            }).fail(function (response) {
                M.toast({ html: response, classes: 'rounded red' });
            })
        }, 2000);
    }
});