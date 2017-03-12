function updateProductDetailTable() {
}

$(function () {
    var datePicker = $('#reservation').datepicker();
    var counter = 1;
    $('#btnDatePicker').click(function () {
        var gridDataSource = $('#productHoldingGrid').data('kendoGrid').dataSource;
        gridDataSource.read();
    });
   
})