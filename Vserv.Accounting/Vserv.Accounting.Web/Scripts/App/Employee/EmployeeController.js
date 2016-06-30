(function () {
    'use strict';
    window.app.controller('EmployeeController', employeeController);
    employeeController.$inject = ['$scope', '$uibModal', '$filter', 'employeeService'];

    function employeeController($scope, $modal, $filter, employeeService) {
        var vm = this;

        vm.employeeId = $("#EmployeeId").val();

        vm.loadEmployee = employeeService.getEmployee(vm.employeeId);
        vm.employee = employeeService.employee;

        vm.empSalaryStructureModel = employeeService.empSalaryStructureModel;
        vm.relievingDate = new Date($("#RelievingDate").val());
        vm.joiningDate = new Date($("#JoiningDate").val());
        vm.financialYears = employeeService.getEmpFinancialYears(vm.joiningDate, vm.relievingDate);
        vm.selectedFinancialYear = vm.relievingDate == null || vm.relievingDate == 'Invalid Date' ? moment().year().toString() : moment(vm.relievingDate).year().toString();
        vm.selectedInvestmentFinancialYear = vm.relievingDate == null || vm.relievingDate == 'Invalid Date' ? moment().year().toString() : moment(vm.relievingDate).year().toString();
        vm.currentYear = moment().year().toString();

        vm.loadEmployeeAppraisalHistory = employeeService.loadEmployeeAppraisalHistory(vm.employeeId);
        vm.employeeAppraisalHistory = employeeService.employeeAppraisalHistory;
        vm.empAppraisalGraphValues = employeeService.empAppraisalGraphValues;

        vm.loadEmployeePaySheet = employeeService.loadEmployeePaySheet(vm.employeeId, parseInt(vm.selectedFinancialYear), parseInt(vm.selectedFinancialYear) + 1);
        vm.employeePaySheet = employeeService.employeePaySheet;
        vm.empsalarystructureid = employeeService.empsalarystructureid;

        vm.viewSelectedSalaryBreakup = viewSelectedSalaryBreakup;

        // Investment Declaration.
        vm.loadInvestmentByEmployeeId = employeeService.loadInvestmentByEmployeeId(vm.employeeId);
        vm.investmentDeclarationResult = employeeService.investmentDeclarationResult;

        vm.appraisalHistoryGraphOptions = {
            chart: {
                type: 'discreteBarChart',
                height: 295,
                width: 600,
                margin: {
                    top: 5,
                    right: 5,
                    bottom: 40,
                    left: 60
                },
                x: function (d) { return d.label; },
                y: function (d) { return d.value; },
                showValues: true,
                valueFormat: function (d) {
                    return d3.format(',')(d);
                },
                transitionDuration: 500,
                xAxis: {
                    // axisLabel: 'Financial Year'
                },
                yAxis: {
                    // axisLabel: '% Growth',
                    //axisLabelDistance: 2
                }
            }
        };

        vm.appraisalHistoryGraphData = [{
            key: "Appraisal History",
            values: vm.empAppraisalGraphValues
        }];

        vm.isEditableSalaryBreakup = function () {
            return vm.employeePaySheet.length > 0 && $.vbsParseFloat(vm.selectedFinancialYear) >= $.vbsParseFloat(vm.currentYear) && (vm.relievingDate == 'Invalid Date' || vm.relievingDate == null);
        }

        $scope.addNewSalaryStructure = function () {
            $modal.open({
                template: '<add-appraisal />',
                animation: true
            });
        }

        $scope.employeeChangeHistoryModal = function () {
            $modal.open({
                template: '<emp-history />',
                animation: true,
            });
        }

        $scope.empSalaryStructureChangeHistoryModal = function (empsalarystructureid) {
            $modal.open({
                template: '<emp-salary-structure-history empsalarystructureid="empsalarystructureid" />',
                scope: angular.extend($scope.$new(true), { empsalarystructureid: empsalarystructureid }),
                animation: true
            });
        }

        function viewSelectedSalaryBreakup() {
            $.showToastrMessage('info', "Functionality not implemented yet..!", "Information!");
        }

        $scope.onChangeFinancialYear = function () {
            employeeService.loadEmployeePaySheet(vm.employeeId, parseInt(vm.selectedFinancialYear), parseInt(vm.selectedFinancialYear) + 1);
        }
        $scope.onChangeInvestmentFinancialYear = function () {
            //alert(">>>>");
            employeeService.loadInvestmentCatogories(vm.selectedInvestmentFinancialYear, vm.employeeId);
        }

        //Method Initialize
        $scope.initialize = function (employeeId) {
        };
        $scope.vbsParseFloat = function (value) {
            return $.vbsParseFloat(value);
        }

        

        $scope.getCurrentComponentTotal = function (item) {

            if (item.SCCode === "SCCTCM") {
                return null;
            }
            var result = 0;

            if (item.April != null && !isNaN(item.April.Amount))
                result = result + $.vbsParseFloat(item.April.Amount);

            if (item.May != null && !isNaN(item.May.Amount))
                result = result + $.vbsParseFloat(item.May.Amount);

            if (item.June != null && !isNaN(item.June.Amount))
                result = result + $.vbsParseFloat(item.June.Amount);

            if (item.July != null && !isNaN(item.July.Amount))
                result = result + $.vbsParseFloat(item.July.Amount);

            if (item.August != null && !isNaN(item.August.Amount))
                result = result + $.vbsParseFloat(item.August.Amount);

            if (item.September != null && !isNaN(item.September.Amount))
                result = result + $.vbsParseFloat(item.September.Amount);

            if (item.October != null && !isNaN(item.October.Amount))
                result = result + $.vbsParseFloat(item.October.Amount);

            if (item.November != null && !isNaN(item.November.Amount))
                result = result + $.vbsParseFloat(item.November.Amount);

            if (item.December != null && !isNaN(item.December.Amount))
                result = result + $.vbsParseFloat(item.December.Amount);

            if (item.January != null && !isNaN(item.January.Amount))
                result = result + $.vbsParseFloat(item.January.Amount);

            if (item.February != null && !isNaN(item.February.Amount))
                result = result + $.vbsParseFloat(item.February.Amount);

            if (item.March != null && !isNaN(item.March.Amount))
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
            bootbox.confirm("Are you sure that you want to update existing Salary Structure?", function (result) {
                if (result) {
                    employeeService.updateYearlyPaySheet(vm.employeePaySheet).then(function (resp) {
                        if (resp.businessException == null) {
                            $.showToastrMessage("success", "Salary Breakup for current financial year updated successfully.");
                        }
                        else {
                            $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
                        }
                    });
                }
            });
        }

        $scope.foodCoupons = [{ value: 0, text: '0' }, { value: 1100, text: '1100' }, { value: 2200, text: '2200' }];
        $scope.showfoodCoupon = function (amount) {
            var selected = $filter('filter')($scope.foodCoupons, { value: amount });
            return (amount && selected.length) ? selected[0].text : '0';
        };

        $scope.vbsParseFloat = function (value) {
            return $.vbsParseFloat(value);
        }

        //---------------- Salary Breakup Ends here -----------
    }
})();