$(document).ready(function () {
    $("#modal-dados-usuario, #modal-criar-jogo").modal();

    $(".div-abre-modal").click(carregaModalUsuario);
    $("#btn-criar-jogo").click(carregaModalJogo);

    function carregaModalUsuario()
    {
        var lsUrl = $(this).data("ajax-url");

        $('.progress').show();

        $.get(lsUrl, function (pHtml) {
            $("#modal-dados-usuario").html(pHtml);
            $("#modal-dados-usuario").modal("open");
            initAtualizarDados();
            configuraInputFoto();
            $("#botao-atualizar").click(atualizarDados);
        }).always(function () {
            $('.progress').hide();
        }); 
    }

    function initAtualizarDados() {
        $("#frmAtualizarDados").ajaxForm({
            success: function (response) {
                if (response.success) {
                    $("#modal-dados-usuario").modal('close');
                    M.toast({ html: response.responseText, classes: 'rounded green' });

                    window.setTimeout(function () { location.reload() }, 2000)
                }
                else {
                    M.toast({ html: response.responseText, classes: 'rounded red' });
                }
            },
            submitting: function () {
                $('.progress').show();
            },
            submitted: function () {
                $('.progress').hide();
            }
        });
    }

    function atualizarDados() {
        if ($("#frmAtualizarDados").valid()) {
            $("#frmAtualizarDados").submit();
        }
    }

    function carregaModalJogo() {
        var lsUrl = $(this).data("ajax-url");

        $('.progress').show();

        $.get(lsUrl, function (pHtml) {
            $("#modal-criar-jogo").html(pHtml);
            $("#modal-criar-jogo").modal("open");
            initCriarJogo();
            configuraInputCapa();
            $("#botao-criar-jogo").click(criarJogo);
        }).always(function () {
            $('.progress').hide();
        });
    }

    function initCriarJogo() {
        $("#frmCriarJogo").ajaxForm({
            success: function (response) {
                if (response.success) {
                    window.location.href = response.url;
                }
                else {
                    M.toast({ html: response.responseText, classes: 'rounded red' });
                }
            },
            submitting: function () {
                $('.progress').show();
            },
            submitted: function () {
                $('.progress').hide();
            }
        });
    }

    function criarJogo() {
        if ($("#frmCriarJogo").valid()) {
            $("#frmCriarJogo").submit();
        }
    }

    function configuraInputFoto() {
        $("#img-usuario").change(function (pImg) {
            var lFile = this.files[0];
            var lReader = new FileReader();

            lReader.onloadend = function () {
                $("#img-usuario").attr("src", lReader.result);
                $("#FotoBase64").val(lReader.result);
            }

            if (lFile) {
                lReader.readAsDataURL(lFile);
            }
        });
    }

    function configuraInputCapa() {
        $("#img-jogo").change(function (pImg) {
            var lFile = this.files[0];
            var lReader = new FileReader();

            lReader.onloadend = function () {
                $("#img-jogo").attr("src", lReader.result);
                $("#CapaBase64").val(lReader.result);
            }

            if (lFile) {
                lReader.readAsDataURL(lFile);
            }
        });
    }
});