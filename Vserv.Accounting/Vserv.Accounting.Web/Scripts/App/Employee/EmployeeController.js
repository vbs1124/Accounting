(function () {
    'use strict';

    function employeeController($modal, employeeService) {
        var vm = this;

        function add() {
            $modal.open({
                template: '<add-appraisal />'
            });
        }

        vm.add = add;
        vm.salarySummaryModel = employeeService.salarySummaryModel;
    }

    window.app.controller('EmployeeController', employeeController);

    employeeController.$inject = ['$uibModal', 'EmployeeService'];
})();