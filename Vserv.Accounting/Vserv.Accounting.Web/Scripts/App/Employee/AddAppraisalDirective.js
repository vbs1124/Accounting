(function () {
    "use strict";

    function controller($scope, employeeService) {
        var vm = this;

        function add() {
            var monthlyCTC = vm.salarySummaryModel.ctc / 12;

            if (vm.salarySummaryModel.cabDeductions > monthlyCTC) {
                $.showToastrMessage("error", "Cab Deductions should be less than CTC.", "Error!");
                return false;
            }

            if (vm.salarySummaryModel.projectIncentive > monthlyCTC) {
                $.showToastrMessage("error", "Project Incentive should be less than CTC.", "Error!");
                return false;
            }

            if (vm.salarySummaryModel.carLease > monthlyCTC) {
                $.showToastrMessage("error", "Car Lease should be less than CTC.", "Error!");
                return false;
            }

            if (vm.salarySummaryModel.foodCoupons > monthlyCTC) {
                $.showToastrMessage("error", "Food Coupons should be less than CTC.", "Error!");
                return false;
            }

            vm.saving = true;
            employeeService.add(vm.salarySummaryModel, $("#EmployeeId").val()).then(function (resp) {
                if (resp.businessException == null) {
                    $.showToastrMessage("success", 'Salary breakup generated successfully.', "Success!");
                    //Close the modal
                    $scope.$close();
                }
                else {
                    vm.errorMessage = 'There was a problem adding the appraisal: ' + data;
                }
                vm.saving = false;
            });
            return false;
        }

        vm.add = add;

        vm.saving = false;
        vm.salarySummaryModel = {};
        vm.errorMessage = null; //-------------------------
        $scope.today = function () {
            $scope.dt = new Date();
        };
        $scope.today();

        $scope.clear = function () {
            $scope.dt = null;
        };

        function getDayClass(data) {
            var date = data.date,
                mode = data.mode;
            if (mode === 'day') {
                var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

                for (var i = 0; i < $scope.events.length; i++) {
                    var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                    if (dayToCheck === currentDay) {
                        return $scope.events[i].status;
                    }
                }
            }

            return "";
        }

        $scope.inlineOptions = {
            customClass: getDayClass,
            minDate: new Date(),
            showWeeks: true
        };

        function disabled(data) {
            var date = data.date,
                mode = data.mode;
            return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
        }

        $scope.dateOptions = {
            dateDisabled: disabled,
            formatYear: 'yy',
            maxDate: new Date(2020, 5, 22),
            minDate: new Date(),
            startingDay: 1
        };

        // Disable weekend selection
        $scope.toggleMin = function () {
            $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
            $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
        };

        $scope.toggleMin();

        $scope.openvmsalarySummaryModeleffectiveFrom = function () {
            $scope.popupvmsalarySummaryModeleffectiveFrom.opened = true;
        };

        $scope.setDate = function (year, month, day) {
            $scope.dt = new Date(year, month, day);
        };

        $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];
        $scope.altInputFormats = ['M!/d!/yyyy'];

        $scope.popupvmsalarySummaryModeleffectiveFrom = {
            opened: false
        };

        var tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);
        var afterTomorrow = new Date();
        afterTomorrow.setDate(tomorrow.getDate() + 1);
        $scope.events = [
            {
                date: tomorrow,
                status: 'full'
            },
            {
                date: afterTomorrow,
                status: 'partially'
            }
        ]; //---------------------------
    }

    function addAppraisal() {
        return {
            templateUrl: '/employee/template/addAppraisal.tmpl.cshtml',
            controller: controller,
            controllerAs: 'vm'
        }
    }

    window.app.directive('addAppraisal', addAppraisal);
    controller.$inject = ['$scope', 'EmployeeService'];
})();