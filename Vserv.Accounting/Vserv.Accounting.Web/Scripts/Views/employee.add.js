$(function () {
    //Initialise any date pickers
    $("#dp-birth-date").datetimepicker({
        format: "MM/DD/YYYY",
        showTodayButton: true,
        showClear: true,
        showClose: true,
        maxDate: moment().subtract(18, 'months')
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
        showClose: true,
        maxDate: moment()
    });

    $("#dp-joining-date").on("dp.change", function (e) {
        $('#dp-relieving-date').data("DateTimePicker").minDate(e.date);
        $('#dp-relieving-date').data("DateTimePicker").clear();
    });
    $("#dp-relieving-date").on("dp.change", function (e) {
        $('#dp-joining-date').data("DateTimePicker").maxDate(e.date);
    });
});

function onchange_copyPermanentAdds(event, args) {
    if (event.checked) {
        $("#MailingAddress_Address1").val($("#PermanentAddress_Address1").val());
        $("#MailingAddress_Address2").val($("#PermanentAddress_Address1").val());
        $("#MailingAddress_City").val($("#PermanentAddress_Address1").val());
        $("#MailingAddress_ZipCode").val($("#PermanentAddress_ZipCode").val());
        $("#MailingAddress_StateId").val($("#PermanentAddress_StateId").val());
    } else {
        $("#MailingAddress_Address1").val("");
        $("#MailingAddress_Address2").val("");
        $("#MailingAddress_City").val("");
        $("#MailingAddress_ZipCode").val("");
        //$("#MailingAddress_StateId").val("");
    }
}