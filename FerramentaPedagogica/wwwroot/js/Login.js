$(document).ready(function () {
    initCadastrarUsuario();
    initLogarUsuario();
    $(".botao-cadastrar").click(cadastrarUsuario);
    $(".botao-login").click(logarUsuario);

    function initCadastrarUsuario()
    {
        $("#frmCadastroUsuario").ajaxForm({
            success: function (response){
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

    function cadastrarUsuario()
    {
        if ($("#frmCadastroUsuario").valid())
        {
            $("#frmCadastroUsuario").submit();
        }
    }

    function initLogarUsuario() {
        $("#frmLogarUsuario").ajaxForm({
            success: function (response) {
                if (response.success) {
                    window.location.href = response.url;
                }
                else
                {
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

    function logarUsuario() {
        if ($("#frmLogarUsuario").valid())
        {
            $("#frmLogarUsuario").submit();
        }
    }
});