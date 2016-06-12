(function () {
    'use strict';

    function getFinancialYears() {
        var currentYear = moment().year();
        var financialYears = [];
        for (var i = currentYear + 1 ; i > currentYear - 9; i--) {
            financialYears.push({ currentYear: i - 1, financialYear: i - 1 + '-' + i });
        }
        console.log(financialYears);
        return financialYears;
    }

    function empSalaryBreakupController($scope, empSalaryBreakupService) {
        $scope.paysheets = [];
        $scope.FinancialYears = getFinancialYears();
        $scope.selectedFinancialYear = moment().year().toString();

        $scope.paySheetParameter = {
            EmployeeId: null,
            FinancialYearFrom: null,
            FinancialYearTo: null
        };

        //Method Initialize
        $scope.initialize = function () {
            //  $scope.loadYearlyPaySheet();
        };

        // Method loadSiteFeatures
        $scope.loadYearlyPaySheet = function (employeeId) {
            $scope.paySheetParameter.EmployeeId = employeeId;
            $scope.paySheetParameter.FinancialYearFrom = 2016;
            $scope.paySheetParameter.FinancialYearTo = 2017;

            empSalaryBreakupService.getYearlyPaySheet($scope.paySheetParameter).then(function (resp) {
                if (resp.businessException == null) {
                    $scope.paysheets = resp.result;
                    // $scope.gridOptions = { data: "paysheets" };
                }
                else {
                    toastr.error("Error!", resp.businessException.ExceptionMessage);
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

    window.app.controller('EmpSalaryBreakupController', empSalaryBreakupController);
    empSalaryBreakupController.$inject = ['$scope', 'EmpSalaryBreakupService'];
})();