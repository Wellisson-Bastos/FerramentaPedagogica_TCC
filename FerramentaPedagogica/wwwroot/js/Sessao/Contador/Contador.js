$(document).ready(function () {
    initContador();

    function initContador() {
        var count = 7;
        var timer = setInterval(function () {
            if (count === 0) {
                clearInterval(timer);
                endCountdown();
            } else {
                $('#contador').html(count);
                count--;
            }
        }, 1000);
    }

    function endCountdown() {
        var lsUrl = $('#contador').data('ajax-url');

        window.location.href = lsUrl;
    }
});