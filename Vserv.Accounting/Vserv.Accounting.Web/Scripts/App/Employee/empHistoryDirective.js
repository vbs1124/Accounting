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

    empHistorycontroller.$inject = ['$scope', '$uibModal', 'employeeService'];

    function empHistorycontroller($scope, $modal, employeeService) {
        var vm = this;
        vm.employeeId = $("#EmployeeId").val();
        vm.loadEmployeeChangeHistory = employeeService.loadEmployeeChangeHistory(vm.employeeId);
        vm.employeeChangeHistory = employeeService.employeeChangeHistory;
        vm.compareWithCurrent = function (employeeArchiveId) {
            window.location.href = VservApp.rootPath + "employee/" + employeeArchiveId + "/compare";
        }

        $scope.openEmpChangeHistoryModal = function (employeeArchiveId, updby, updon) {
            $modal.open({
                template: '<emp-change-comparison  emparchiveid="emparchiveid" updby="updby" updon="updon" />',
                animation: true,
                size: 'lg',
                scope: angular.extend($scope.$new(true), { emparchiveid: employeeArchiveId, updby: updby, updon: updon })
            });
        }
    }

})();