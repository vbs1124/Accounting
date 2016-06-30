(function () {
    "use strict";
    window.app.directive('addAppraisal', addAppraisal);

    function addAppraisal() {
        return {
            templateUrl: '/employee/template/addAppraisal.tmpl.cshtml',
            controller: controller,
            controllerAs: 'vm'
        }
    }

    controller.$inject = ['$scope', 'employeeService'];

    function controller($scope, employeeService) {
        var vm = this;
        vm.employeeId = parseInt($("#EmployeeId").val());
        vm.addEmployeeSalaryDetail = addEmployeeSalaryDetail;
        vm.saving = false;
        vm.errorMessage = null;
        vm.isEditableEffectiveFrom = true;
        vm.empSalaryStructureModel = {
            effectiveFrom: setEffectiveFrom()
        };
        vm.paySheetParameter = {
            EmployeeId: $("#EmployeeId").val(),
            FinancialYearFrom: moment().year(),
            FinancialYearTo: moment().year() + 1
        };

        function setEffectiveFrom() {
            if (employeeService.employeeAppraisalHistory.length > 0) {
                return new Date();
            } else {
                vm.isEditableEffectiveFrom = false;
                return new Date($("#JoiningDate").val()) // Set the default "Effective From" to joining date of the employee.
            }
        }

        function addEmployeeSalaryDetail() {
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

            bootbox.confirm("Are you sure that you want to add a new Appraisal?", function (result) {
                if (result) {
                    vm.saving = true;
                    employeeService.addEmployeeSalaryDetail(vm.empSalaryStructureModel, vm.employeeId).then(function (resp) {
                        if (resp.businessException == null) {

                            if (resp.result.IsErrorOccurred) {
                                $.showToastrMessage("error", resp.result.Message, "Error!");
                                vm.saving = false;
                            }
                            else {
                                $.showToastrMessage("success", resp.result.Message, "Success!");
                                employeeService.loadEmployeeAppraisalHistory(vm.employeeId);

                                // Activate "Salary Breakup" tab.
                                if (moment(vm.empSalaryStructureModel.effectiveFrom).year() == moment().year()) {
                                    employeeService.loadEmployeePaySheet(vm.employeeId, parseInt(moment().year()), parseInt(moment().year()) + 1);
                                    activateTab("tabs-manage-employee", "emp-salary-breakup");
                                }

                                //Close the modal
                                $scope.$close();
                            }
                        }
                        else {
                            vm.errorMessage = 'There was a problem adding the appraisal: ' + data;
                        }
                        vm.saving = false;
                    });
                }
                vm.saving = true;
            });
        }

        //-------------------------
        $scope.dateOptionsEffectiveFrom = {
            formatYear: 'yyyy',
            startingDay: 1,
            minDate: new Date(2009, 1, 1),
            maxDate: new Date(moment().year(), 12, 31)
        };

        $scope.openEffectiveFrom = function () {
            if (vm.isEditableEffectiveFrom) {
                $scope.popupEffectiveFrom.opened = true;
            } else {
                $.showToastrMessage("error", "Not allowed..", "Error!");
            }
        };

        $scope.popupEffectiveFrom = {
            opened: false
        };

        //---------------------------
    }

    function activateTab(tabId, tab) {
        $('#' + tabId + ' a[href="#' + tab + '"]').tab('show');
    };
})();