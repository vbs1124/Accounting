(function () {
    'use strict';
    window.app.controller('EmployeeController', employeeController);
    employeeController.$inject = ['$scope', '$uibModal', 'employeeService'];

    function employeeController($scope, $modal, employeeService) {
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
            $.showToastrMessage('info', "Functionality not implemented yet..!", "Information!")
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

            if (!isNaN(item.April))
                result = result + $.vbsParseFloat(item.April);
            if (!isNaN(item.May))
                result = result + $.vbsParseFloat(item.May);
            if (!isNaN(item.June))
                result = result + $.vbsParseFloat(item.June);
            if (!isNaN(item.July))
                result = result + $.vbsParseFloat(item.July);
            if (!isNaN(item.August))
                result = result + $.vbsParseFloat(item.August);
            if (!isNaN(item.September))
                result = result + $.vbsParseFloat(item.September);
            if (!isNaN(item.October))
                result = result + $.vbsParseFloat(item.October);
            if (!isNaN(item.November))
                result = result + $.vbsParseFloat(item.November);
            if (!isNaN(item.December))
                result = result + $.vbsParseFloat(item.December);
            if (!isNaN(item.January))
                result = result + $.vbsParseFloat(item.January);
            if (!isNaN(item.February))
                result = result + $.vbsParseFloat(item.February);
            if (!isNaN(item.March))
                result = result + $.vbsParseFloat(item.March);

            return $.vbsParseFloat(result);
        };

        $scope.getCurrentMonthTotal = function (data, month) {
            console.log("getCurrentMonthTotal fired....");
            if (typeof (data) === "undefined" || typeof (month) === "undefined") {
                return 0;
            }

            var componentForFooterTotal = [
                                            "SCBASC",
                                            "SCSHRA",
                                            "SCCONV",
                                            "SCSPCL",
                                            "SCPERF",
                                            "SCLECM",
                                            "SCSALA",
                                            "SCCABD",
                                            "SCODN",
                                            "SCCOMN",
                                            "SCOTHR",
                                            "SCMEDC",
                                            "SCFCPN"];

            var sum = 0;
            for (var i = data.length - 1; i >= 0; i--) {
                var currentcomp = data[i]["SCCode"];
                if ($.inArray(currentcomp, componentForFooterTotal) !== -1) {
                    sum += $.vbsParseFloat(data[i][month]);
                }
            }

            return sum.toFixed(0);
        }

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
            //employeeService.updateYearlyPaySheet($scope.paysheets).then(function (resp) {
            //    if (resp.businessException == null) {
            //    }
            //    else {
            //        $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
            //    }
            //});
        }

        //---------------- Salary Breakup Ends here -----------
    }
})();