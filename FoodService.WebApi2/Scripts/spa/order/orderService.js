(function (app) {
    "use strict";

    app.factory("orderService", [
        "$http", "$q", function ($http, $q) {
            return  {
                getUserSet: function (date) {
                    date = date || new Date().getTime();

                    var deferred = $q.defer();
                    $http.get("api/order/getuserset?miliSecFrom1970=" + date).
                        success(function (data) {
                            deferred.resolve(data);
                        }).
                        error(function (status) {
                            deferred.reject(status);
                        });
                    return deferred.promise;
                },
                editUserSet: function (date, dishes) {
                    var deferred = $q.defer();
                    var setOnDay = {};
                    setOnDay.DishId = dishes;
                    setOnDay.Date = date;
                    $http.post("api/order/edituserset", setOnDay).
                        success(function (data) {
                            deferred.resolve(data);
                        }).
                        error(function (status) {
                            deferred.reject(status);
                        });
                    return deferred.promise;
                },
                getOrderList: function (date) {
                    var deferred = $q.defer();
                    $http.get("api/order/orderlist?miliSecFrom1970=" + date).
                        success(function (data) {
                            deferred.resolve(data);
                        }).
                        error(function (status) {
                            deferred.reject(status);
                        });
                    return deferred.promise;
                },
                deleteOrders: function(orders) {
                    var deferred = $q.defer();
                    $http.post("api/order/ordersdelete", orders).
                        success(function (data) {
                            deferred.resolve(data);
                        }).
                        error(function (status) {
                            deferred.reject(status);
                        });
                    return deferred.promise;
                },
                sendMailToChef: function(mail) {
                    var deferred = $q.defer();
                    $http.post("api/order/sendmail", mail).
                        success(function (data) {
                            deferred.resolve(data);
                        }).
                        error(function (status) {
                            deferred.reject(status);
                        });
                    return deferred.promise;
                }
            }
        }
    ]);
})(angular.module("orderModule"))