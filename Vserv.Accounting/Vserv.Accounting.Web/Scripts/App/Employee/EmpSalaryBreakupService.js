(function () {
    'use strict';
    window.app.factory('empSalaryBreakupService', empSalaryBreakupService);

    empSalaryBreakupService.$inject = ['serviceHandler'];
    function empSalaryBreakupService(serviceHandler) {
        var salaryBreakups = [];

        loadSalaryBreakup();

        var svc = {
            //add: add,
            //update: update,
            salaryBreakups: salaryBreakups,
            //getCustomer: getCustomer
        };

        return svc;

        function loadSalaryBreakup(employeeId, isCurrentBreakup) {
            serviceHandler.executeGetService('/Employee/GetSalaryBreakup').then(function (resp) {
                if (resp.businessException == null) {
                    salaryBreakups.addRange(resp.result);
                }
                else {
                    toastr.error("Error!", resp.businessException.ExceptionMessage);
                }
            });
        }

        //function add(customer) {
        //    return $http.post('/Customer/Add', customer)
		//		.success(function (customer) {
		//		    customers.unshift(customer);
		//		});
        //}

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