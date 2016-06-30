(function () {
    "use strict";
    window.app.directive('vbsPopover', addEmployeeInvestments);
    function addEmployeeInvestments() {
        return function (scope, element, attrs) {
            element.find("i[vbs=popover]").popover();
        };
    }

})();