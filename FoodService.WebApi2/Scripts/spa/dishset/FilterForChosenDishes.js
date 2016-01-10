(function (app) {
    'use strict';

    app.filter('unchosenDishes', function() {
        return function(allDishes, menuSet) {
            var out = [];
            for (var i = 0; i < allDishes.length; i++) {
                var addFlag = true;
                for (var j = 0; j < menuSet.length; j++) {
                    if (allDishes[i].ID == menuSet[j].ID) {
                        addFlag = false;
                        break;
                    }
                }
                if (addFlag) {
                    out.push(allDishes[i]);
                }
            }
            return out;
        }
    });


})(angular.module('dishsetModule'));