(function () {
    'use strict';
    window.app.factory('AccountService', AccountService);
    AccountService.$inject = ['serviceHandler'];

    function AccountService(serviceHandler) {
        var securityQuestions = [];
        loadsecurityQuestions();

        var svc = {
            securityQuestions: securityQuestions,
        };

        return svc;

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
    }
})();