(function () {
    'use strict';

    function employeeService(serviceHandler) {

        var salarySummaryModel = {};

        function loadSalarySummaryModel() {
            return serviceHandler.executePostService('/Employee/GetSalarySummaryModel').then(function (resp) {
                if (resp.businessException == null) {
                    salarySummaryModel = resp.result;
                }
                else {
                    toastr.error("Error!", resp.businessException.ExceptionMessage);
                }
            });
        }

        loadSalarySummaryModel();

        function add(salarySummaryModel, employeeId) {
            return serviceHandler.executePostService('/Employee/SaveEmployeeSalaryDetail?employeeId=' + employeeId, salarySummaryModel);
        }

        var svc = {
            add: add,
            salarySummaryModel: salarySummaryModel
        };

        return svc;
    }

    window.app.service('EmployeeService', employeeService);
    employeeService.$inject = ['serviceHandler'];
})();