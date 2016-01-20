(function (app) {
    "use strict";

    app.factory("accountService", ["$http", "$q", function ($http, $q) {
        var userProfile;

        function editProfile(user) {
            var deferred = $q.defer();
            $http.post("/api/account/edit", user).
                success(function(data) {
                     deferred.resolve(data);
                }, function() {
                     deferred.reject(status);
                });
            return deferred.promise;
        }

        function getUserData() {
            if (angular.isUndefined(userProfile) && isUserLoggedIn()) {
                userProfile = {};
                userProfile.Name = "";
                $http.get("/api/account/profileInfo").
                success(function (data1) {
                    userProfile = data1;
                }).
                error(function (data1, status) {
                });
            }
            return userProfile;
        }

        function login(user) {
            var deferred = $q.defer();
            $http.post("/api/account/login", user).
                success(function (data) {

                    sessionStorage.foodServiceToken = data;

                    $http.get("/api/account/profileInfo").
                success(function (data1) {
                    userProfile = data1;
                }).
                error(function (data1, status) {
                });
                    deferred.resolve(data);
                }).
                error(function (data, status) {
                    deferred.reject(status);
                });

            return deferred.promise;
        }

        function register(user) {
            var deferred = $q.defer();
            $http.post("/api/account/register", user).
                success(function (data) {
                    sessionStorage.foodServiceToken = data;

                    deferred.resolve(data);
                }).
                error(function (data, status) {

                    deferred.reject(status);
                });
            return deferred.promise;
        }

        function logoutUser() {
            sessionStorage.clear();
        }

        function isUserLoggedIn() {
            return sessionStorage.foodServiceToken != null;
        }

        function getUserAsync() {
                            var deferred = $q.defer();
                            $http.get("/api/account/profileInfo").
                                success(function (data) {
                                    deferred.resolve(data);
                                }).
                                error(function (data1, status) {
                                    deferred.reject(status);
                                });
                            return deferred.promise;
                        }




        var service = {
            login: login,
            register: register,
            logoutUser: logoutUser,
            isUserLoggedIn: isUserLoggedIn,
            getUserData: getUserData,
            getUserAsync: getUserAsync,
            editProfile: editProfile
        }
        return service;
    }
    ]);
})(angular.module("accountModule"));


//(function (app) {
//    "use strict";

//    app.factory("accountService", ["$http", "$q", function ($http, $q) {
//        var userProfile;

//        return {

//            login: function (user) {
//                var deferred = $q.defer();
//                $http.post("/api/account/login", user).
//                    success(function (data) {

//                        sessionStorage.foodServiceToken = data;

//                        $http.get("/api/account/profileInfo").
//                    success(function (data1) {
//                        userProfile = data1;
//                    }).
//                    error(function (data1, status) {
//                    });
//                        deferred.resolve(data);
//                    }).
//                    error(function (data, status) {
//                        deferred.reject(status);
//                    });

//                return deferred.promise;
//            },


//            register: function (user) {
//                var deferred = $q.defer();
//                $http.post("/api/account/register", user).
//                    success(function (data) {
//                        sessionStorage.foodServiceToken = data;

//                        deferred.resolve(data);
//                    }).
//                    error(function (data, status) {

//                        deferred.reject(status);
//                    });
//                return deferred.promise;
//            }
//,

//            logoutUser: function () {
//                sessionStorage.clear();
//            },


//            isUserLoggedIn: function () {
//                return sessionStorage.foodServiceToken != null;
//            },


//            getUserData: function () {
//                if (angular.isUndefined(userProfile) && isUserLoggedIn()) {
//                    userProfile = {};
//                    userProfile.Name = "";
//                    $http.get("/api/account/profileInfo").
//                    success(function (data1) {
//                        userProfile = data1;
//                    }).
//                    error(function (data1, status) {
//                    });
//                }
//                return userProfile;
//            },


//            editProfile: function (user) {
//                var deferred = $q.defer();
//                $http.post("/api/account/edit", user).
//                    success(function () {
//                        deferred.resolve(data);
//                    }, function () {
//                        deferred.reject(status);
//                    });
//                return deferred.promise;
//            },


//            getUserAsync: function () {
//                var deferred = $q.defer();
//                $http.get("/api/account/profileInfo").
//                    success(function (data) {
//                        deferred.resolve(data);
//                    }).
//                    error(function (data1, status) {
//                        deferred.reject(status);
//                    });
//                return deferred.promise;
//            }
//        }
//    }
//    ]);
//})(angular.module("accountModule"));
