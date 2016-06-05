(function () {
    window.app.filter('parseDate', parseDate);

    function parseDate() {
        return function (input) {
            if (typeof input != 'string' || input.indexOf('/Date') === -1) return input;

            return new Date(parseInt(input.substr(6)));
        }
    }


    window.app.filter('sumByKey', sumByKey);

    function sumByKey() {
        return function (data, key) {
            if (typeof (data) === 'undefined' || typeof (key) === 'undefined') {
                return 0;
            }

            var componentForFooterTotal = ["Basic", "HRA", "Conveyance"
             , "Special Allowance", "Performance Incentive", "Leave encashment"
             , "Salary Arrears", "Cab Deductions", "Other Deduction"
             , "Commission", "Others", "Medical", "Food Coupons"];

            var sum = 0;
            for (var i = data.length - 1; i >= 0; i--) {
                var currentcomp = data[i]["ComponentName"];
                //console.log(currentcomp);
                if ($.inArray(currentcomp, componentForFooterTotal) != -1) {
                    sum += parseFloat(data[i][key]);
                }
            }

            return sum;
        };
    }
})();