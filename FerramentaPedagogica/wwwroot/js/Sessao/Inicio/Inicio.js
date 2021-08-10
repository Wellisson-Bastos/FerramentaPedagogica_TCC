$(document).ready(function () {
    buscarJogadores();
    $("#btn-iniciar-jogo").click(iniciarJogo);

    function buscarJogadores() {
        var lUrl = $('#listagem-jogadores').data('ajax-url');

        setInterval(function () {
            $.get(lUrl, function (html) {
                $('#listagem-jogadores').empty();
                $('#listagem-jogadores').html(html);
            })
        }, 5000);
    }

    function iniciarJogo() {
        var lsUrl = $(this).data('ajax-url');

        if (!window.screenTop && !window.screenY) {

            M.toast({ html: "Para iniciar o jogo, coloque o navegador em tela cheia clicando na tecla 'F11'", classes: 'rounded red' });

        } else if ($(".collection-item").length > 0) {

            window.location.href = lsUrl;
        }
        else {
            M.toast({ html: "Para iniciar o jogo, ao menos um jogador deve estar conectado.", classes: 'rounded red' });
        }
    }
});