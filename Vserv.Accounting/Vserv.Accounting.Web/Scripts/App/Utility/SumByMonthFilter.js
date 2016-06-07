(function () {
    window.app.filter('sumByMonth', sumByMonth);

    function sumByMonth() {
        return function (data, month) {
            if (typeof (data) === 'undefined' || typeof (month) === 'undefined') {
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
                    sum += parseFloat(data[i][month]);
                }
            }

            return sum.toFixed(0);
        };
    }
})();