function selectRow(trId, index, page){
    var tr = document.getElementById(trId);
    tr.onclick = function(event) {
        window.location.href = page + "?item=" + index;
    };
}