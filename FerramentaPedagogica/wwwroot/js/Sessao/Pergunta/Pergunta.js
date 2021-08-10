$(document).ready(function () {
    initPergunta();

    var audio = document.getElementById("efeitoSonoro");
    audio.volume = 0.1;

    function initPergunta() {
        var tempo = parseInt($("#Tempo").val(), 10);
        var timer = setInterval(function () {
            if (tempo === 0) {
                clearInterval(timer);
                endCountdown();
            } else {
                tempo--;
            }
        }, 1000);
    }

    function endCountdown() {
        var lsUrl = $('#texto-pergunta').data('ajax-url');

        window.location.href = lsUrl;
    }
});