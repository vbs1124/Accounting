(function () {
    'use strict';
    window.app.service('employeeService', employeeService);
    employeeService.$inject = ['serviceHandler'];

    function employeeService(serviceHandler) {
        var employeePaySheet = [];
        var employeeAppraisalHistory = [];
        var employeeChangeHistory = [];
        var empAppraisalGraphValues = [];
        var empSalaryStructureHistory = [];
        var investmentDeclarationResult = {};
        var empInvestmentDeclarationModel = [];
        var empsalarystructureid = null;
        var employee = {};

        var svc = {
            addEmployeeSalaryDetail: addEmployeeSalaryDetail,
            getEmpFinancialYears: getEmpFinancialYears,
            getEmployee: getEmployee,

            loadEmployeeAppraisalHistoryForGraph: loadEmployeeAppraisalHistoryForGraph,
            loadEmployeeAppraisalHistory: loadEmployeeAppraisalHistory,
            employeeAppraisalHistory: employeeAppraisalHistory,
            empAppraisalGraphValues: empAppraisalGraphValues,

            loadEmployeePaySheet: loadEmployeePaySheet,
            employeePaySheet: employeePaySheet,
            empsalarystructureid: empsalarystructureid,

            updateYearlyPaySheet: updateYearlyPaySheet,

            loadEmployeeChangeHistory: loadEmployeeChangeHistory,
            employeeChangeHistory: employeeChangeHistory,

            loadEmpSalaryStructureHistory: loadEmpSalaryStructureHistory,
            empSalaryStructureHistory: empSalaryStructureHistory,

            getEmpSalaryStructureComparisonList: getEmpSalaryStructureComparisonList,

            // Investment Declaration.
            loadInvestmentByEmployeeId: loadInvestmentByEmployeeId,
            investmentDeclarationResult: investmentDeclarationResult,

            loadInvestmentCatogories: loadInvestmentCatogories,
            empInvestmentDeclarationModel: empInvestmentDeclarationModel,
            addEmployeeInvestments: addEmployeeInvestments,

            loadEmployee: loadEmployee,
            employee: employee
        };

        return svc;

        function addEmployeeSalaryDetail(empSalaryStructureModel, employeeId) {
            return serviceHandler.executePostService('/Employee/SaveEmployeeSalaryDetail?employeeId=' + employeeId, empSalaryStructureModel);
        }

        function addEmployeeInvestments(employeeId,selectedInvestmentFinancialYear,empInvestmentDeclarationModel) {
            return serviceHandler.executePostService('/Employee/SaveEmployeeInvestments?employeeId=' + employeeId+'&finYear='+selectedInvestmentFinancialYear, empInvestmentDeclarationModel.InvestmentCategories);
        }

        function loadEmployeeAppraisalHistory(employeeId) {
            serviceHandler.executePostService('/Employee/GetEmployeeAppraisalHistory?employeeId=' + employeeId).then(function (resp) {
                var graphValues = [];
                if (resp.businessException == null) {

                    $.map(resp.result, function (val, i) {
                        val.CurrentEffectiveFrom = moment(val.CurrentEffectiveFrom).format("DD/MM/YYYY");
                        graphValues.push({ label: val.CurrentEffectiveFrom, value: val.CurrentCTC });
                    });

                    if (resp.result.length > 0) {
                        angular.extend(employeeAppraisalHistory, resp.result);
                        angular.extend(empAppraisalGraphValues, graphValues);
                    }
                }
                else {
                    $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
                }
            });
        }

        function loadEmployeeAppraisalHistoryForGraph(employeeId) {
            return serviceHandler.executePostService('/Employee/GetEmployeeAppraisalHistory?employeeId=' + employeeId);
        }

        function getEmployee(employeeId) {
            return serviceHandler.executePostService('/Employee/GetEmployeeDetail?employeeId=' + employeeId);
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
                    if (employeePaySheet.length > 0) {
                        empsalarystructureid = employeePaySheet[0].EmpSalaryStructureId;
                }
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
                            val.UpdatedDate = moment(val.UpdatedDate).format("DD/MM/YYYY, h:mm:ss A");
                        });

                        employeeChangeHistory.addRange(resp.result);
                    }
                }
                else {
                    $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
                }
            });
        }

        function loadEmpSalaryStructureHistory() {
            if (empsalarystructureid) {
                serviceHandler.executePostService("/employee/salary/" + empsalarystructureid + "/changeHistory").then(function (resp) {
                    if (resp.businessException == null) {
                        if (resp.result.length > 0) {
                            empSalaryStructureHistory.removeAll();// clear all the existing items.
                            $.map(resp.result, function (val, i) {
                                val.UpdatedDate = moment(val.UpdatedDate).format("DD/MM/YYYY, h:mm:ss A");
                            });

                            empSalaryStructureHistory.addRange(resp.result);
                        }
                    }
                    else {
                        $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
                    }
                });
            }
        }

        function getEmpSalaryStructureComparisonList(salaryComparisonParameter) {
            return serviceHandler.executePostService('/Employee/GetEmpSalaryStructureComparisonList', salaryComparisonParameter);
        }

        // Investment Declaration.
        function loadInvestmentByEmployeeId(employeeId) {
            serviceHandler.executePostService('/Employee/GetInvestmentByEmployeeId?employeeId=' + employeeId).then(function (resp) {
                if (resp.businessException == null) {
                    if (resp.result) {
                        angular.extend(investmentDeclarationResult, resp.result);
                    }
                }
                else {
                    $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
                }
            });
        }

        function loadInvestmentCatogories(financialYear, employeeId) {
            if (financialYear) {
                serviceHandler.executePostService('/Employee/GetInvestmentCatogories?financialYear=' + financialYear + '&employeeId=' + employeeId).then(function (resp) {
                    if (resp.businessException == null) {
                        if (resp.result) {
                            angular.extend(empInvestmentDeclarationModel, resp.result);
                        }
                    }
                    else {
                        $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
                    }
                });
            }
        }

        function loadEmployee(employeeId) {
            serviceHandler.executePostService('/Employee/GetEmployee', employeeId).then(function (resp) {
                if (resp.businessException == null) {
                    if (resp.result) {
                        angular.extend(employee, resp.result);
                    }
                }
                else {
                    $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
                }
            });
        }
    }
})();