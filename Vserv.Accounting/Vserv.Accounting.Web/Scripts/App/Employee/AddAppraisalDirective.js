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

    controller.$inject = ['$scope', 'EmployeeService'];
    function controller($scope, EmployeeService) {
        var vm = this;
        vm.add = add;

        vm.saving = false;
        vm.salarySummaryModel = {};
        vm.errorMessage = null;

        function add() {
            vm.saving = true;
            EmployeeService.add(vm.salarySummaryModel).then(function (resp) {
                if (resp.businessException == null) {
                    //Close the modal
                    $scope.$close();
                }
                else {
                    vm.errorMessage = 'There was a problem adding the appraisal: ' + data;
                }
                vm.saving = false;
            });
        }
    }
})();