(function (app) {
    "use strict";

    app.directive("editVMonDnD", function() {
        return {
            scope: { twoway: "=" },
            restrict: 'A',
                link: function(scope, element, attrs) {
                    if (!scope.twoway) return;

                    function read() {
                        var html = element.html();

                        scope.twoway.$setViewValue(html);
                    }

                    element.on("ondrop keyup ondragover", function () {
                        scope.$evalAsync(read);
                    });
                    read(); // initialize
                }
            };
        });
})(angular.module('dishsetModule'));