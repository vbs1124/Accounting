(function () {
    'use strict';
    window.app.controller('SecurityQuestionsController', SecurityQuestionsController);
    SecurityQuestionsController.$inject = ['AccountService'];

    function SecurityQuestionsController(AccountService) {
        var vm = this;
       // vm.add = add;
        vm.securityQuestions = AccountService.securityQuestions;


        //function add() {
        //    $modal.open({
        //        template: '<add-customer />'
        //    });
        //}
    }
})();