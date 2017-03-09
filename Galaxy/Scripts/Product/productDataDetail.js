$(function () {
    //initialize data table 
    $('#prodcutDetailTable').DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": false,
        "autoWidth": false,
        "scrollX": true,
        "sScrollX": "100%",
        "sScrollXInner": "110%",
        "bScrollCollapse": true
    });

    //fix display issue for a datatable in a tab div initially
    $('div.dataTables_scrollHeadInner').css('width', '110%');
    $('div.dataTables_scrollHeadInner table').css('width', '110%');
});