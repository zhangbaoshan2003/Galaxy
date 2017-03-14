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
        $.getJSON('/Product/GetNetValues/?productId=100', function(data) {
            var chartBuilder = new TimeSeriesChartBuilder(data, GLOBAL_NETVALUE_CHART, true);
            chartBuilder.buildChart();
        });

        $.getJSON('/Product/GetPiggyBackDist/?productId=100', function(data) {
            var chartBuilder = new TimeSeriesChartBuilder(data, GLOBAL_PIGGYBACK_DIST_CHART, false);
            chartBuilder.buildChart();
        });

        $.getJSON('/Product/GetReturnDist/?productId=100', function(data) {
            var chartBuilder = new CategoryChartBuilder(data, GLOBAL_RETURN_DIST_CHART, false);
            chartBuilder.buildChart();
        });

        $.getJSON('/Product/GetPnlDist/?productId=100', function(data) {
            var chartBuilder = new CategoryChartBuilder(data, GLOBAL_PNL_DIST_CHART, false);
            chartBuilder.buildChart();
        });

        Global_netvalue_chart_initialized = true;
    }

});

var Global_asset_dist_chart_initialized = false;
$('#btnTabFour').click(function () {
    if (Global_asset_dist_chart_initialized == false) {
        $.getJSON('/Product/GetFuncAssetDist/?productId=100', function (data) {
            var chartBuilder = new CategoryChartBuilder(data, GLOBAL_FUND_DIST_CHART, true);
            chartBuilder.buildChart();
        });
        Global_asset_dist_chart_initialized = true;
    }
});