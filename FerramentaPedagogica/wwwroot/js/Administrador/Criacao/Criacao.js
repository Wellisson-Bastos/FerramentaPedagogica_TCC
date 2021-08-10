$(document).ready(function () {
    $("#btn-adicionar-pergunta").click(criarNovaPergunta);
    $("#btn-favoritar-jogo").click(favoritarJogo);
    $(".collection-item").click(buscarPergunta);

    function criarNovaPergunta() {
        desabilitarBotoes();

        var lCount = 0;
        var lsCodigoJogo = $("#CodigoJogo").val();
        var lsCodigoUsuario = $("#CodigoUsuario").val();

        $('.collection-item').each(function () { lCount++; })

        $(".collection").append("<a href='#!' class='collection-item truncate' data-ajax-url='/Criacao?Usuario=" + lsCodigoUsuario + "&Jogo=" + lsCodigoJogo + "' data-codigo-jogo>" + (lCount + 1) + " - Nova Pergunta</a>");

        $.get($(this).data('ajax-url'), function (html) {
            $("#div-pergunta").empty();
            $("#div-pergunta").html(html);
            initEditarPergunta();
            $(".collection-item").off('click');
            $(".collection-item").click(buscarPergunta);
            $(".collection-item").removeClass('active');
            $(".collection").children().last().addClass('active');
            configuraInputImagem();
            configuraInputUrlYoutube();
            habilitarBotoes();
        })
    }

    function favoritarJogo()
    {
        var lsIcone = "";
        var lsTexto = "";
        var lFavorito = "";
        var lbFavoritar = $(this).data('favorito') == "S";
        var lsUrl = $(this).data('ajax-url') + "/" + !lbFavoritar;

        if (lbFavoritar) {
            lsTexto = "Adicionar aos favoritos"
            lsIcone = "star_border";
            lFavorito = "N";
        }
        else
        {
            lsTexto = "Remover dos favoritos"
            lsIcone = "star";
            lFavorito = "S";
        }

        $.post(lsUrl, function (response) {
            M.toast({ html: response.responseText, classes: 'rounded green' });

            $("#btn-favoritar-jogo").html(lsTexto + "<i class='material-icons right'>" + lsIcone + "</i>");
            $("#btn-favoritar-jogo").data("favorito", lFavorito);
        })
    }

    function buscarPergunta()
    {
        desabilitarBotoes();
        $(".collection-item").removeClass('active');
        $(this).addClass('active');

        var lsUrl = $(this).data('ajax-url') + '&Pergunta=' + $(this).data('codigo-pergunta');

        $.get(lsUrl, function (html) {
            $("#div-pergunta").empty();
            $("#div-pergunta").html(html);
            initEditarPergunta();
            configuraInputImagem();
            configuraInputUrlYoutube();
            habilitarBotoes();
            validaUsuarioCriador()
        })
    }

    function initEditarPergunta() {
        $("#btn-excluir-pergunta").click(excluirPergunta);
        $("#btn-salvar-pergunta").click(editarPergunta);
        M.Range.init($("input[type=range]"));
        $("#btn-excluir-midia").click(limparMidia);

        $("#frmEditarPergunta").ajaxForm({
            success: function (response) {
                if (response.success) {
                    M.toast({ html: response.responseText, classes: 'rounded green' });

                    if (response.nova) {
                        var lItem = $(".collection-item.active");

                        lItem.attr("data-codigo-pergunta", response.perguntaCodigo);

                        var lTextoItem = lItem.text().split('-')[0];

                        lItem.text(lTextoItem + " - " + response.descricao)
                    }
                    else {
                        var lItem = $(".collection-item.active");

                        var lTextoItem = lItem.text().split('-')[0];

                        lItem.text(lTextoItem + " - " + response.descricao)
                    }
                }
                else {
                    M.toast({ html: response.responseText, classes: 'rounded red' });
                }
            },
            submitting: function () {
                desabilitarBotoes();
            },
            submitted: function () {
                habilitarBotoes();
            }
        });
    }

    function editarPergunta() {
        if ($("#frmEditarPergunta").valid()) {
            $("#frmEditarPergunta").submit();
        }
    }

    function excluirPergunta() {
        var lsUrl = $(this).data('ajax-url');

        MaterialDialog.dialog(
            "Deseja realmente excluir?",
            {
                title: "Confirmação",
                buttons: {
                    close: {
                        className: "red",
                        text: "Não"
                    },
                    confirm: {
                        className: "green",
                        text: "Sim",
                        callback: function () {
                            $('.progress').show();
                            $.ajax({
                                type: "POST",
                                url: lsUrl,
                                success: function (response) {
                                    if (response.success) {
                                        M.toast({ html: response.responseText, classes: 'rounded green' });
                                        $(".collection-item.active").remove();
                                        $("#div-pergunta").empty();
                                    }
                                    else {
                                        M.toast({ html: response.responseText, classes: 'rounded red' });
                                    }
                                }
                            }).always(function () {
                                $('.progress').hide();
                            });
                        }
                    }
                }
            }
        );
    }

    function configuraInputImagem() {
        var lImagem = $("#img-inserida");

        if (lImagem.length) {
            $(".div-midia").css('border', 'none');
            $("#btn-excluir-midia").css('display', 'inline-block');
        }

        $("#img-pergunta").change(function () {
            var lFile = this.files[0];
            var lReader = new FileReader();

            lReader.onloadend = function () {
                $("#btn-excluir-midia").css('display', 'inline-block');
                $("#Imagem").val(lReader.result);
                $("#URLYoutube").val(null);
                $(".div-midia").css('border', 'none');
                $(".div-midia").empty();
                $(".div-midia").prepend('<img id="img-inserida" src="" />');
                $("#img-inserida").attr("src", lReader.result);
                $("#ImagemByte").val(null);
            }

            if (lFile) {
                lReader.readAsDataURL(lFile);
            }
        });
    }

    function desabilitarBotoes()
    {
        $('html').css('cursor', 'not-allowed');
        $('.progress').show();
        $("a").addClass("a-disabled");
    }

    function habilitarBotoes() {
        $('html').css('cursor', 'default');
        $('.progress').hide();
        $("a").removeClass("a-disabled");
    }

    function configuraInputUrlYoutube()
    {
        var lVideo = $("#videoYoutube");

        if (lVideo.length) {
            $(".div-midia").css('border', 'none');
            $("#btn-excluir-midia").css('display', 'inline-block');
        }

        $("#btn-adicionar-video").click(function () {
            var url = $('#LinkYoutube').val();
            if (url != undefined || url != '') {
                var regExp = /^.*(youtu.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=|\?v=)([^#\&\?]*).*/;
                var match = url.match(regExp);
                if (match && match[2].length == 11) {
                    $("#btn-excluir-midia").css('display', 'inline-block');
                    $("#Imagem").val(null);
                    $("#URLYoutube").val('https://www.youtube.com/embed/' + match[2] + '?enablejsapi=1');
                    $(".div-midia").css('border', 'none');
                    $(".div-midia").empty();
                    $(".div-midia").prepend('<iframe id="videoYoutube" type="text/html" width="700" height="345" frameborder="0" allowfullscreen></iframe>');
                    $('#videoYoutube').attr('src', 'https://www.youtube.com/embed/' + match[2] + '?autoplay=1&enablejsapi=1');
                    $("#ImagemByte").val(null);
                } else {
                    M.toast({ html: "Link inserido é inválido", classes: 'rounded red' });
                }
            }
        });
    }

    function limparMidia()
    {
        $("#Imagem").val(null);
        $("#URLYoutube").val(null);
        $(".div-midia").empty();
        $(".div-midia").css('border', '4px dotted grey');
    }

    function validaUsuarioCriador()
    {
        if ($("#UsuarioCriador").val() == "False")
        {
            $("input").attr("disabled", true);
            $(".materialize-textarea").attr("disabled", true);
            $("#btn-adicionar-video").addClass("disabled");
        }
    }
});