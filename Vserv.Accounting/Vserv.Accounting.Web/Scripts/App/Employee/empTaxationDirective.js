(function () {
    "use strict";
    window.app.directive('empTaxation', empTaxation);
    function empTaxation() {
        return {
            templateUrl: '/employee/template/empTaxation.tmpl.cshtml',
            controller: empTaxationcontroller,
            controllerAs: 'vm',
            scope: {
                vmodel: "="
            }
        }
    }

    empTaxationcontroller.$inject = ['$scope', '$uibModal', 'employeeService'];

    function empTaxationcontroller($scope, $modal, employeeService) {
        var vm = this;
        vm.employee = $scope.vmodel;
        vm.selectedInvestmentFinancialYear = vm.employee.selectedInvestmentFinancialYear;
        vm.joiningDate = vm.employee.joiningDate;
        vm.relievingDate = vm.employee.relievingDate;
        vm.financialYears = employeeService.getEmpFinancialYears(vm.joiningDate, vm.relievingDate);

        vm.loadEmployeeTaxation = employeeService.loadEmployeeTaxation(vm.employee.employeeId, vm.selectedInvestmentFinancialYear);
        vm.employeeTaxation = employeeService.employeeTaxation;

        $scope.onChangeInvestmentFinancialYear = function () {
            employeeService.loadEmployeeTaxation(vm.employee.employeeId, vm.selectedInvestmentFinancialYear);
        }

        $scope.options = {
            chart: {
                type: 'pieChart',
                height: 400,
                width: 420,
                donut: false,
                x: function (d) { return d.key; },
                y: function (d) { return d.y; },
                showLabels: false,
                pie: {
                    startAngle: function (d) { return d.startAngle / 2 - Math.PI / 2 },
                    endAngle: function (d) { return d.endAngle / 2 - Math.PI / 2 }
                },
                duration: 500,
                legend: {
                    margin: {
                        top: 5,
                        right: 5,
                        bottom: 5,
                        left: 0
                    }
                }
            }
        };

        $scope.data = [
            {
                key: "Take Home",
                y: 82.1
            },
            {
                key: "Income Tax",
                y: 6.4
            },
            {
                key: "Gratuity",
                y: 1.9
            },
            {
                key: "PF + EPF + EPS",
                y: 9.6
            }
        ];

        $scope.optionsMultiBarHorizontalChart = {
            chart: {
                type: 'multiBarHorizontalChart',
                height: 120,
                width: 380,
                x: function (d) { return d.label; },
                y: function (d) { return d.value; },
                showControls: false,
                showValues: false,
                duration: 500,
                xAxis: {
                    showMaxMin: false
                },
                yAxis: {
                    axisLabel: 'Values',
                    tickFormat: function (d) {
                        return d3.format(',.2f')(d);
                    }
                }
            }
        };

        $scope.dataMultiBarHorizontalChart = [
            {
                "key": "Tax Saving",
                "color": "#8b0000",
                "values": [
                    {
                        "label": "Current",
                        "value": 6
                    },
                    {
                        "label": "Potential",
                        "value": 10
                    }
                ]
            }
        ];
    }
})();