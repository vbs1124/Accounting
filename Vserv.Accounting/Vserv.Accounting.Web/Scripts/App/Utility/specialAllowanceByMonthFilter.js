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

            var deductedSum = null;
            var monthlyCTCAmount = null;
            for (var i = data.length - 1; i >= 0; i--) {
                var currentcomp = data[i]["SCCode"];

                if (currentcomp === "SCCTCM" && data[i][month] != null && !isNaN(data[i][month].Amount)) {
                    monthlyCTCAmount = $.vbsParseFloat(data[i][month].Amount);
                }

                if ($.inArray(currentcomp, deductedComponentFromMonthlyCTC) !== -1) {
                    if (data[i][month] != null && !isNaN(data[i][month].Amount)) {
                        deductedSum += $.vbsParseFloat(data[i][month].Amount);
                    }
                }
            }

            var specialAlloance = monthlyCTCAmount - deductedSum;
            if (isNaN(specialAlloance) || specialAlloance == 0) {
                return null;
            }
            return specialAlloance.toFixed(0);
        };
    }
})();