(function () {
    'use strict';
    window.app.service('EmpSalarySummaryService', EmpSalarySummaryService);
    EmpSalarySummaryService.$inject = ['serviceHandler'];
    function EmpSalarySummaryService(serviceHandler) {
        var svc = {
            saveEmployeeSalaryDetail: saveEmployeeSalaryDetail
        };

        return svc;

        function saveEmployeeSalaryDetail(salarySummaryModel) {
            return serviceHandler.executePostService('/Employee/SaveEmployeeSalaryDetail', salarySummaryModel);
        }
    }
})();