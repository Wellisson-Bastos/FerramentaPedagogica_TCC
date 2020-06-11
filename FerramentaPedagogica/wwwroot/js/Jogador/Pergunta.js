$(document).ready(function () {
    var time = 10;
    var initialOffset = '440';
    var i = 1;
    var lTimeController = time;

    /* Need initial run as interval hasn't yet occured... */
    $('.circle_animation').css('stroke-dashoffset', initialOffset - (1 * (initialOffset / time)));

    var interval = setInterval(function () {
        $("#tempo-pergunta").text(lTimeController - 1);
        if (i == time) {
            clearInterval(interval);
            return;
        }
        $('.circle_animation').css('stroke-dashoffset', initialOffset - ((i + 1) * (initialOffset / time)));
        i++;
        lTimeController--;
    }, 1000);
});