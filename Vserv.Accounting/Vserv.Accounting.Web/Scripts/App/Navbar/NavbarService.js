(function () {
    'use strict';

    function navbarService(serviceHandler) {
        var features = [];

        function loadFeatures() {
            serviceHandler.executePostService('/Navbar/GetFeatures').then(function (resp) {
                if (resp.businessException == null) {
                    securityQuestions.addRange(resp.result);
                }
                else {
                    toastr.error("Error!", resp.businessException.ExceptionMessage);
                }
            });
        }

        loadFeatures();

        var svc = {
            features: features
        };

        return svc;
    }

    window.app.factory('NavbarService', navbarService);
    navbarService.$inject = ['serviceHandler'];
})();