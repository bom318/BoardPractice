$(document).ready(function () {

    //hover
    $('tr').has('td').hover(function () {
        $(this).css('background', '#f5f5f5');
    }, function () {
        $(this).css('background', '#fff');
    });

    //페이지 이동
    $('tr').has('td').on('click', function () {
        
        location.href = "/Main/Details?num=" + $(this).children('td').first().text();
    });

    //cursor
    $('tr').has('td').css('cursor', 'pointer');
});
