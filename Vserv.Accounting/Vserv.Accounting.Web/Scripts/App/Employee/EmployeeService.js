﻿(function () {
    'use strict';
    window.app.service('employeeService', employeeService);
    employeeService.$inject = ['serviceHandler'];

    function employeeService(serviceHandler) {
        var employeePaySheet = [];
        var employeeAppraisalHistory = [];
        var employeeChangeHistory = [];

        var svc = {
            addEmployeeSalaryDetail: addEmployeeSalaryDetail,
            getEmpFinancialYears: getEmpFinancialYears,
            getEmployee: getEmployee,

            loadEmployeeAppraisalHistory: loadEmployeeAppraisalHistory,
            employeeAppraisalHistory: employeeAppraisalHistory,
            loadEmployeePaySheet: loadEmployeePaySheet,
            employeePaySheet: employeePaySheet,
            updateYearlyPaySheet: updateYearlyPaySheet,

            loadEmployeeChangeHistory: loadEmployeeChangeHistory,
            employeeChangeHistory: employeeChangeHistory,

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

        function getEmpFinancialYears(joiningDate, relievingDate) {
            var currentYear = relievingDate == null || relievingDate == 'Invalid Date' ? parseInt(moment().year()) : parseInt(moment(relievingDate).year());
            var joiningYear = parseInt(moment(joiningDate).year());
            var financialYears = [];

            for (var i = currentYear; i >= joiningYear; i--) {
                financialYears.push({ currentYear: i, financialYear: i + '-' + (i + 1) });
            }

            return financialYears;
        }

        function loadEmployeeChangeHistory(employeeId) {
            serviceHandler.executePostService('/Employee/LoadEmployeeChangeHistory?employeeId=' + employeeId).then(function (resp) {
                if (resp.businessException == null) {
                    if (resp.result.length > 0) {
                        employeeChangeHistory.removeAll();// clear all the existing items.
                        $.map(resp.result, function (val, i) {
                            val.UpdatedDate = moment(val.UpdatedDate).format("DD/MM/YYYY");
                        });

                        employeeChangeHistory.addRange(resp.result);
                    }
                }
                else {
                    $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
                }
            });
        }
    }
})();