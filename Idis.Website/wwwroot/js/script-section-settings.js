function UserDelete(id) {
    $.post("/UserDelete", {
        userId: id
    }).done(function (data) {
        if (data)
            window.location = '/Authentication';
    }).fail(function () {
        alert("Error");
    });
}

$(document).on('ready', function () {

    $('body').attr('data-offset', '400')
    $('body').attr('data-hs-scrollspy-options', '{"target": "#navbarSettings"}')


    // INITIALIZATION OF MEGA MENU
    // =======================================================
    var megaMenu = new HSMegaMenu($('.js-mega-menu'), {
        desktop: {
            position: 'left'
        }
    }).init();


    // INITIALIZATION OF STICKY BLOCKS
    // =======================================================
    $('.js-sticky-block').each(function () {
        var stickyBlock = new HSStickyBlock($(this), {
            targetSelector: $('#header').hasClass('navbar-fixed') ? '#header' : null
        }).init();
    });


    // INITIALIZATION OF SCROLL NAV
    // =======================================================
    var scrollspy = new HSScrollspy($('body'), {
        // !SETTING "resolve" PARAMETER AND RETURNING "resolve('completed')" IS REQUIRED
        beforeScroll: function (resolve) {
            if (window.innerWidth < 992) {
                $('#navbarVerticalNavMenu').collapse('hide').on('hidden.bs.collapse', function () {
                    return resolve('completed');
                });
            } else {
                return resolve('completed');
            }
        }
    }).init();


    // INITIALIZATION OF PASSWORD STRENGTH MODULE
    // =======================================================
    $('.js-pwstrength').each(function () {
        var pwstrength = $.HSCore.components.HSPWStrength.init($(this));
    });
});