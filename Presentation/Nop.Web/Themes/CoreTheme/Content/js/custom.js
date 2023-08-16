/*!
 * nopAccelerate Core Theme v2.5.0 (http://themes.nopaccelerate.com/themes/nopaccelerate-core-theme/)
 * Copyright 2021 Xcellence-IT.
 * Licensed under LICENSETYPE (http://www.nopaccelerate.com/terms/)
 */

/*Custom js for stick header menu*/
//http://www.jqueryscript.net/menu/Sticky-Navigation-Bar-with-jQuery-Bootstrap.html

$(document).ready(function () {
    $(window).bind('scroll', function() {
        //var navHeight = $("nav.top-menu").height();
        var navHeight = $("div.header").height();
        var navWidth = $("div.header").width();
        ($(window).scrollTop() > navHeight) ? $('.navbar.navbar-inverse').addClass('goToTop').width(navWidth) : $('.navbar.navbar-inverse').removeClass('goToTop');
    });
});

$(window).resize(function () {    
    $(".navbar.navbar-inverse").width("100%");
});

$(document).ready(function () {
    $("#exit").on('click', function (e) {
        $('.responsive').hide();
        $('.master-wrapper-page').css('margin-top', '0');
        $('.header-links').css('margin-top', '20px');
    });
});

/* Custom Style for Collapse Sidebar Box */
$(document).ready(function () {
    $(".block .title").on('click', function () {
        var e = window, a = 'inner';
        if (!('innerWidth' in window)) {
            a = 'client';
            e = document.documentElement || document.body;
        }

        var result = { width: e[a + 'Width'], height: e[a + 'Height'] };

        if (result.width < 992) {
            $(this).siblings('.listbox').slideToggle('slow');
            $(this).toggleClass("arrow-up-down");
        }
    });
});