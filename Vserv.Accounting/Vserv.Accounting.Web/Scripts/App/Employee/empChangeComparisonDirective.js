(function () {
    "use strict";
    window.app.directive('empChangeComparison', empChangeComparison);
    function empChangeComparison() {
        return {
            scope: {
                emparchiveid: "=",
                updby: "=",
                updon: "="
            },
            templateUrl: '/employee/template/empChangeComparisonList.tmpl.cshtml',
            controller: empChangeComparisonController,
            controllerAs: 'vm'
        }
    }

    empChangeComparisonController.$inject = ['$scope', 'employeeService'];

    function empChangeComparisonController($scope, employeeService) {
        var vm = this;
        $scope.empChangeComparisonResult = [];
        $scope.getEmpChangeHistoryResult = function () {
            employeeService.loadEmpChangeComparisonResult({ employeeArchiveId: $scope.emparchiveid }).then(function (resp) {
                if (resp.businessException == null) {
                    if (resp.result) {
                        if (resp.result.PreviousEmployeeInfo.BirthDay)
                            resp.result.PreviousEmployeeInfo.BirthDay = moment(resp.result.PreviousEmployeeInfo.BirthDay).format("DD/MM/YYYY");
                        if (resp.result.PreviousEmployeeInfo.JoiningDate)
                            resp.result.PreviousEmployeeInfo.JoiningDate = moment(resp.result.PreviousEmployeeInfo.JoiningDate).format("DD/MM/YYYY");
                        if (resp.result.PreviousEmployeeInfo.ResignationDate)
                            resp.result.PreviousEmployeeInfo.ResignationDate = moment(resp.result.PreviousEmployeeInfo.ResignationDate).format("DD/MM/YYYY");
                        if (resp.result.PreviousEmployeeInfo.RelievingDate)
                            resp.result.PreviousEmployeeInfo.RelievingDate = moment(resp.result.PreviousEmployeeInfo.RelievingDate).format("DD/MM/YYYY");

                        if (resp.result.CurrentEmployeeInfo.BirthDay)
                            resp.result.CurrentEmployeeInfo.BirthDay = moment(resp.result.CurrentEmployeeInfo.BirthDay).format("DD/MM/YYYY");
                        if (resp.result.CurrentEmployeeInfo.JoiningDate)
                            resp.result.CurrentEmployeeInfo.JoiningDate = moment(resp.result.CurrentEmployeeInfo.JoiningDate).format("DD/MM/YYYY");
                        if (resp.result.CurrentEmployeeInfo.ResignationDate)
                            resp.result.CurrentEmployeeInfo.ResignationDate = moment(resp.result.CurrentEmployeeInfo.ResignationDate).format("DD/MM/YYYY");
                        if (resp.result.CurrentEmployeeInfo.RelievingDate)
                            resp.result.CurrentEmployeeInfo.RelievingDate = moment(resp.result.CurrentEmployeeInfo.RelievingDate).format("DD/MM/YYYY");
                    }
                    angular.extend($scope.empChangeComparisonResult, resp.result);
                }
                else {
                    $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
                }
            });
        }
    }
})();