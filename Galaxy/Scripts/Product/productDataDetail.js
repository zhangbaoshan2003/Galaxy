$(function () {
    //initialize data table 
    $('#prodcutDetailTable').DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": false,
        "ordering": true,
        "info": false,
        "autoWidth": false,
        "scrollX": true,
        "sScrollX": "100%",
        "sScrollXInner": "110%",
        "bScrollCollapse": true
    });

    $('.dataTables_scrollHeadInner').css('width', '');

});