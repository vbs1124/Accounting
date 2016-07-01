(function () {
    function parseDate() {
        return function (input) {
            if (typeof input != "string" || input.indexOf("/Date") === -1) return input;

            return moment(new Date(parseInt(input.substr(6)))).format("DD/MM/YYYY");
        }
    }

    window.app.filter("parseDate", parseDate);
})();