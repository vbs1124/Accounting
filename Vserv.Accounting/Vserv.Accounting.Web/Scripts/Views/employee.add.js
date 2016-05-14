$(function () {
    //Initialise any date pickers
    $("#dp-birth-date").datetimepicker({
        format: "MM/DD/YYYY",
        showTodayButton: true,
        showClear: true,
        showClose: true
    });

    $("#dp-relieving-date").datetimepicker({
        format: "MM/DD/YYYY",
        showTodayButton: true,
        showClear: true,
        showClose: true
    });

    $("#dp-joining-date").datetimepicker({
        format: "MM/DD/YYYY",
        showTodayButton: true,
        showClear: true,
        showClose: true
    });

    $("#dp-joining-date").on("dp.change", function (e) {
        $('#dp-relieving-date').data("DateTimePicker").minDate(e.date);
        $('#dp-relieving-date').data("DateTimePicker").clear();
    });
    $("#dp-relieving-date").on("dp.change", function (e) {
        $('#dp-joining-date').data("DateTimePicker").maxDate(e.date);
    });
});