(function () {
	"use strict";

    function controller($scope) {
        var vm = this;

        vm.field = $scope.field;

        function canBeValidated() {
            return ($scope.form[vm.field].$touched || $scope.form.$submitted);
        }

        function isValid() {
            return $scope.form[vm.field].$valid;
        }

        function getValidationClass() {
            if (!canBeValidated()) return "";

            if (isValid()) return "has-success";

            return "has-error";
        }

        vm.getValidationClass = getValidationClass;
    }

    function formGroupValidation() {
        return {
            require: "^form",
            replace: true,
            transclude: true,
            template:
                "<div class=\"has-feedback\" ng-class=\"vm.getValidationClass()\">" +
                    "<ng-transclude></ng-transclude>" +
                    "<input-validation-icons field=\"vm.field\"></input-validation-icons>" +
                    "</div>",
            scope: {
                field: "@formGroupValidation"
            },
            controller: controller,
            controllerAs: "vm",
            link: function (scope, element, attrs, formCtrl) {
                scope.form = formCtrl;
            }
        }
    }

    window.app.directive("formGroupValidation", formGroupValidation);
    controller.$inject = ["$scope"];
})();