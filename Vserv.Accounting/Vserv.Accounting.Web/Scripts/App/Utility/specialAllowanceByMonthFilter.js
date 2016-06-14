(function () {
    window.app.filter("specialAllowanceByMonth", specialAllowanceByMonth);
    function specialAllowanceByMonth() {
        return function (data, month) {
            if (typeof (data) === "undefined" || typeof (month) === "undefined") {
                return 0;
            }

            var deductedComponentFromMonthlyCTC = [
                    "SCBASC",
                    "SCSHRA",
                    "SCCONV",
                    "SCPERF",
                    "SCMEDC",
                    "SCFCPN",
                    "SCPROJ",
                    "SCCARL",
                    "SCTLTC",
                    "SCEPFO",
                    "SCMEDM",
                    "SCGRAT",
                    "SCCABD",
            ];

            var deductedSum = 0;
            var monthlyCTCAmount = 0;
            for (var i = data.length - 1; i >= 0; i--) {
                var currentcomp = data[i]["SCCode"];

                if (currentcomp === "SCCTCM" && !isNaN(data[i][month])) {
                    monthlyCTCAmount = parseFloat(data[i][month]);
                }

                if ($.inArray(currentcomp, deductedComponentFromMonthlyCTC) !== -1) {
                    if (!isNaN(data[i][month])) {
                        deductedSum += parseFloat(data[i][month]);
                    }
                }
            }

            var specialAlloance = monthlyCTCAmount - deductedSum;
            if (isNaN(specialAlloance)) {
                return null;
            }
            return specialAlloance.toFixed(0);
        };
    }
})();