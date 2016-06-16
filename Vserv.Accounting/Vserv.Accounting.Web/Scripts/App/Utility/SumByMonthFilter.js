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
                    sum += $.vbsParseFloat(data[i][month].Amount);
                }
            }

            return sum;
        };
    }
})();