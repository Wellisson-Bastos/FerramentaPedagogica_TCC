$(document).ready(function () {
    $("#botao-filtragem").click(obterListaJogos);

    function obterListaJogos()
    {
        var lsUrl = $(this).data('ajax-url');
        var lsTermoPesquisa = $("#termo-pesquisa").val();

        $('.progress').show();

        $.get(lsUrl + '&TermoPesquisa=' + lsTermoPesquisa, function (html)
        {
            $("#lista-jogos").empty();
            $("#lista-jogos").html(html);

            $('.progress').hide();
        })
    }
});