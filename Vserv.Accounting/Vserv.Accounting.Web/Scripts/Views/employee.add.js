$(function () {
    $("#dp-birth-date").datepicker({
        autoclose: true,
        defaultViewDate: "today",
        clearBtn: true,
        todayBtn: true,
        todayHighlight: true,
        format: "dd/mm/yyyy"
    }); //Initialise any date pickers

    $("#dp-relieving-date").datepicker({
        autoclose: true,
        defaultViewDate: "today",
        clearBtn: true,
        todayBtn: true,
        todayHighlight: true,
        format: "dd/mm/yyyy"
    }); //Initialise any date pickers

    $("#dp-joining-date").datepicker({
        autoclose: true,
        defaultViewDate: "today",
        clearBtn: true,
        todayBtn: true,
        todayHighlight: true,
        format: "dd/mm/yyyy"
    }); //Initialise any date pickers



    //$("#dp-joining-date").on("dp.change", function (e) {
    //    $('#dp-relieving-date').data("DateTimePicker").minDate(e.date);
    //});
    //$("#dp-relieving-date").on("dp.change", function (e) {
    //    $('#dp-joining-date').data("DateTimePicker").maxDate(e.date);
    //});
});