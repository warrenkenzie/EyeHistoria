function toggleCollapse(id) {
    var collapse = document.getElementById('collapse_' + id);
    if (collapse.classList.contains('show')) {
        collapse.classList.remove('show');
    } else {
        collapse.classList.add('show');
    }
}