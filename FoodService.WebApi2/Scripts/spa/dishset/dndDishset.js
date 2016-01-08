function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
}

function drop(ev) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("text");
    var dishDiv = document.getElementById("dishMenu").childNodes[data];

    if (typeof dishDiv == "undefined")
    {
        //data = ev.dataTransfer.getData("text");
        var draggableEl = document.getElementById("allDishes").childNodes[parseInt(data)+1].childNodes(data).cloneNode(true);
        draggableEl.removeAttribute("draggable");
        draggableEl.removeAttribute("ondragstart");
        draggableEl.setAttribute("ondrop", "e.preventDefault(); return false;");

        draggableEl.innerHTML = '<button class="btn btn-danger cancelButton" onclick="removeDishFromMenu(this)"><i class="glyphicon glyphicon-remove"></i></button>' + draggableEl.innerHTML;

        ev.target.appendChild(draggableEl);
    }
}

function removeDishFromMenu(el) {
    var parent = document.getElementById('dishMenu');
    var child = el.parentNode;
    parent.removeChild(child);
}
