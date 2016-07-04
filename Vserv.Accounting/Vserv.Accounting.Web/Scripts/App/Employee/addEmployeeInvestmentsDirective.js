(function () {
    "use strict";
    window.app.directive('addEmployeeInvestments', addEmployeeInvestments);
    function addEmployeeInvestments() {
        return {
            scope: {
                vmodel: "=",
                isapprovermode: "="
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
            bootbox.confirm("Are you sure that you want to save Investment Declaration?", function (result) {
                if (result) {
                    vm.saving = true;
                    employeeService.addEmployeeInvestments(vm.employeeId, vm.selectedInvestmentFinancialYear, vm.empInvestmentDeclarationModel).then(function (resp) {
                        if (resp.businessException == null) {
                            employeeService.loadInvestmentCatogories(vm.selectedInvestmentFinancialYear, vm.employeeId);
                            $.showToastrMessage("success", "Investment Declaration saved successfully.");
                        }
                        else {
                            $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
                        }
                        vm.saving = false;
                    });
                } else {
                    vm.saving = false;
                }
            });
        }

        $scope.getHRALabel = function (subCategory) {
            if (subCategory.name === 'January' || subCategory.name === 'February' || subCategory.name === 'March') {
                return subCategory.name + " " + ($.vbsParseFloat(vm.selectedInvestmentFinancialYear) + 1).toString();
            } else {
                return subCategory.name + " " + vm.selectedInvestmentFinancialYear;
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