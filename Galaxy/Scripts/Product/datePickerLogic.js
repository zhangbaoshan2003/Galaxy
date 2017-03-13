function updateProductDetailTable() {
}
var GLOBAL_AS_OF_DATE;

$(function () {
    var datePicker = $('#reservation').datepicker();
    var counter = 1;
    $('#btnDatePicker').click(function () {
        GLOBAL_AS_OF_DATE = datePicker.val();

        var gridDataSource = $('#productHoldingGrid').data('kendoGrid').dataSource;
        gridDataSource.read({ asOfDate: GLOBAL_AS_OF_DATE, productId: $('#hidenProductId').val() });

        $.getJSON('/Product/GetReturnDist/?productId=100', function (data) {
            var chartBuilder = new CategoryChartBuilder(data, GLOBAL_RETURN_DIST_CHART, false);
            chartBuilder.buildChart();
        });

        $.getJSON('/Product/GetPnlDist/?productId=100', function (data) {
            var chartBuilder = new CategoryChartBuilder(data, GLOBAL_PNL_DIST_CHART, false);
            chartBuilder.buildChart();
        });
    });
   
})