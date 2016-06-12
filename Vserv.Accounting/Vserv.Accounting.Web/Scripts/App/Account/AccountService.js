(function () {
    "use strict";

    function accountService(serviceHandler) {
        var securityQuestions = [];

        function loadsecurityQuestions() {
            serviceHandler.executePostService('/Account/GetSecurityQuestions').then(function (resp) {
                if (resp.businessException == null) {
                    securityQuestions.addRange(resp.result);
                }
                else {
                    toastr.error("Error!", resp.businessException.ExceptionMessage);
                }
            });
        }

        loadsecurityQuestions();

        var svc = {
            securityQuestions: securityQuestions
        };

        return svc;
    }

    window.app.factory("AccountService", accountService);
    accountService.$inject = ["serviceHandler"];
})();