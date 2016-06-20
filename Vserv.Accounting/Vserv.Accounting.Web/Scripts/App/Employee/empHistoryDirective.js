(function () {
    "use strict";
    window.app.directive('empHistory', empHistory);
    function empHistory() {
        return {
            templateUrl: '/employee/template/empHistory.tmpl.cshtml',
            controller: empHistorycontroller,
            controllerAs: 'vm'
        }
    }

    empHistorycontroller.$inject = ['$scope', 'employeeService'];

    function empHistorycontroller($scope, employeeService) {
        var vm = this;
        vm.employeeId = $("#EmployeeId").val();
        vm.loadEmployeeChangeHistory = employeeService.loadEmployeeChangeHistory(vm.employeeId);
        vm.employeeChangeHistory = employeeService.employeeChangeHistory;
        vm.compareWithCurrent = function (employeeArchiveId) {
            window.location.href = VservApp.rootPath + "employee/" + employeeArchiveId + "/compare";
        }
    }

})();