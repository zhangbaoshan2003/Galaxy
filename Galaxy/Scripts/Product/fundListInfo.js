$(function () {
    //initialize data table 
    $('#productListTable').DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "scrollX": false,
        "columns": [
            { "width": "120px" },
            { "width": "380px" },
            { "width": "200px" },
            null,
            null
        ]
    });
});