(function () {
    'use strict';

    function empSalaryBreakupService(serviceHandler) {
        var salaryBreakups = [];

        function getYearlyPaySheet(paySheetParameter) {
            return serviceHandler.executePostService('/Employee/GetYearlyPaySheet', paySheetParameter);
        }

        var svc = {
            //add: add,
            //update: update,
            salaryBreakups: salaryBreakups,
            getYearlyPaySheet: getYearlyPaySheet
        };

        return svc;
    }

    window.app.service('EmpSalaryBreakupService', empSalaryBreakupService);
    empSalaryBreakupService.$inject = ['serviceHandler'];
})();