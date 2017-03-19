$('#netValueChartCtrlBtn').click(function () {
    $.getJSON('/Product/GetNetValues/?productId=100', function (data) {
        var chartBuilder = new TimeSeriesChartBuilder(data, GLOBAL_NETVALUE_CHART,true);
        chartBuilder.buildChart();
    });
});

$('#piggybackBtn').click(function (){
    $.getJSON('/Product/GetPiggyBackDist/?productId=100', function (data) {
        var chartBuilder = new TimeSeriesChartBuilder(data, GLOBAL_PIGGYBACK_DIST_CHART,false);
        chartBuilder.buildChart();
    });
});

$(window).resize(function () {
    //var realVal = $('#netValueDiv').width();
    //Global_NetValue_Chart.setSize(realVal + 100, realVal + 200, false)
});

var Global_netvalue_chart_initialized = false;
$('#btnTabTwo').click(function () {
    if (Global_netvalue_chart_initialized == false) {
        //GLOBAL_AS_OF_DATE = datePicker.val();
        //GLOBAL_PROD_ID = $('#hidenProductId').val();

        //var request = '/Product/{method}/?productId={pId}&asOfDateStr={asOfDate}';
        //$.getJSON(request.replace('{method}', 'GetNetValues').replace('{pId}', GLOBAL_PROD_ID).replace('{asOfDate}', GLOBAL_AS_OF_DATE), function (data) {
        //    var chartBuilder = new TimeSeriesChartBuilder(data, GLOBAL_NETVALUE_CHART, true);
        //    chartBuilder.buildChart();
        //});

        //$.getJSON(request.replace('{method}', 'GetPiggyBackDist').replace('{pId}', GLOBAL_PROD_ID).replace('{asOfDate}', GLOBAL_AS_OF_DATE), function(data) {
        //    var chartBuilder = new TimeSeriesChartBuilder(data, GLOBAL_PIGGYBACK_DIST_CHART, false);
        //    chartBuilder.buildChart();
        //});

        //$.getJSON(request.replace('{method}', 'GetReturnDist').replace('{pId}', GLOBAL_PROD_ID).replace('{asOfDate}', GLOBAL_AS_OF_DATE), function(data) {
        //    var chartBuilder = new CategoryChartBuilder(data, GLOBAL_RETURN_DIST_CHART, false);
        //    chartBuilder.buildChart();
        //});

        //$.getJSON(request.replace('{method}', 'GetPnlDist').replace('{pId}', GLOBAL_PROD_ID).replace('{asOfDate}', GLOBAL_AS_OF_DATE), function (data) {
        //    var chartBuilder = new CategoryChartBuilder(data, GLOBAL_PNL_DIST_CHART, false);
        //    chartBuilder.buildChart();
        //});
        Global_netvalue_chart_initialized = true;
    }

});

var Global_asset_dist_chart_initialized = false;
$('#btnTabFour').click(function () {
    if (Global_asset_dist_chart_initialized == false) {
        //$.getJSON('/Product/GetFuncAssetDist/?productId=100', function (data) {
        //    var chartBuilder = new CategoryChartBuilder(data, GLOBAL_FUND_DIST_CHART, true);
        //    chartBuilder.buildChart();
        //});

        //$.getJSON('/Product/GetEquitAssetDist/?productId=100', function (data) {
        //    var chartBuilder = new CategoryChartBuilder(data, GLOBAL_EQUITY_ASSET_DIST_CHART, true);
        //    chartBuilder.buildChart();
        //});
        Global_asset_dist_chart_initialized = true;
    }
});