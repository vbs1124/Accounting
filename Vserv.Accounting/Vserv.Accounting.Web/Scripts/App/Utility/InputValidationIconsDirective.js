(function () {
	"use strict";

    function controller($scope) {
        var vm = this;

        vm.field = $scope.field;

        function canBeValidated() {
            return ($scope.form[vm.field].$touched || $scope.form.$submitted);
        }

        vm.canBeValidated = canBeValidated;

        function isValid() {
            return $scope.form[vm.field].$valid;
        }

        vm.isValid = isValid;
    }

    function inputValidationIcons() {
        return {
            require: "^form",
            scope: {
                field: "="
            },
            template:
                "<span ng-show=\"vm.canBeValidated() && vm.isValid()\" " +
                    "class=\"fa fa-2x fa-check-square form-control-feedback\"></span>" +
                    "<span ng-show=\"vm.canBeValidated() && !vm.isValid()\" " +
                    "class=\"fa fa-2x fa-exclamation-triangle form-control-feedback\"></span>",
            controller: controller,
            controllerAs: "vm",
            link: function (scope, element, attrs, formCtrl) {
                scope.form = formCtrl;
            }
        }
    }

    window.app.directive("inputValidationIcons", inputValidationIcons);
    controller.$inject = ["$scope"];
})();