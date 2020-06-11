$(document).ready(function () {
    initDadosJogo();
    $(".botao-iniciar").click(iniciarJogo);

    function initDadosJogo() {
        $("#frmDadosJogo").ajaxForm({
            success: function () {
                M.toast("Funcionou");
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