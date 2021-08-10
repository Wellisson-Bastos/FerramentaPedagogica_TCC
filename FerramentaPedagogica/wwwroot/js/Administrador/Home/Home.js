$(document).ready(function () {
	$(".botao-excluir-jogo").click(excluirJogo);
	$(".botao-jogar-jogo").click(iniciarJogo);

    function excluirJogo()
	{
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

										window.setTimeout(function () { location.reload() }, 2000)
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

	function iniciarJogo() {
		var lsUrl = $(this).data('ajax-url');

		$.get(lsUrl, function (response)
		{
			if (response.success) {
				window.location.href = response.url;
			}
			else {
				M.toast({ html: response.responseText, classes: 'rounded red' });
			}
		})
	}
});