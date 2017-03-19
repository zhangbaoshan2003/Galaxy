function updateProductDetailTable() {
}
var GLOBAL_AS_OF_DATE='1/1/2017';
var GLOBAL_PROD_ID;
$(function () {
    var datePicker = $('#reservation').datepicker("setDate", GLOBAL_AS_OF_DATE);
    $('#btnDatePicker').click(function () {
        GLOBAL_AS_OF_DATE = datePicker.val();
        GLOBAL_PROD_ID = $('#hidenProductId').val();
        var gridDataSource = $('#productHoldingGrid').data('kendoGrid').dataSource;
        gridDataSource.read({ asOfDateStr: GLOBAL_AS_OF_DATE, productId: GLOBAL_PROD_ID });

        var request = '/Product/{method}/?productId={pId}&asOfDateStr={asOfDate}';
        $.getJSON(request.replace('{method}', 'GetNetValues').replace('{pId}', GLOBAL_PROD_ID).replace('{asOfDate}', GLOBAL_AS_OF_DATE), function (data) {
            var chartBuilder = new TimeSeriesChartBuilder(data, GLOBAL_NETVALUE_CHART, true);
            chartBuilder.buildChart();
        });

        $.getJSON(request.replace('{method}', 'GetReturnDist').replace('{pId}', GLOBAL_PROD_ID).replace('{asOfDate}', GLOBAL_AS_OF_DATE), function (data) {
            var chartBuilder = new CategoryChartBuilder(data, GLOBAL_RETURN_DIST_CHART, false);
            chartBuilder.buildChart();
        });

        $.getJSON(request.replace('{method}', 'GetPnlDist').replace('{pId}', GLOBAL_PROD_ID).replace('{asOfDate}', GLOBAL_AS_OF_DATE), function (data) {
            var chartBuilder = new CategoryChartBuilder(data, GLOBAL_PNL_DIST_CHART, false);
            chartBuilder.buildChart();
        });

        $.getJSON(request.replace('{method}', 'GetFuncAssetDist').replace('{pId}', GLOBAL_PROD_ID).replace('{asOfDate}', GLOBAL_AS_OF_DATE), function (data) {
            var chartBuilder = new CategoryChartBuilder(data, GLOBAL_FUND_DIST_CHART, true);
            chartBuilder.buildChart();
        });

        $.getJSON(request.replace('{method}', 'GetEquitAssetDist').replace('{pId}', GLOBAL_PROD_ID).replace('{asOfDate}', GLOBAL_AS_OF_DATE), function (data) {
            var chartBuilder = new CategoryChartBuilder(data, GLOBAL_EQUITY_ASSET_DIST_CHART, true);
            chartBuilder.buildChart();
        });

        $.getJSON(request.replace('{method}', 'GetCurrentFuncAssetDist').replace('{pId}', GLOBAL_PROD_ID).replace('{asOfDate}', GLOBAL_AS_OF_DATE), function (data) {
            var chartBuilder = new CategoryChartBuilder(data, GLOBAL_ASSET_DIST_CHART, false);
            chartBuilder.buildChart();
        });
    });
   
})