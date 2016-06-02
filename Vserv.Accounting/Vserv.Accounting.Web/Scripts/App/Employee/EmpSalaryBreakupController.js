(function () {
    'use strict';
    window.app.controller('EmpSalaryBreakupController', EmpSalaryBreakupController);
    EmpSalaryBreakupController.$inject = ['$scope', 'EmpSalaryBreakupService'];

    function EmpSalaryBreakupController($scope, EmpSalaryBreakupService) {
        $scope.paysheets = [];

        $scope.paySheetParameter = {
            EmployeeId: null,
            FinancialYearFrom: null,
            FinancialYearTo: null,
        };

        //Method Initialize
        $scope.initialize = function () {
            //  $scope.loadYearlyPaySheet();
        };

        // Method loadSiteFeatures
        $scope.loadYearlyPaySheet = function (employeeId) {
            $scope.paySheetParameter.EmployeeId = employeeId;
            $scope.paySheetParameter.FinancialYearFrom = 2016;
            $scope.paySheetParameter.FinancialYearTo = 2017;

            EmpSalaryBreakupService.getYearlyPaySheet($scope.paySheetParameter).then(function (resp) {
                if (resp.businessException == null) {
                    $scope.paysheets = resp.result;
                    // $scope.gridOptions = { data: "paysheets" };
                }
                else {
                    toastr.error("Error!", resp.businessException.ExceptionMessage);
                }
            });
        };

        $scope.gridOptions = {
            enableCellSelection: true,
            enableCellEditOnFocus: true,
            cellEditableCondition: 'row.entity.editable',
            columnDefs: [
                { displayName: "", name: '', field: 'ComponentName', width: 140, enableCellEdit: true },
                { displayName: "April", name: 'April', field: 'April', width: 80, enableCellEdit: true },
                { displayName: "May", name: 'May', field: 'May', width: 80 },
                { displayName: "June", name: 'June', field: 'June', width: 80 },
                { displayName: "July", name: 'July', field: 'July', width: 80 },
                { displayName: "August", name: 'August', field: 'August', width: 80 },
                { displayName: "September", name: 'September', field: 'September', width: 100 },
                { displayName: "October", name: 'October', field: 'October', width: 100 },
                { displayName: "November", name: 'November', field: 'November', width: 100 },
                { displayName: "December", name: 'December', field: 'December', width: 100 },
                { displayName: "January", name: 'January', field: 'January', width: 100 },
                { displayName: "February", name: 'February', field: 'February', width: 100 },
                { displayName: "March", name: 'March', field: 'March', width: 100 },
            ],
            data: 'paysheets'
        };

        $scope.user = {
            name: 'awesome user'
        };
    }
})();