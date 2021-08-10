$(document).ready(function () {
	$(".botao-jogar-jogo").click(iniciarJogo);

	function iniciarJogo() {
		var lsUrl = $(this).data('ajax-url');

		$.get(lsUrl, function (response) {
			if (response.success) {
				window.location.href = response.url;
			}
			else {
				M.toast({ html: response.responseText, classes: 'rounded red' });
			}
		})
	}
});