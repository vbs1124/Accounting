(function () {
    'use strict';
    window.app.service('EmployeeService', EmployeeService);
    EmployeeService.$inject = ['serviceHandler'];
    function EmployeeService(serviceHandler) {

        var salarySummaryModel = {};
        loadSalarySummaryModel();

        var svc = {
            add: add,
            salarySummaryModel: salarySummaryModel,
        };

        return svc;

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

        function add(salarySummaryModel, employeeId) {
            return serviceHandler.executePostService('/Employee/SaveEmployeeSalaryDetail?employeeId=' + employeeId, salarySummaryModel);
        }
    }
})();