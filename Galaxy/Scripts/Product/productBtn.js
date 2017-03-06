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