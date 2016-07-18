(function () {
    "use strict";
    window.app.directive('vbsTooltip', addEmployeeInvestments);
    function addEmployeeInvestments() {
        return function (scope, element, attrs) {
            element.find("[vbs=tooltip]").tooltip();
        };
    }

})();