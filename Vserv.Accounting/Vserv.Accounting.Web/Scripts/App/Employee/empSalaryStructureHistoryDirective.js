(function () {
    "use strict";
    window.app.directive('empSalaryStructureHistory', empSalaryStructureHistory);
    function empSalaryStructureHistory() {
        return {
            scope: {
                empsalarystructureid: "="
            },
            templateUrl: '/employee/template/empSalaryStructureHistory.tmpl.cshtml',
            controller: empSalaryStructureHistorycontroller,
            controllerAs: 'vm'
        }
    }

    empSalaryStructureHistorycontroller.$inject = ['$scope', '$uibModal', 'employeeService'];

    function empSalaryStructureHistorycontroller($scope, $modal, employeeService) {
        var vm = this;
        vm.loadEmpSalaryStructureHistory = employeeService.loadEmpSalaryStructureHistory();
        vm.empSalaryStructureHistory = employeeService.empSalaryStructureHistory;
        vm.compareWithCurrent = function (empsalarystructureid) {
            window.location.href = VservApp.rootPath + "salaryStructure/" + empsalarystructureid + "/compare";
        }

        $scope.getEmpSalaryStructureComparisonList = function () {
            employeeService.getEmpSalaryStructureComparisonList();
        }

        $scope.openEmpSalaryChangeHistoryModal = function (item) {
            $modal.open({
                template: '<emp-salary-comparison empid="empid" fyf="fyf" ucid ="ucid" />',
                animation: true,
                size: 'lg',
                scope: angular.extend($scope.$new(true), { empid: item.EmployeeId, fyf: $("#cmb-financial-year").val(), ucid: item.UniqueChangeId })
            });
        }
    }

})();