
/// <reference path="Scripts/Controllers/RouteController.js" />


var module = angular.module("moduleregistration", ["ngRoute"]);

/********************Registration************************/
module.controller("MailCtrl", mailCtrl);
module.controller("MailInfoCtrl", mailInfo);
/*******************Utility************************/
//module.directive("myCustomDirective", function () {
//    return {
//        templateUrl: "templates/CustomDirectivetemplate.html"
//    }
//});


module.config(function ($routeProvider, $locationProvider) {
    $routeProvider
        .when("/Home", {
            templateUrl: "templates/Mail.html",
            controller: "MailCtrl"
        })

        .when("/Info", {
            templateUrl: "templates/MailInfo.html",
            controller: "MailInfoCtrl"
        })

        .otherwise("/Home", {
            templateUrl: "templates/student.html",
            controller: "MailCtrl"
        })
    // $locationProvider.html5Mode(true);
    $locationProvider.hashPrefix('');

});

