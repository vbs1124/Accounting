(function () {
    'use strict';
    window.app.controller('EmployeeController', employeeController);
    employeeController.$inject = ['$scope', '$uibModal', 'employeeService'];

    function employeeController($scope, $modal, employeeService) {
        var vm = this;

        function add() {
            $modal.open({
                template: '<add-appraisal />'
            });
        }

        vm.add = add;
        vm.empSalaryStructureModel = employeeService.empSalaryStructureModel;
        $scope.employeeAppraisalHistory = [];

        $scope.loadEmployeeAppraisalHistory = function (employeeId) {
            employeeService.loadEmployeeAppraisalHistory(employeeId).then(function (resp) {
                if (resp.businessException == null) {

                    $.map(resp.result, function (val, i) {
                        val.CurrentEffectiveFrom = moment(val.CurrentEffectiveFrom).format("DD/MM/YYYY");
                    });
                    $scope.employeeAppraisalHistory = resp.result;
                }
                else {
                    $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
                }
            });
        }

        $scope.employeeAppraisalHistoryGridOptions = {
            data: "employeeAppraisalHistory",
            enableRowSelection: true,
            multiSelect: false,
            columnDefs: [
              { field: 'CurrentEffectiveFrom', displayName: 'Effective From', width: 150 },
              { field: 'CurrentCTC', displayName: 'Cost to Company(CTC)', width: 150 },
              { field: 'PercentageGrowth', displayName: '% Growth', width: 150 },
            ]
        };

        ///---------------- Salary Breakup 

        {
            $scope.paysheets = [];
            $scope.FinancialYears = employeeService.getFinancialYears();
            $scope.currentYear = moment().year().toString();
            $scope.selectedFinancialYear = $scope.currentYear;

            $scope.paySheetParameter = {
                EmployeeId: null,
                FinancialYearFrom: null,
                FinancialYearTo: null
            };

            $scope.onChangeFinancialYear = function () {
                $scope.loadYearlyPaySheet();
            }

            //Method Initialize
            $scope.initialize = function (employeeId) {
                $scope.loadEmployeeAppraisalHistory(employeeId);
                $scope.loadYearlyPaySheet(employeeId);
            };

            // Method loadSiteFeatures
            $scope.loadYearlyPaySheet = function (employeeId) {

                if (employeeId) {
                    $scope.paySheetParameter.EmployeeId = employeeId;
                } else {
                    $scope.paySheetParameter.EmployeeId = $("#EmployeeId").val();
                }

                $scope.paySheetParameter.FinancialYearFrom = $scope.selectedFinancialYear;
                $scope.paySheetParameter.FinancialYearTo = parseInt($scope.selectedFinancialYear) + 1;

                employeeService.loadYearlyPaySheet($scope.paySheetParameter).then(function (resp) {
                    if (resp.businessException == null) {
                        $scope.paysheets = resp.result;
                    }
                    else {
                        $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
                    }
                });
            };

            $scope.parseFloat = function (value) {
                return parseFloat(value);
            }

            $scope.getCurrentComponentTotal = function (item) {
                var result = parseFloat(item.April) + parseFloat(item.May)
                    + parseFloat(item.June) + parseFloat(item.July)
                    + parseFloat(item.August) + parseFloat(item.September)
                    + parseFloat(item.October) + parseFloat(item.November)
                    + parseFloat(item.December) + parseFloat(item.January)
                    + parseFloat(item.February) + parseFloat(item.March);

                result = result.toFixed(0);
                return result;
            };

            $scope.nonEditableComponents = ["CTC", "Basic", "HRA", "Conveyance"
                , "Special Allowance", "PerformanceIncentive"
                , "Medical", "PF", "Mediclaim", "Gratuity"];

            $scope.componentForFooterTotal = ["Basic", "HRA", "Conveyance"
                , "Special Allowance", "Performance Incentive", "Leave encashment"
                , "Salary Arrears", "Cab Deductions", "Other Deduction"
                , "Commission", "Others", "Medical", "Food Coupons"];

            $scope.isEditableColumn = function (componentName) {
                return $.inArray(componentName, $scope.nonEditableComponents) === -1;
            }
        }
        // End
    }
})();