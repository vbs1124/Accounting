(function () {
    function parseDate() {
        return function (input) {
            if (!input) return input;
            if (typeof input != "string" || input.indexOf("/Date") === -1) return moment(input).format("DD/MM/YYYY hh:mm:ss A");

            return moment(new Date(parseInt(input.substr(6)))).format("DD/MM/YYYY");
        }
    }

    window.app.filter("parseDate", parseDate);
})();