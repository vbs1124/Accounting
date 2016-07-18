(function () {
    window.app.filter("sumByMonth", sumByMonth);
    function sumByMonth() {
        return function (data, month) {
            if (typeof (data) === "undefined" || typeof (month) === "undefined") {
                return 0;
            }

            var componentForFooterTotal = [
                                            "SCBASC",
                                            "SCSHRA",
                                            "SCCONV",
                                            "SCSPCL",
                                            "SCPERF",
                                            "SCLECM",
                                            "SCSALA",
                                            "SCCABD",
                                            "SCODN",
                                            "SCCOMN",
                                            "SCOTHR",
                                            "SCMEDC",
                                            "SCFCPN"];

            var sum = 0;
            for (var i = data.length - 1; i >= 0; i--) {
                var currentcomp = data[i]["SCCode"];
                if ($.inArray(currentcomp, componentForFooterTotal) !== -1) {
                    if (data[i][month] != null) {
                        sum += $.vbsParseFloat(data[i][month].Amount);
                    }
                }
            }

            return sum == 0 ? null : sum;
        };
    }

    window.app.filter("sumByColumn", sumByColumn);
    function sumByColumn() {
        return function (data, column) {
            if (typeof (data) === "undefined" || typeof (column) === "undefined") {
                return 0;
            }

            var sum = 0;

            for (var i = data.length - 1; i >= 0; i--) {
                if (data[i][column] != null) {
                    sum += $.vbsParseFloat(data[i][column]);
                }
            }

            return sum == 0 ? 0 : sum;
        };
    }
})();