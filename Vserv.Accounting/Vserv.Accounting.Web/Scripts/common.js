/// <reference path="toastr.js" />
$(function () {
    $('#side-menu').metisMenu();
    $("[data-toggle=popover]").popover();

    (function (vbs) {
        var viewModelHelper = function () {
            var self = this;
            self.isLoading = function (isLoaderEnabled) {

            }
            self.apiGet = function (url, data, success, error) {
                self.isLoading(true);
                self.errorMessage = "";
                $.ajax({
                    url: url,
                    type: "GET",
                    data: data,
                    dataType: "json",
                    traditional: true,
                    contentType: "application/json; charset=utf-8",
                    success: success,
                    error: error
                });

                //$.get(VservApp.rootPath + uri, data)
                //    .done(success)
                //    .fail(function (result) {
                //        if (failure == null) {
                //            if (result.status != 400) {
                //                self.errorMessage = result.status + ':' + result.statusText + ' - ' + result.responseText;
                //            }
                //            else {
                //                self.errorMessage = JSON.parse(result.responseText);
                //            }
                //        }
                //        else
                //            failure(result);
                //    })
                //    .always(function () {
                //        if (always == null) {
                //            self.isLoading(true);
                //        }
                //        else {
                //            always();
                //        }
                //    });
            };

            self.apiPost = function (uri, data, success, failure, always) {
                self.isLoading(true);
                $.post(VservApp.rootPath + uri, data)
                    .done(success)
                    .fail(function (result) {
                        if (failure == null) {
                            if (result.status != 400)
                                self.errorMessage = result.status + ':' + result.statusText + ' - ' + result.responseText;
                            else
                                self.errorMessage = JSON.parse(result.responseText);
                        }
                        else
                            failure(result);
                    })
                    .always(function () {
                        if (always == null)
                            self.isLoading(false);
                        else
                            always();
                    });
            };
        }

        vbs.viewModelHelper = viewModelHelper;
    }(window.VservApp));

    //toastr.options.timeOut = 500; // 1.5s
    toastr.options = { "positionClass": "toast-top-full-width" };
});

//Loads the correct sidebar on window load,
//collapses the sidebar on window resize.
// Sets the min-height of #page-wrapper to window size
$(function () {
    $(window).bind("load resize", function () {
        topOffset = 50;
        width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
        if (width < 768) {
            $('div.navbar-collapse').addClass('collapse');
            topOffset = 100; // 2-row-menu
        } else {
            $('div.navbar-collapse').removeClass('collapse');
        }

        height = ((this.window.innerHeight > 0) ? this.window.innerHeight : this.screen.height) - 1;
        height = height - topOffset;
        if (height < 1) height = 1;
        if (height > topOffset) {
            $("#page-wrapper").css("min-height", (height) + "px");
        }
    });

    var url = window.location;
    var element = $('ul.nav a').filter(function () {
        return this.href == url || url.href.indexOf(this.href) == 0;
    }).addClass('active').parent().parent().addClass('in').parent();
    if (element.is('li')) {
        element.addClass('active');
    }
});


function showError(divid) {
    if (divid) {
        $("#" + divid).html("Some error occurred! Please try again.");
        $("#" + divid).attr("class", "alert alert-danger");
    }
    else {
        alert("Some error occurred! Please try again.");
    }
}
var parseBool = function (str) {
    if (str == null)
        return false;

    if (typeof str === 'boolean') {
        if (str === true)
            return true;

        return false;
    }

    if (typeof str === 'string') {
        if (str == "")
            return false;

        str = str.replace(/^\s+|\s+$/g, '');
        if (str.toLowerCase() == 'true' || str.toLowerCase() == 'yes')
            return true;

        str = str.replace(/,/g, '.');
        str = str.replace(/^\s*\-\s*/g, '-');
    }

    // var isNum = string.match(/^[0-9]+$/) != null;
    // var isNum = /^\d+$/.test(str);
    if (!isNaN(str))
        return (parseFloat(str) != 0);

    return false;
}
function showMessage(divid, message, mode) {
    /*success
    info
    warning
    danger*/
    if ($("#" + divid)) {
        $("#" + divid).attr("class", "alert alert-" + mode);
        $("#" + divid).html(message)
    }
    else {
        alert(message);
    }
}
function showException(exceptionObject, status, headers, config) {
    // To be implemented 
    //if(status=="404") invalid URL, Page not found
    //if(status=="401") Unauthorized, Session Expired
    //if(status=="500") Internal Error
    if (status == 401) // session expired
        location.href = "/dashboard/index?ReturnUrl=" + location.pathname;
    else {
        var message = "<div class='text-danger'>";
        message += "<h1>Oops some thing went wrong</h1>";
        message += "<p>Sorry, we are not able to process your reqiest due to some unexpected error...</p>";
        message += "<p>Please re-check if you can perform the same by doing the action again. Click here if you want to go to <a href='/'>Homepage</a>?</p>";

        // more details of exception
        if (exceptionObject.ExceptionMessage)
            message += "<div class='text-info'>Server returned : <br/>" + exceptionObject.ExceptionMessage + "</div>";
        else if (exceptionObject.Message)
            message += "<div class='text-info'>" + exceptionObject.Message + "</div>";
        message += "</div>";
        bootbox.alert(message);
    }

}



function checkActiveDisplay(status) {
    if (status == true)
        return "<span class='glyphicon glyphicon-ok'></span>";
    else
        return "<span class='glyphicon glyphicon-remove'></span>";
}

function setModalDialogData(modalContent) {
    $("#panelModalContent").html(modalContent);
}
function nullToBlank(str) {
    if (!str)
        return "";
    else
        return str;
}
function showModalDialog(title) {
    $("#modalTitle").html(title);
    $('#panelModal').modal({
        keyboard: false,
        backdrop: 'static'

    })
}
function bindEnterEventOnTable(tableId, tableObject) {
    $('#' + tableId + '_filter input').unbind();
    $('#' + tableId + '_filter input').bind('keyup', function (e) {
        if (e.keyCode == 13)
            tableObject.fnFilter(this.value);
    });
}

$.showToastrMessage = function (messageType, message, title, optionsOverride) {
    if (messageType == "success") {
        toastr.success(message, title, optionsOverride);
    }
    else if (messageType == "error") {
        toastr.error(message, title, optionsOverride);
    }
    else if (messageType == "info") {
        toastr.info(message, title, optionsOverride);
    }
    else if (messageType == "warning") {
        toastr.warning(message, title, optionsOverride);
    }
};

$.vbsParseFloat = function (value) {
    if (isNaN(value) || value == "") {
        return 0;
    }
    return parseFloat(value);;
}
