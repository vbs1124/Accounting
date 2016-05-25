$(function () {
    $(".mailing-geo-field").geocomplete({
        details: "#fieldset-mailing-address",
        detailsAttribute: "data-geo"
    });

    $(".permanent-geo-field").geocomplete({
        details: "#fieldset-permanent-address",
        detailsAttribute: "data-geo"
    });

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

    $("#dp-resignation-date").datetimepicker({
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
        $('#dp-resignation-date').data("DateTimePicker").maxDate(e.date);
    });

    // Reset form controls once the modal is closed.
    $('#modal-add-designation').on('hidden.bs.modal', function (e) {
        $(this)
          //.find("input,textarea,select")
            .find("input,textarea")
             .val('')
             .end()
          .find("input[type=checkbox], input[type=radio]")
             .prop("checked", "")
             .end();
    });
});

function onchange_copyPermanentAdds(event, args) {
    if (event.checked) {
        $("#MailingAddress1").val($("#PermanentAddress1").val());
        $("#MailingAddress2").val($("#PermanentAddress2").val());
        $("#MailingZipCode").val($("#PermanentZipCode").val());
        $("#MailingCity").val($("#PermanentCity").val());
        $("#MailingStateId").val($("#PermanentStateId").val());
    } else {
        $("#MailingAddress1").val("");
        $("#MailingAddress2").val("");
        $("#MailingZipCode").val("");
        $("#MailingCity").val("");
    }
}