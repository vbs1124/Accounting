(function () {
    "use strict";
    window.app.directive('empSalaryStructureHistory', empSalaryStructureHistory);
    function empSalaryStructureHistory() {
        return {
            scope: {
                empSalaryStructureId: "="
            },
            templateUrl: '/employee/template/empSalaryStructureHistory.tmpl.cshtml',
            controller: empSalaryStructureHistorycontroller,
            controllerAs: 'vm'
        }
    }

    empSalaryStructureHistorycontroller.$inject = ['$scope', 'employeeService'];

    function empSalaryStructureHistorycontroller($scope, employeeService) {
        var vm = this;
        vm.loadEmpSalaryStructureHistory = employeeService.loadEmpSalaryStructureHistory($scope.empSalaryStructureId);
        vm.empSalaryStructureHistory = employeeService.empSalaryStructureHistory;
        vm.compareWithCurrent = function (empSalaryStructureId) {
            window.location.href = VservApp.rootPath + "salaryStructure/" + empSalaryStructureId + "/compare";
        }
    }

})();