(function () {
    'use strict';
    window.app.factory('NavbarService', NavbarService);
    NavbarService.$inject = ['serviceHandler'];

    function NavbarService(serviceHandler) {
        var features = [];
        loadFeatures();

        var svc = {
            features: features,
        };

        return svc;

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
    }
})();