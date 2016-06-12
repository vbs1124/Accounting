(function () {
    'use strict';

    function empSalarySummaryController($scope, empSalarySummaryService) {

        $scope.salarySummaryModel = null;

        $scope.saveEmployeeSalaryDetail = function () {
            empSalarySummaryService.saveEmployeeSalaryDetail(salarySummaryModel).then(function (resp) {
                if (resp.businessException == null) {

                }
                else {
                    toastr.error("Error!", resp.businessException.ExceptionMessage);
                }
            });
        }
    }

    window.app.controller('EmpSalarySummaryController', empSalarySummaryController);
    empSalarySummaryController.$inject = ['$scope', 'EmpSalarySummaryService'];
})();