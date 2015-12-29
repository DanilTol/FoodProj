(function(app) {
    'use strict';
    app.factory('sessionInjector', function() {
        var sessionInjector = {
            request: function(config) {
                if (sessionStorage.foodServiceToken) {
                    config.headers['Token'] = sessionStorage.foodServiceToken;
                }
                return config;
            }
        };
        return sessionInjector;
    });
    app.config([
        '$httpProvider', function($httpProvider) {
            $httpProvider.interceptors.push('sessionInjector');
        }
    ]);
})(angular.module("common.core"));