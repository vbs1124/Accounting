(function () {
    'use strict';

    function empSalarySummaryService(serviceHandler) {
        function saveEmployeeSalaryDetail(salarySummaryModel) {
            return serviceHandler.executePostService('/Employee/SaveEmployeeSalaryDetail', salarySummaryModel);
        }

        var svc = {
            saveEmployeeSalaryDetail: saveEmployeeSalaryDetail
        };

        return svc;
    }

    window.app.service('EmpSalarySummaryService', empSalarySummaryService);
    empSalarySummaryService.$inject = ['serviceHandler'];
})();