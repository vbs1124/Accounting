(function () {
    'use strict';
    window.app.service('EmpSalaryBreakupService', EmpSalaryBreakupService);
    EmpSalaryBreakupService.$inject = ['serviceHandler'];
    function EmpSalaryBreakupService(serviceHandler) {
        var salaryBreakups = [];
        var svc = {
            //add: add,
            //update: update,
            salaryBreakups: salaryBreakups,
            getYearlyPaySheet: getYearlyPaySheet
        };

        return svc;

        function getYearlyPaySheet(paySheetParameter) {
            return serviceHandler.executePostService('/Employee/GetYearlyPaySheet', paySheetParameter);
        }
    }
})();