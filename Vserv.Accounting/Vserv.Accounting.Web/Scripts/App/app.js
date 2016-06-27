(function () {
    'use strict';

    window.app = angular.module('VservAccountingApp', ['angular-loading-bar', 'ngAnimate', 'ui.bootstrap', 'ui.grid', 'xeditable', 'nvd3', 'data-table'])
                        .config(['cfpLoadingBarProvider', function (cfpLoadingBarProvider) { cfpLoadingBarProvider.includeSpinner = false; }])
                        //.config(['$locationProvider', function ($locationProvider) { $locationProvider.html5Mode(true); }])
                        .run(function (editableOptions, editableThemes) {
                            editableThemes.bs3.inputClass = 'input-sm';
                            editableThemes.bs3.buttonsClass = 'btn-sm';
                            editableOptions.theme = 'bs3'; // bootstrap3 theme. Can be also 'bs2', 'default'
                        });

    //cfpLoadingBar.start();
    //// will insert the loading bar into the DOM, and display its progress at 1%.
    //// It will automatically call `inc()` repeatedly to give the illusion that the page load is progressing.

    //cfpLoadingBar.inc();
    //// increments the loading bar by a random amount.
    //// It is important to note that the auto incrementing will begin to slow down as
    //// the progress increases.  This is to prevent the loading bar from appearing
    //// completed (or almost complete) before the XHR request has responded.

    //cfpLoadingBar.set(0.3) // Set the loading bar to 30%
    //cfpLoadingBar.status() // Returns the loading bar's progress.
    //// -> 0.3

    //cfpLoadingBar.complete()
    //// Set the loading bar's progress to 100%, and then remove it from the DOM.
})();