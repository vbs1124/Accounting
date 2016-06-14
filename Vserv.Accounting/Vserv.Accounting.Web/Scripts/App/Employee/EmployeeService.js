(function () {
    'use strict';
    window.app.service('employeeService', employeeService);
    employeeService.$inject = ['serviceHandler'];

    function employeeService(serviceHandler) {
        var yearlyPaySheets = [];
        var employeeAppraisalHistory = [];

        var svc = {
            addEmployeeSalaryDetail: addEmployeeSalaryDetail,
            loadEmployeeAppraisalHistory: loadEmployeeAppraisalHistory,
            loadYearlyPaySheet: loadYearlyPaySheet,
            yearlyPaySheets: yearlyPaySheets,
            getFinancialYears: getFinancialYears,
            getEmployee: getEmployee,
            employeeAppraisalHistory: employeeAppraisalHistory,
            updateYearlyPaySheet: updateYearlyPaySheet
        };

        return svc;

        function addEmployeeSalaryDetail(empSalaryStructureModel, employeeId) {
            return serviceHandler.executePostService('/Employee/SaveEmployeeSalaryDetail?employeeId=' + employeeId, empSalaryStructureModel);
        }

        function loadEmployeeAppraisalHistory(employeeId) {
            return serviceHandler.executePostService('/Employee/GetEmployeeAppraisalHistory?employeeId=' + employeeId);
        }

        function getEmployee(employeeId) {
            return serviceHandler.executePostService('/Employee/GetEmployee?employeeId=' + employeeId);
        }

        function loadYearlyPaySheet(paySheetParameter) {
            return serviceHandler.executePostService('/Employee/GetYearlyPaySheet', paySheetParameter);
        }

        function updateYearlyPaySheet(paysheets) {
            return serviceHandler.executePostService('/Employee/UpdateYearlyPaySheet', paysheets);
        }

        function getFinancialYears() {
            var currentYear = moment().year();
            var financialYears = [];
            for (var i = currentYear + 1 ; i > currentYear - 9; i--) {
                financialYears.push({ currentYear: i - 1, financialYear: i - 1 + '-' + i });
            }
            return financialYears;
        }
    }
})();