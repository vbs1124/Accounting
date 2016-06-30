(function () {
    "use strict";
    window.app.directive('addEmployeeInvestments', addEmployeeInvestments);
    function addEmployeeInvestments() {
        return {
            scope: {
                vmodel: "=",                
            },
            templateUrl: '/employee/template/addEmployeeInvestments.tmpl.cshtml',
            controller: addEmployeeInvestmentsController,
            controllerAs: 'vm'
        }
    }

    addEmployeeInvestmentsController.$inject = ['$scope', 'employeeService'];

    function addEmployeeInvestmentsController($scope, employeeService) {
        var vm = this;
        vm.employeeId = $scope.vmodel.employeeId;
        vm.selectedInvestmentFinancialYear = $scope.vmodel.selectedInvestmentFinancialYear;
        vm.form = 'frm-add-emp-investments',
        vm.joiningDate = $scope.vmodel.joiningDate,
        vm.relievingDate = $scope.vmodel.relievingDate,
        vm.financialYears = employeeService.getEmpFinancialYears(vm.joiningDate, vm.relievingDate);
        vm.loadInvestmentCatogories = employeeService.loadInvestmentCatogories(vm.selectedInvestmentFinancialYear, vm.employeeId);
        vm.empInvestmentDeclarationModel = employeeService.empInvestmentDeclarationModel;
        vm.saving = false;
        vm.addEmployeeInvestments = addEmployeeInvestments;
        function addEmployeeInvestments() {
            vm.saving = true;
            employeeService.addEmployeeInvestments(vm.employeeId,vm.selectedInvestmentFinancialYear, vm.empInvestmentDeclarationModel).then(function (resp) {
                if (resp.businessException == null) {
                    $.showToastrMessage("success", "Investment Declaration saved successfully.");
                }
                else {
                    $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
                }
                vm.saving = false;
            });
        }

        $scope.getHRALabel = function (subCategory) {
            if (subCategory.Name == 'January' || subCategory.Name == 'February' || subCategory.Name == 'March') {
                return subCategory.Name + " " + ($.vbsParseFloat(vm.selectedInvestmentFinancialYear) + 1).toString();
            } else {
                return subCategory.Name + " " + vm.selectedInvestmentFinancialYear;
            }
        }
        $scope.vbsParseFloat = function (value) {
            return $.vbsParseFloat(value);
        }

        $scope.onChangeInvestmentFinancialYear = function () {
            //alert(">>>>");
            employeeService.loadInvestmentCatogories(vm.selectedInvestmentFinancialYear, vm.employeeId);
        }
    }
})();