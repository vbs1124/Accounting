(function () {
    "use strict";
    window.app.directive('addAppraisal', addAppraisal);
    controller.$inject = ['$scope', 'employeeService'];

    function controller($scope, employeeService) {
        var vm = this;

        function add() {
            var monthlyCTC = vm.empSalaryStructureModel.ctc / 12;

            if (vm.empSalaryStructureModel.cabDeductions > monthlyCTC) {
                $.showToastrMessage("error", "Cab Deductions should be less than CTC.", "Error!");
                return false;
            }

            if (vm.empSalaryStructureModel.projectIncentive > monthlyCTC) {
                $.showToastrMessage("error", "Project Incentive should be less than CTC.", "Error!");
                return false;
            }

            if (vm.empSalaryStructureModel.carLease > monthlyCTC) {
                $.showToastrMessage("error", "Car Lease should be less than CTC.", "Error!");
                return false;
            }

            if (vm.empSalaryStructureModel.foodCoupons > monthlyCTC) {
                $.showToastrMessage("error", "Food Coupons should be less than CTC.", "Error!");
                return false;
            }

            vm.saving = true;
            employeeService.addEmployeeSalaryDetail(vm.empSalaryStructureModel, $("#EmployeeId").val()).then(function (resp) {
                if (resp.businessException == null) {
                    $.showToastrMessage("success", 'Salary breakup generated successfully.', "Success!");
                    //Close the modal
                    $scope.$close();
                }
                else {
                    vm.errorMessage = 'There was a problem adding the appraisal: ' + data;
                }
                vm.saving = false;
            });
            return false;
        }

        vm.add = add;

        vm.saving = false;
        vm.empSalaryStructureModel = {};
        vm.errorMessage = null;

        //-------------------------
        $scope.dateOptionsEffectiveFrom = {
            formatYear: 'yyyy',
            startingDay: 1
        };

        $scope.openEffectiveFrom = function () {
            $scope.popupEffectiveFrom.opened = true;
        };

        $scope.popupEffectiveFrom = {
            opened: false
        };
        //---------------------------
    }

    function addAppraisal() {
        return {
            templateUrl: '/employee/template/addAppraisal.tmpl.cshtml',
            controller: controller,
            controllerAs: 'vm'
        }
    }
})();