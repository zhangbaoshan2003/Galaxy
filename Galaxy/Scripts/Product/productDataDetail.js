var Global_oTable;
$('#btnTabOne').click(function () {
    setTimeout(function () { Global_oTable.draw(); }, 1);
});
$(function () {
    //initialize data table 
    Global_oTable = $('#prodcutDetailTable').DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": false,
        "autoWidth": true,
        "scrollX": true,
        "sScrollX": "100%",
        "sScrollXInner": "110%"
    });

    //fix display issue for a datatable in a tab div initially
    //$('div.dataTables_scrollHeadInner').css('width', '110%');
    //$('div.dataTables_scrollHeadInner table').css('width', '110%');
});