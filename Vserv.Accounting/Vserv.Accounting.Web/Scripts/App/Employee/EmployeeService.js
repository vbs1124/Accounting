(function () {
    'use strict';
    window.app.service('employeeService', employeeService);
    employeeService.$inject = ['serviceHandler'];

    function employeeService(serviceHandler) {
        var employeePaySheet = [];
        var employeeAppraisalHistory = [];

        var svc = {
            addEmployeeSalaryDetail: addEmployeeSalaryDetail,
            getFinancialYears: getFinancialYears,
            getEmployee: getEmployee,

            loadEmployeeAppraisalHistory: loadEmployeeAppraisalHistory,
            employeeAppraisalHistory: employeeAppraisalHistory,
            loadEmployeePaySheet: loadEmployeePaySheet,
            employeePaySheet: employeePaySheet,
            updateYearlyPaySheet: updateYearlyPaySheet
        };

        return svc;

        function addEmployeeSalaryDetail(empSalaryStructureModel, employeeId) {
            return serviceHandler.executePostService('/Employee/SaveEmployeeSalaryDetail?employeeId=' + employeeId, empSalaryStructureModel);
        }

        function loadEmployeeAppraisalHistory(employeeId) {
            serviceHandler.executePostService('/Employee/GetEmployeeAppraisalHistory?employeeId=' + employeeId).then(function (resp) {
                if (resp.businessException == null) {

                    $.map(resp.result, function (val, i) {
                        val.CurrentEffectiveFrom = moment(val.CurrentEffectiveFrom).format("DD/MM/YYYY");
                    });

                    if (resp.result.length > 0)
                        angular.extend(employeeAppraisalHistory, resp.result);
                }
                else {
                    $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
                }
            });
        }

        function getEmployee(employeeId) {
            return serviceHandler.executePostService('/Employee/GetEmployee?employeeId=' + employeeId);
        }

        function loadEmployeePaySheet(employeeId, financialYearFrom, financialYearTo) {

            var paySheetParameter = {
                EmployeeId: employeeId,
                FinancialYearFrom: financialYearFrom,
                FinancialYearTo: financialYearTo
            };

            serviceHandler.executePostService('/Employee/GetYearlyPaySheet', paySheetParameter).then(function (resp) {
                if (resp.businessException == null) {
                    employeePaySheet.removeAll();// clear all the existing items.
                    employeePaySheet.addRange(resp.result);
                }
                else {
                    $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
                }
            });
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