(function () {
    'use strict';
    window.app.service('EmployeeService', EmployeeService);
    EmployeeService.$inject = ['serviceHandler'];
    function EmployeeService(serviceHandler) {
        var salarySummaryModel = {};

        loadSalarySummaryModel();

        var svc = {
            add: add,
            //  update: update,
            salarySummaryModel: salarySummaryModel,
            // getCustomer: getCustomer
        };

        return svc;

        function loadSalarySummaryModel() {
            return serviceHandler.executePostService('/Employee/GetSalarySummaryModel').then(function (resp) {
                if (resp.businessException == null) {
                    salarySummaryModel = resp.result;
                }
                else {
                    toastr.error("Error!", resp.businessException.ExceptionMessage);
                }
            });
        }

        function add(salarySummaryModel) {
            salarySummaryModel.EmployeeId = $("#EmployeeId").val();
            return serviceHandler.executePostService('/Employee/SaveEmployeeSalaryDetail', salarySummaryModel);
        }

        //function update(existingCustomer, updatedCustomer) {
        //    return $http.post('/Customer/Update', updatedCustomer)
        //		.success(function (customer) {
        //		    angular.extend(existingCustomer, customer);
        //		});
        //}

        //function getCustomer(id) {
        //    for (var i = 0; i < customers.length; i++) {
        //        if (customers[i].Id == id) return customers[i];
        //    }

        //    return null;
        //}
    }
})();