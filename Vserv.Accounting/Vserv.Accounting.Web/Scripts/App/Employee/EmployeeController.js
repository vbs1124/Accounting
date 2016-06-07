(function () {
    'use strict';
    window.app.controller('EmployeeController', EmployeeController);

    EmployeeController.$inject = ['$uibModal', 'EmployeeService'];
    function EmployeeController($modal, EmployeeService) {
        var vm = this;
        vm.add = add;
        vm.salarySummaryModel = EmployeeService.salarySummaryModel;

        function add() {
            $modal.open({
                template: '<add-appraisal />'
            });
        }
    }
})();