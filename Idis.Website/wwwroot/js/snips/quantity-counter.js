// INITIALIZATION OF QUANTITY COUNTER
// =======================================================
$('.js-quantity-counter').each(function () {
    var quantityCounter = new HSQuantityCounter($(this)).init();
});