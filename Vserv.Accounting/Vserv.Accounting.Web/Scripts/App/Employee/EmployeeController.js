(function () {
    'use strict';
    window.app.controller('EmployeeController', employeeController);
    employeeController.$inject = ['$scope', '$uibModal', '$filter', 'employeeService'];

    function employeeController($scope, $modal, $filter, employeeService) {
        var vm = this;

        vm.employeeId = $("#EmployeeId").val();
        vm.addNewSalaryStructure = addNewSalaryStructure;
        vm.empSalaryStructureModel = employeeService.empSalaryStructureModel;
        vm.financialYears = employeeService.getEmpFinancialYears(new Date($("#JoiningDate").val()));
        vm.selectedFinancialYear = moment().year().toString();

        vm.loadEmployeeAppraisalHistory = employeeService.loadEmployeeAppraisalHistory(vm.employeeId);
        vm.employeeAppraisalHistory = employeeService.employeeAppraisalHistory;

        vm.loadEmployeePaySheet = employeeService.loadEmployeePaySheet(vm.employeeId, parseInt(vm.selectedFinancialYear), parseInt(vm.selectedFinancialYear) + 1);
        vm.employeePaySheet = employeeService.employeePaySheet;

        vm.viewSelectedSalaryBreakup = viewSelectedSalaryBreakup;

        function addNewSalaryStructure() {
            $modal.open({
                template: '<add-appraisal />'
            });
        }

        function employeeChangeHistoryModal() {
            $modal.open({
                template: '<emp-history />'
            });
        }

        function viewSelectedSalaryBreakup() {
            $.showToastrMessage('info', "Functionality not implemented yet..!", "Information!");
        }

        $scope.onChangeFinancialYear = function () {
            employeeService.loadEmployeePaySheet(vm.employeeId, parseInt(vm.selectedFinancialYear), parseInt(vm.selectedFinancialYear) + 1);
        }

        //Method Initialize
        $scope.initialize = function (employeeId) {
        };

        $scope.getCurrentComponentTotal = function (item) {

            if (item.SCCode === "SCCTCM") {
                return null;
            }
            var result = 0;

            if (!isNaN(item.April.Amount))
                result = result + $.vbsParseFloat(item.April.Amount);
            if (!isNaN(item.May.Amount))
                result = result + $.vbsParseFloat(item.May.Amount);
            if (!isNaN(item.June.Amount))
                result = result + $.vbsParseFloat(item.June.Amount);
            if (!isNaN(item.July.Amount))
                result = result + $.vbsParseFloat(item.July.Amount);
            if (!isNaN(item.August.Amount))
                result = result + $.vbsParseFloat(item.August.Amount);
            if (!isNaN(item.September.Amount))
                result = result + $.vbsParseFloat(item.September.Amount);
            if (!isNaN(item.October.Amount))
                result = result + $.vbsParseFloat(item.October.Amount);
            if (!isNaN(item.November.Amount))
                result = result + $.vbsParseFloat(item.November.Amount);
            if (!isNaN(item.December.Amount))
                result = result + $.vbsParseFloat(item.December.Amount);
            if (!isNaN(item.January.Amount))
                result = result + $.vbsParseFloat(item.January.Amount);
            if (!isNaN(item.February.Amount))
                result = result + $.vbsParseFloat(item.February.Amount);
            if (!isNaN(item.March.Amount))
                result = result + $.vbsParseFloat(item.March.Amount);

            return $.vbsParseFloat(result);
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

        $scope.isEditableColumn = function (componentCode) {
            return $.inArray(componentCode, $scope.nonEditableComponents) === -1;
        }

        $scope.updateYearlyPaySheet = function () {
            employeeService.updateYearlyPaySheet(vm.employeePaySheet).then(function (resp) {
                if (resp.businessException == null) {
                    $.showToastrMessage("success", "Salary Breakup for current financial year updated successfully.");
                }
                else {
                    $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
                }
            });
        }

        $scope.foodCoupons = [{ value: 0, text: '0' }, { value: 1100, text: '1100' }, { value: 2200, text: '2200' }];
        $scope.showfoodCoupon = function (amount) {
            var selected = $filter('filter')($scope.foodCoupons, { value: amount });
            return (amount && selected.length) ? selected[0].text : '0';
        };
        //---------------- Salary Breakup Ends here -----------
    }
})();