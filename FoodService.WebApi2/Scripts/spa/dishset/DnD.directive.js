(function (app) {
    "use strict";
    app.directive("lvlDraggable", function () {
        return {
            restrict: "A",
            link: function (scope, el, attrs, controller) {
                angular.element(el).attr("draggable", "true");

                el.bind("dragstart", function (e) {
                    e.originalEvent.dataTransfer.setData("text", e.target.id);
                });
            }
        };
    });

    app.directive("lvlDropTarget", function () {
        return {
            restrict: "A",
            scope: {
                onDrop: "&",
                dishSet: "="
            },
            link: function (scope, el, attrs, controller) {
                var id = angular.element(el).attr("id");
                
                el.bind("dragover", function (e) {
                    if (e.preventDefault) {
                        e.preventDefault(); // Necessary. Allows us to drop.
                    }
                    e.originalEvent.dataTransfer.dropEffect = 'move';  // See the section on the DataTransfer object.
                    return false;
                });

                el.bind("drop", function (e) {
                    if (e.preventDefault) {
                        e.preventDefault(); // Necessary. Allows us to drop.
                    }

                    if (e.stopPropagation) {
                        e.stopPropagation(); // Necessary. Allows us to drop.
                    }

                    var data = e.originalEvent.dataTransfer.getData("text");
                    var dishDiv = document.getElementById(data);
                    var draggableEl = dishDiv.parentNode;//.cloneNode(true);

                        var someDish = {};
                        someDish.ID = parseInt(data);
                        if (draggableEl.childNodes.length<5) {
                            someDish.ImagePath = draggableEl.childNodes[1].style["backgroundImage"];
                            someDish.Name = draggableEl.childNodes[1].childNodes[1].innerHTML.trim();
                        } else {
                            someDish.ImagePath = draggableEl.childNodes[2].style["backgroundImage"];
                            someDish.Name = draggableEl.childNodes[2].childNodes[1].innerHTML.trim();
                        }
                        var index = someDish.ImagePath.lastIndexOf("/") + 1;
                        someDish.ImagePath = someDish.ImagePath.substring(index, someDish.ImagePath.length - 2);

                        scope.onDrop({someDish: someDish });
                });
            }
        };
    });

})(angular.module("navbarModule"));