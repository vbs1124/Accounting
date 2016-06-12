(function () {
    'use strict';

    function securityQuestionsController(accountService) {
        var vm = this;
        // vm.add = add;
        vm.securityQuestions = accountService.securityQuestions;


        //function add() {
        //    $modal.open({
        //        template: '<add-customer />'
        //    });
        //}
    }

    window.app.controller('SecurityQuestionsController', securityQuestionsController);
    securityQuestionsController.$inject = ['AccountService'];
})();