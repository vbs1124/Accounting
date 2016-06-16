$(function () {

    //$(".mailing-geo-field").geocomplete({
    //    details: "#fieldset-mailing-address",
    //    detailsAttribute: "data-geo"
    //}).bind("geocode:result", function (event, result) {
    //    console.log(result);
    //    var selectedMailingCity = $("#MailingCity").val();
    //    if (selectedMailingCity) {
    //        $.selectMailingStateByCityName(selectedMailingCity);
    //    }
    //});

    //$(".permanent-geo-field").geocomplete({
    //    details: "#fieldset-permanent-address",
    //    detailsAttribute: "data-geo"
    //}).bind("geocode:result", function (event, result) {
    //    console.log(result);
    //    var selectedPermanentCity = $("#PermanentCity").val();
    //    if (selectedPermanentCity) {
    //        $.selectPermanentStateByCityName(selectedPermanentCity);
    //    }
    //});

    //Initialize any date pickers
    $('#dp-birth-date').datepicker({
        autoclose: true,
        clearBtn: true,
        enableOnReadonly: false,
        assumeNearbyYear: true,
        endDate: new Date(moment().subtract(18, "years"))
    });

    $('#dp-relieving-date').datepicker({
        autoclose: true
    }).on("changeDate", function (e) {
        var endDate = new Date(e.date.valueOf());
        $("#dp-joining-date").datepicker("setEndDate", endDate);
        $("#dp-resignation-date").datepicker("setEndDate", endDate);
    });

    $('#dp-resignation-date').datepicker({
        autoclose: true
    });

    $('#dp-joining-date').datepicker({
        autoclose: true
    }).on("changeDate", function (e) {
        var minDate = new Date(e.date.valueOf());
        $("#dp-relieving-date").datepicker("setStartDate", minDate);
        $("#dp-resignation-date").datepicker("setStartDate", minDate);
        //    $("#dp-relieving-date").data("DateTimePicker").clear();
    });

    // Reset form controls once the modal is closed.
    $("#modal-add-designation").on("hidden.bs.modal", function () {
        $(this)
          //.find("input,textarea,select")
            .find("input,textarea")
             .val("")
             .end()
          .find("input[type=checkbox], input[type=radio]")
             .prop("checked", "")
             .end();
    });

    $("#cmbEmployeeFilter").on("change", function () {
        var filterChoice = $("#cmbEmployeeFilter").val();
        $("#div-employee-list").load(window.VservApp.rootPath + "employee/GetFilteredEmployees?filterChoice=" + filterChoice);
    });
});

function onchange_copyPermanentAdds(event) {
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

$.selectPermanentStateByCityName = function (selectedPermanentCity) {
    var viewModelHelper = new window.VservApp.viewModelHelper();
    var url = window.VservApp.rootPath + "employee/GetStateByCityName";
    viewModelHelper.apiGet(url, { cityName: selectedPermanentCity }, function (data) {
        $("#PermanentStateId").val(data);
    }, function () {
        toastr.error("An error has occurred while loading state!");
    });
};
$.selectMailingStateByCityName = function (selectedMailingCity) {
    var viewModelHelper = new window.VservApp.viewModelHelper();
    var url = window.VservApp.rootPath + "employee/GetStateByCityName";
    viewModelHelper.apiGet(url, { cityName: selectedMailingCity }, function (data) {
        $("#MailingStateId").val(data);
    }, function () {
        toastr.error("An error has occurred while loading state!");
    });
};