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
        $scope.employeeId = $("#EmployeeId").val();

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

        //---------------- Salary Breakup Starts here -----------
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
            $scope.employeeId = employeeId;
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

            if (item.SCCode === "SCCTCM") {
                return null;
            }
            var result = 0;

            if (!isNaN(item.April))
                result = result + item.April;
            if (!isNaN(item.May))
                result = result + item.May;
            if (!isNaN(item.June))
                result = result + item.June;
            if (!isNaN(item.July))
                result = result + item.July;
            if (!isNaN(item.August))
                result = result + item.August;
            if (!isNaN(item.September))
                result = result + item.September;
            if (!isNaN(item.October))
                result = result + item.October;
            if (!isNaN(item.November))
                result = result + item.November;
            if (!isNaN(item.December))
                result = result + item.December;
            if (!isNaN(item.January))
                result = result + item.January;
            if (!isNaN(item.February))
                result = result + item.February;
            if (!isNaN(item.March))
                result = result + item.March;

            result = result.toFixed(0);
            return result;
        };

        $scope.nonEditableComponents = [
            "SCCTCM",
            "SCBASC",
            "SCSHRA",
            "SCCONV",
            "SCSPCL",
            "SCPERF",
            "SCMEDC",
            "SCEPFO",
            "SCMEDM",
            "SCGRAT"];

        $scope.isEditableColumn = function (componentName) {
            return $.inArray(componentName, $scope.nonEditableComponents) === -1;
        }

        $scope.updateYearlyPaySheet = function () {
            employeeService.updateYearlyPaySheet($scope.paysheets).then(function (resp) {
                if (resp.businessException == null) {
                }
                else {
                    $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
                }
            });
        }

        //---------------- Salary Breakup Ends here -----------
    }
})();