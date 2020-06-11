$(document).ready(function () {
    initCadastrarUsuario();
    $(".botao-cadastrar").click(cadastrarUsuario);

    function initCadastrarUsuario()
    {
        $("#frmCadastroUsuario").ajaxForm({
            success: function (url)
            {
                window.location.href = url;

                $('#frmCadastroUsuario').each(function () {
                    this.reset();
                });
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
});