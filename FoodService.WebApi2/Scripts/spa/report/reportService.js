(function (app) {
    "use strict";

    app.factory("reportService", [
        "$http", "$q", function ($http, $q) {
            return {
                getallreports: function() {
                    var deferred = $q.defer();
                    $http.get("api/report/getallreports").
                        success(function(data) {
                            deferred.resolve(data);
                        }).
                        error(function(status) {
                            deferred.reject(status);
                        });
                    return deferred.promise;
                },

                sendMailToChef: function(dateInputMiliSec, mail) {
                    var deferred = $q.defer();
                    $http.get("api/report/sentmail?miliSecFrom1970=" + dateInputMiliSec + "&chefMail=" + mail).
                        success(function(data) {
                            deferred.resolve(data);
                        }).
                        error(function(status) {
                            deferred.reject(status);
                        });
                    return deferred.promise;
                },

                reportsformatch: function (dateInputMiliSec) {
                    var deferred = $q.defer();
                    $http.get("api/report/match?miliSecFrom1970=" + dateInputMiliSec).
                        success(function (data) {
                            deferred.resolve(data);
                        }).
                        error(function (status) {
                            deferred.reject(status);
                        });
                    return deferred.promise;
                }
            }
        }]);
})(angular.module("reportModule"))