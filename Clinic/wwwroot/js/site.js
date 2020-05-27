// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function () {
    jQuery.datetimepicker.setLocale('en');

    const tomorrow = new Date('DD/MM/YYYY');

    // add 1 day to today
    tomorrow.setDate(new Date().getDate() + 1);

    console.log(tomorrow);
// 2019-12-28T10:46:44.105Z

    jQuery('#datetimepicker').datetimepicker({
        allowTimes: [
            '8:00', '9:00', '10:00',
            '11:00', '12:00', '13:00', '14:00', '15:00', '16:00'],
        formatDate: 'd/m/Y',
        minDate: '0',//yesterday is minimum date(for today use 0 or -1970/01/01)
        maxDate: '01/01/2025'//tomorrow is maximum date calendar
    });


});
