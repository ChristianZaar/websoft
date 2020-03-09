function selectRow(trId, index){
    var tr = document.getElementById(trId);
    tr.onclick = function(event) {
    window.location.href = 'delete.php' + "?item=" +index;
    };
}