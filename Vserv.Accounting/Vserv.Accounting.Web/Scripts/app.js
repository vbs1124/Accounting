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
