(function (app) {
    "use strict";

    app.factory("dishService", [
        "$http", "$q", "fileUploadService", function($http, $q, fileUploadService) {


            function search(page, pageSize, filterDishes) {
                page = page || 0;
                pageSize = pageSize || 6;

                var deferred = $q.defer();
                $http.get("api/dishes/search?page=" + page + "&pageSize=" + pageSize + "&filter=" + filterDishes).
                    success(function(data) {
                        deferred.resolve(data);
                    }).
                    error(function(data, status) {
                        deferred.reject(status);
                    });
                return deferred.promise;
            }

            function loadDishDetails(idDish) {
                var deferred = $q.defer();
                $http.get("/api/dishes/details/" + idDish).
                    success(function(data) {
                        deferred.resolve(data);
                    }).
                    error(function(data, status) {
                        deferred.reject(status);
                    });

                return deferred.promise;
            }

            function updateDish(dish) {
                var deferred = $q.defer();
                $http.post("/api/dishes/update", dish).
                    success(function(data) {
                        deferred.resolve(data);
                    }).
                    error(function(data, status) {
                        deferred.reject(status);
                    });

                return deferred.promise;
            }

            function addDish(dish) {
                var deferred = $q.defer();
                fileUploadService.saveModel(dish, "/api/dishes/add").then
                (function(data) {
                        deferred.resolve(data);
                    },
                    function(status) {
                        deferred.reject(status);
                    });
                return deferred.promise;
            }


            function deleteDish(dishId) {
                var deferred = $q.defer();
                $http.post("/api/dishes/delete", parseInt(dishId)).
                    success(function(data) {
                        deferred.resolve(data);
                    }).
                    error(function(data, status) {
                        deferred.reject(status);
                    });
                return deferred.promise;
            }

            var service = {
                search: search,
                loadDishDetails: loadDishDetails,
                updateDish: updateDish,
                addDish: addDish,
                deleteDish: deleteDish
            }
            return service;
        }
    ]);
})(angular.module("dishModule"))