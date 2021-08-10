$(document).ready(function () {
    initTemporizador();
    $(".div-opcao").click(marcarResposta);

    function initTemporizador() {
        var time = $("#tempoPergunta").val();
        var initialOffset = '440';
        var i = 1;
        var lTimeController = parseInt(time, 10);

        $('.circle_animation').css('stroke-dashoffset', initialOffset - (1 * (initialOffset / time)));

        var interval = setInterval(function () {
            $("#tempo-pergunta").text(lTimeController - 1);
            $('.circle_animation').css('stroke-dashoffset', initialOffset - ((i + 1) * (initialOffset / time)));
            i++;
            lTimeController--;

            if (lTimeController === 1) {
                clearInterval(interval);
                var lUrl = $('#tempo-pergunta').data('ajax-url');
                var lsResposta = $(".div-opcao.selected").data('resposta');

                $.post(lUrl + "&Resposta=" + lsResposta, function (response)
                {
                    if (response.success)
                    {
                        window.location.href = response.url;
                    }
                });
            }
        }, 1000);
    }

    function marcarResposta() {
        $('.div-opcao').removeClass('selected');
        $(this).addClass('selected');
        $(this).css('border', '4px solid grey;')
    }
});