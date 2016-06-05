(function () {
    'use strict';
    window.app.controller('EmpSalarySummaryController', EmpSalarySummaryController);
    EmpSalarySummaryController.$inject = ['$scope', 'EmpSalarySummaryService'];

    function EmpSalarySummaryController($scope, EmpSalarySummaryService) {

        $scope.salarySummaryModel = null;

        $scope.saveEmployeeSalaryDetail = function () {
            EmpSalarySummaryService.saveEmployeeSalaryDetail(salarySummaryModel).then(function (resp) {
                if (resp.businessException == null) {

                }
                else {
                    toastr.error("Error!", resp.businessException.ExceptionMessage);
                }
            });
        }
    }
})();