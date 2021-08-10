$(document).ready(function () {
    initDadosJogo();
    $(".botao-iniciar").click(iniciarJogo);

    function initDadosJogo() {
        $("#frmDadosJogo").ajaxForm({
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

    function iniciarJogo() {
        if ($("#frmDadosJogo").valid()) {
            $("#frmDadosJogo").submit();
        }
    }
});