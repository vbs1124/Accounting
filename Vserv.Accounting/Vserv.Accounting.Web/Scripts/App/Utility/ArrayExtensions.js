(function() {
	"use strict";

	Object.defineProperty(Array.prototype, "count", {
		get: function () { return this.length; }
	});

	if (Array.prototype.addRange) return;

	Array.prototype.addRange = function (target) {
		this.push.apply(this, target);
	};

	Array.prototype.removeAll = function () {
	    this.splice(0, this.count);
	};
})();