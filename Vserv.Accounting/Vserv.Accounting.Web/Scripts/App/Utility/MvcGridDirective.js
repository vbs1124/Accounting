(function () {
    function controller($scope, $http) {
        var vm = this;

        vm.gridOptions = {
            enableHorizontalScrollbar: 0,
            enableFiltering: true
        }

        vm.loading = true;

        vm.title = $scope.title;

        if ($scope.columns)
            vm.gridOptions.columnDefs = angular.fromJson($scope.columns);

        $http.post($scope.gridDataUrl)
            .success(function (data) {
                vm.gridOptions.data = data;
                vm.loading = false;
            });
    }

    function mvcGrid() {
        return {
            scope: {
                gridDataUrl: "@",
                title: "@",
                columns: "@?"
            },
            template:
                "<div>" +
                    //'<h4><i class="fa fa-pie-chart fa-fw"></i> {{vm.title}}</h4>' +
                    "<div>" +
                    "<p ng-if=\"vm.loading\">Loading...</p>" +
                    "<div ng-if=\"!vm.loading\" ui-grid=\"vm.gridOptions\" ui-grid-edit></div>" +
                    "</div>" +
                    "</div>",
            controllerAs: "vm",
            controller: controller
        }
    }

    window.app.directive("mvcGrid", mvcGrid);
    controller.$inject = ["$scope", "$http"];
})();
