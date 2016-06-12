function onclick_AddDesignation(event) {
    if ($("#cmbPopupDesgName").prop("disabled")) {
        addDesignation();
    } else {
        var designationName = $("#txtDesignationName").val();
        var designationId = $("#cmbPopupDesgName").val();
        if (designationName.length > 0 && designationId > 0) {
            if (confirm("Are you sure you want to update existing designation?")) {
                updateDesignation();
            } else {
                event.preventDefault();
                return false;
            }
        } else {
            toastr.error("Designation Name is required.", "Error!");
            return false;
        }
    }
    return false;
}

$(function () {
    $("#dataTables-designation").DataTable({
        responsive: true,
        select: true,
        processing: true
    });

    $("#cmbPopupDesgName").prop("disabled", true);

    $("#cmbPopupDesgName").on("change", function () {
        var selectedDesignation = this.options[this.selectedIndex].innerHTML;
        $("#txtDesignationName").val(selectedDesignation);
    });
});

function refreshDesignationDropdownList() {
    $.getJSON(window.VservApp.rootPath + "Designation/GetDesignations", function (data) {
        var result = "";

        for (var i = 0, iL = data.length; i < iL; i++) {
            result += "<option value=\"" + data[i].Value + "\">" + data[i].Text + "</option>";
        }
        $("#cmbDesignation").html(result);
    });
}

function onclick_linkEditDesignation() {
    $("#cmbPopupDesgName").prop("disabled", !$("#cmbPopupDesgName").prop("disabled"));
}

function addDesignation() {
    var designationName = $("#txtDesignationName").val();
    if (designationName.length > 0) {
        $.ajax({
            url: window.VservApp.rootPath + "Designation/Add",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ designationName: designationName }),
            success: function (result) {
                if (result) {
                    refreshDesignationDropdownList(); // Refresh the designation dropdown.
                    $("#modal-add-designation").modal("hide");
                    toastr.success("Designation added successfully.", "Success!");
                }
            }
        }).done(function () {

        }).fail(function () {

            toastr.error("Unable to save the requested records.", "Error!");
        }).always(function () {

        });
    }
    else {
        toastr.error("Designation Name is required.", "Error!");
        return false;
    }
    return false;
}

function updateDesignation() {
    var designationName = $("#txtDesignationName").val();
    var designationId = $("#cmbPopupDesgName").val();
    if (designationName.length > 0 && designationId > 0) {
        $.ajax({
            url: window.VservApp.rootPath + "Designation/Edit",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ designationName: designationName, designationId: designationId }),
            success: function (result) {
                if (result) {
                    refreshDesignationDropdownList(); // Refresh the designation dropdown.
                    $("#modal-add-designation").modal("hide");
                    toastr.success("Designation updated successfully.", "Success!");
                } else {
                    toastr.error("Failed to update selected designation.", "Error!");
                    return false;
                }
                return false;
            }
        }).done(function () {

        }).fail(function () {
            toastr.error("Unable to save the requested records.", 'Error!');
        }).always(function () {

        });
    }
    else {
        toastr.error("Designation Name is required.", "Error!");
        return false;
    }
    return false;
}