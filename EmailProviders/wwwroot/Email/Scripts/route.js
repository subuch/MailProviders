
/// <reference path="Scripts/Controllers/RouteController.js" />


var module = angular.module("moduleregistration", ["ngRoute"]);
var serverURL = 'http://localhost:16185/api/'
/********************Registration************************/
module.controller("MailInfoCtrl", mailCtrl);
module.controller("SendMailCtrl", sendCtrl);


module.config(function ($routeProvider, $locationProvider) {
    $routeProvider
        .when("/Home", {
            templateUrl: "templates/Mail.html",
            controller: "MailInfoCtrl"
        })

        .when("/Info", {
            templateUrl: "templates/SendMail.html",
            controller: "SendMailCtrl"
        })

        .otherwise("/Home", {
            templateUrl: "templates/Mail.html",
            controller: "MailInfoCtrl"
        })

    $locationProvider.hashPrefix('');

});

/*******RegisteredFactory******/
module.factory("APIServiceFactory", ['$http', '$log', function ($http, $log) {


    var returnObj = {};
    returnObj.getAPICalls = function (actionName, attribute) {
        return $http.get(serverURL + actionName + "/" + attribute)
            .then(function (response) {
                $log.info(response);
                return response;
            }, function (reason) {
                $log.info(reason);
                return reason;
            })
    }

    returnObj.getAPICallsWithModel = function (method, objModel) {

        return $http({
            method: 'POST',
            headers: { 'Content-Type': 'Application/JSON' },
            data: objModel,
            url: serverURL + method
        })
            .then(function (response) {
                $log.info(response);
                return response;
            }, function (reason) {
                $log.info(reason);
                return reason;
            });
    }

    return returnObj;
}]);


