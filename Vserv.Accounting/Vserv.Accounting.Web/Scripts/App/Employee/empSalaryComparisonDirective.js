(function () {
    "use strict";
    window.app.directive('empSalaryComparison', empSalaryComparison);
    function empSalaryComparison() {
        return {
            scope: {
                empid: "=",
                fyf: "=",
                ucid: "="
            },
            templateUrl: '/employee/template/empSalaryComparisonList.tmpl.cshtml',
            controller: empSalaryComparisonController,
            controllerAs: 'vm'
        }
    }

    empSalaryComparisonController.$inject = ['$scope', 'employeeService'];

    function empSalaryComparisonController($scope, employeeService) {
        var vm = this;
        $scope.empSalaryCompareResult = [];
        $scope.getEmpSalaryStructureComparisonList = function () {
            var salaryComparisonParameter = { EmployeeId: $scope.empid, FinancialYearFrom: $scope.fyf, UniqueChangeId: $scope.ucid };
            employeeService.getEmpSalaryStructureComparisonList(salaryComparisonParameter).then(function (resp) {
                if (resp.businessException == null) {
                    angular.extend($scope.empSalaryCompareResult, resp.result);
                }
                else {
                    $.showToastrMessage("error", resp.businessException.ExceptionMessage, "Error!");
                }
            });
        }

        $scope.getCurrentComponentTotal = function (item) {

            if (item.SCCode === "SCCTCM") {
                return null;
            }
            var result = 0;

            if (item.April != null && !isNaN(item.April.Amount))
                result = result + $.vbsParseFloat(item.April.Amount);

            if (item.May != null && !isNaN(item.May.Amount))
                result = result + $.vbsParseFloat(item.May.Amount);

            if (item.June != null && !isNaN(item.June.Amount))
                result = result + $.vbsParseFloat(item.June.Amount);

            if (item.July != null && !isNaN(item.July.Amount))
                result = result + $.vbsParseFloat(item.July.Amount);

            if (item.August != null && !isNaN(item.August.Amount))
                result = result + $.vbsParseFloat(item.August.Amount);

            if (item.September != null && !isNaN(item.September.Amount))
                result = result + $.vbsParseFloat(item.September.Amount);

            if (item.October != null && !isNaN(item.October.Amount))
                result = result + $.vbsParseFloat(item.October.Amount);

            if (item.November != null && !isNaN(item.November.Amount))
                result = result + $.vbsParseFloat(item.November.Amount);

            if (item.December != null && !isNaN(item.December.Amount))
                result = result + $.vbsParseFloat(item.December.Amount);

            if (item.January != null && !isNaN(item.January.Amount))
                result = result + $.vbsParseFloat(item.January.Amount);

            if (item.February != null && !isNaN(item.February.Amount))
                result = result + $.vbsParseFloat(item.February.Amount);

            if (item.March != null && !isNaN(item.March.Amount))
                result = result + $.vbsParseFloat(item.March.Amount);

            return $.vbsParseFloat(result);
        };
    }
})();