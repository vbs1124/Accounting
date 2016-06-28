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

    empSalaryStructureHistorycontroller.$inject = ['$scope', 'employeeService'];

    function empSalaryStructureHistorycontroller($scope, employeeService) {
        var vm = this;
        vm.loadEmpSalaryStructureHistory = employeeService.loadEmpSalaryStructureHistory();
        vm.empSalaryStructureHistory = employeeService.empSalaryStructureHistory;
        vm.compareWithCurrent = function (empsalarystructureid) {
            window.location.href = VservApp.rootPath + "salaryStructure/" + empsalarystructureid + "/compare";
        }
    }

})();