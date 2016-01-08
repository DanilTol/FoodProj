function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
}

function drop(ev) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("text");
    var dishDiv = document.getElementById(data);

    if ($(dishDiv).parent().parent().attr('id') == 'allDishes')
    {
        var draggableEl = dishDiv.cloneNode(true);

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
