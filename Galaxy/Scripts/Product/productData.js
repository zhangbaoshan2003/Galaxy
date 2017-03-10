$(function () {
    //initialize data table 
    $('#productListTable').DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        //"info": true,
        //"autoWidth": false,
        //"scrollX": true,
        //"sScrollX": "100%",
        //"sScrollXInner": "110%",
        //"bScrollCollapse": true
    });
});

$('#productListTable tbody').on('click', 'tr', function () {
    $(this).toggleClass('selected');
});