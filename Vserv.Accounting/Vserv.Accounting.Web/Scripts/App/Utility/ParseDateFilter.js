(function () {
    function parseDate() {
        return function (input) {
            if (typeof input != "string" || input.indexOf("/Date") === -1) return input;

            return new Date(parseInt(input.substr(6)));
        }
    }

    window.app.filter("parseDate", parseDate);
})();