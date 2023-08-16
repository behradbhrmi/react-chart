(function ($, ssCore, ssEx) {

    window.themeSettings = {
        themeBreakpoint: 1024,
        isAccordionMenu: false
    };

    $(document).ready(function () {
        var newsItemWrapper = $('.news-items .item');

        if (newsItemWrapper.length % 2 === 0) {
            newsItemWrapper.addClass('even');
        }
        else if (newsItemWrapper.length === 1) {
            newsItemWrapper.addClass('one');
        }

        if ($('.home-page-banners').children().length < 1) {
            $('.home-page-listbox').addClass('without-banners');
        }

        var responsiveAppSettings = {
            isEnabled: true,
            themeBreakpoint: window.themeSettings.themeBreakpoint,
            isSearchBoxDetachable: true,
            isHeaderLinksWrapperDetachable: true,
            doesDesktopHeaderMenuStick: true,
            doesScrollAfterFiltration: true,
            doesSublistHasIndent: true,
            displayGoToTop: true,
            hasStickyNav: true,
            selectors: {
                menuTitle: ".menu-title",
                headerMenu: ".header-menu",
                closeMenu: ".close-menu",
                //movedElements: ".admin-header-links, .header-logo, .slider-wrapper, .header, sub-header, .responsive-nav-wrapper, .master-wrapper-content, .footer",
                sublist: ".header-menu .sublist",
                overlayOffCanvas: ".overlayOffCanvas",
                withSubcategories: ".with-subcategories",
                filtersContainer: ".nopAjaxFilters7Spikes",
                filtersOpener: ".filters-button span",
                searchBoxOpener: ".search-wrap > span",
                searchBox: ".search-box.store-search-box",
                searchBoxBefore: ".wishlist-cart-wrapper",
                navWrapper: ".responsive-nav-wrapper",
                navWrapperParent: ".responsive-nav-wrapper-parent",
                headerLinksOpener: "#header-links-opener",
                headerLinksWrapper: ".header-options-wrapper",
                shoppingCartLink: ".shopping-cart-link",
                overlayEffectDelay: 300
            }
        };

        function detachLogo() {
            if (ssCore.getViewPort().width <= responsiveAppSettings.themeBreakpoint) {
                $('.header-logo').detach().insertBefore('.master-wrapper-page .overlayOffCanvas');
            }
            else {
                $('.header-logo').detach().prependTo('.sub-header-center');
            }
        }

        ssEx.initResponsiveTheme(responsiveAppSettings);
        
        $(window).on("resize orientationchange", detachLogo);
        detachLogo();

        $(".footer-middle-block .title").click(function (e) {
            if (ssCore.getViewPort().width <= responsiveAppSettings.themeBreakpoint) {
                $(this).siblings("ul:nth-of-type(1)").slideToggle("slow");
            }
            else {
                e.preventDefault();
            }
        });
    });

    $(window).on('load', function () {
        $('.loader-overlay').hide();
    });
})(jQuery, sevenSpikesCore, sevenSpikesEx);