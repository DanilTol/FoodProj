(function(app) {
    'use strict';
    app.factory('sessionInjector', ['$location',function($location) {
        var sessionInjector = {
            request: function(config) {
                if (sessionStorage.foodServiceToken) {
                    config.headers['Token'] = sessionStorage.foodServiceToken;
                }
                return config;
            },
            'responseError': function(rejection) {
                if (rejection.status == 401) {
                    $location.path('/');
                }
            }
        };
        return sessionInjector;
    }]);
    app.config([
        '$httpProvider', function($httpProvider) {
            $httpProvider.interceptors.push('sessionInjector');
        }
    ]);
})(angular.module("common.core"));