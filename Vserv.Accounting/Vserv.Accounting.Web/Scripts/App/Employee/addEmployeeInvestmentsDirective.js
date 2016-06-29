(function () {
    "use strict";
    window.app.directive('addEmployeeInvestments', addEmployeeInvestments);
    function addEmployeeInvestments() {
        return {
            scope: {
                vmodel: "="
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
        vm.loadInvestmentCatogories = employeeService.loadInvestmentCatogories(vm.selectedInvestmentFinancialYear, vm.employeeId);
        vm.empInvestmentDeclarationModel = employeeService.empInvestmentDeclarationModel;
        vm.saving = false;
        vm.addEmployeeInvestments = addEmployeeInvestments;
        function addEmployeeInvestments() {
            vm.saving = true;
            employeeService.addEmployeeInvestments(vm.employeeId, vm.empInvestmentDeclarationModel).then(function (resp) {
                if (resp.businessException == null) {
                    $.showToastrMessage("success", "Investment Declaration saved successfully.");
                }
                else {
                    $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
                }
                vm.saving = false;
            });
        }
    }
})();