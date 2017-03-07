﻿var GLOBAL_NETVALUE_CHART;
var GLOBAL_PIGGYBACK_DIST_CHART;
var GLOBAL_ASSET_DIST_CHART;
var GLOBAL_RETURN_DIST_CHART;
var GLOBAL_PNL_DIST_CHART

GLOBAL_NETVALUE_CHART = new Highcharts.stockChart({
    chart: {
        // Edit chart spacing
        spacingBottom: 5,
        spacingTop: 5,
        spacingLeft: 5,
        spacingRight: 5,
        backgroundColor: null,
        renderTo: 'netvalueChartContainer',

        // Explicitly tell the width and height of a chart
        width: null,
        //width:400,
        height: null
    },

    rangeSelector: {
        selected: 1
    },

    credits: {
        enabled: false
    }
});

GLOBAL_PIGGYBACK_DIST_CHART = new Highcharts.chart({
    chart: {
        // Edit chart spacing
        spacingBottom: 5,
        spacingTop: 5,
        spacingLeft: 5,
        spacingRight: 5,
        backgroundColor: null,
        renderTo: 'pigbackRateChartContainer',
        type: 'area',
        width: null,
        height: null
    },
    title: {
        text:null
    },

    xAxis: {
        type: 'datetime'
    },
    yAxis: {
        title: {
            text: null
        }
    },
    plotOptions: {
        area: {
            fillOpacity: 0.5
        }
    },
    credits: {
        enabled: false
    },
    legend: {
        align: 'right',
        verticalAlign: 'top',
        floating: true,
        enabled: false
    }
});

GLOBAL_RETURN_DIST_CHART = new Highcharts.chart({
    chart: {
        // Edit chart spacing
        spacingBottom: 0,
        spacingTop: 5,
        spacingLeft: 5,
        spacingRight: 5,
        backgroundColor: null,
        renderTo: 'returnDistChartContainer',
        type: 'column',
        width: null,
        height: null
    },
    title: {
        text: null
    },

    xAxis: {
        type: 'category',
        labels: {
            rotation: -45,
            style: {
                fontSize: '10px',
                fontFamily: 'Verdana, sans-serif'
            }
        }
    },
    yAxis: {
        min: 0,
        title: null,
        labels: {
            overflow: 'justify'
        }
    },
    credits: {
        enabled: false
    },
    legend: {
        align: 'right',
        verticalAlign: 'top',
        floating: true,
        enabled:false
    }
});

GLOBAL_PNL_DIST_CHART = new Highcharts.chart({
    chart: {
        // Edit chart spacing
        spacingBottom: 0,
        spacingTop: 5,
        spacingLeft: 5,
        spacingRight: 5,
        backgroundColor: null,
        renderTo: 'pnlChartContainer',
        type: 'pie',
        width: null,
        height: null
    },
    title: {
        text: null
    },

    xAxis: {
        type: 'category',
        labels: {
            rotation: -45,
            style: {
                fontSize: '10px',
                fontFamily: 'Verdana, sans-serif'
            }
        }
    },
    yAxis: {
        min: 0,
        title: null,
        labels: {
            overflow: 'justify'
        }
    },
    plotOptions: {
        pie: {
            allowPointSelect: true,
            cursor: 'pointer',
            dataLabels: {
                enabled: false,
                format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                style: {
                    color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                },
                connectorColor: 'silver'
            },
            showInLegend: true
        }
    },
    credits: {
        enabled: false
    },
    legend: {
        align: 'right',
        verticalAlign: 'top',
        floating: true,
        enabled: true,
        layout: 'vertical'
    }
});

function TimeSeriesChartBuilder(data, chart,isMutipleSeiers) {
    this.data = data;
    this.chart = chart;
    this.chartName = "N/A";

    this.buildChart = function () {
        var containerWidth = ($(document).width() - 350)/12*7;
        this.chart.setSize(containerWidth, null, false);
       
        if (this.data.length == 0) {
            alert("No data found!");
            return;
        }

        var series = this.chart.series;
        while (series.length > 0) {
            series[0].remove(false);
        }

        var chartMain = this.chart;
        if (isMutipleSeiers == true) {
            $.each(data, function(idx, value) {
                var dataPoints = [];
                var chartName = value[0].Name;
                $.each(value, function(index, dataValue) {
                    var year = dataValue.Year;
                    var month = dataValue.Month;
                    var day = dataValue.Day;
                    var dateTimObj = Date.UTC(year, month, day);
                    var time_value_pair = [];
                    time_value_pair.push(dateTimObj);
                    time_value_pair.push(dataValue.ReportedValue);
                    dataPoints.push(time_value_pair);
                });
                chartMain.addSeries({
                    name: chartName,
                    data: dataPoints,
                    tooltip: {
                        valueDecimals: 2
                    }
                });
            });
        } else {
            var dataPoints = [];
            var chartName = data[0].Name;

            $.each(data, function (index, dataValue) {
                var year = dataValue.Year;
                var month = dataValue.Month;
                var day = dataValue.Day;
                var dateTimObj = Date.UTC(year, month, day);
                var time_value_pair = [];
                time_value_pair.push(dateTimObj);
                time_value_pair.push(dataValue.ReportedValue);
                dataPoints.push(time_value_pair);
            });
            chartMain.addSeries({
                name: chartName,
                data: dataPoints,
                tooltip: {
                    valueDecimals: 2
                }
            });
        }

        this.chart.redraw();
    }
};

function CategoryChartBuilder(data, chart, isMutipleSeiers) {
    this.data = data;
    this.chart = chart;
    this.chartName = "N/A";

    this.buildChart = function () {
        var containerWidth = ($(document).width() - 350) / 12 * 2;
        this.chart.setSize(containerWidth, 150, false);

        if (this.data.length == 0) {
            alert("No data found!");
            return;
        }

        var series = this.chart.series;
        while (series.length > 0) {
            series[0].remove(false);
        }

        var chartMain = this.chart;
        if (isMutipleSeiers == true) {
            $.each(data, function (idx, value) {
                var dataPoints = [];
                var chartName = value[0].Name;
                $.each(value, function (index, dataValue) {
                    var year = dataValue.Year;
                    var month = dataValue.Month;
                    var day = dataValue.Day;
                    var dateTimObj = Date.UTC(year, month, day);
                    var time_value_pair = [];
                    time_value_pair.push(dateTimObj);
                    time_value_pair.push(dataValue.ReportedValue);
                    dataPoints.push(time_value_pair);
                });
                chartMain.addSeries({
                    name: chartName,
                    data: dataPoints,
                    tooltip: {
                        valueDecimals: 2
                    }
                });
            });
        } else {
            var dataPoints = [];
            var chartName = data[0].Name;
            $.each(data, function (index, dataValue) {
                var time_value_pair = [];
                time_value_pair.push(dataValue.Label);
                time_value_pair.push(dataValue.Value);
                dataPoints.push(time_value_pair);
            });
            chartMain.addSeries({
                name: chartName,
                data: dataPoints
            });
        }

        this.chart.redraw();
    }
};

$(function () {
    $.ajaxSetup({ cache: false });

    Highcharts.getOptions().colors = Highcharts.map(Highcharts.getOptions().colors, function (color) {
        return {
            radialGradient: {
                cx: 0.5,
                cy: 0.3,
                r: 0.7
            },
            stops: [
                [0, color],
                [1, Highcharts.Color(color).brighten(-0.3).get('rgb')] // darken
            ]
        };
    });

    Highcharts.setOptions({
        lang: {
            loading: '加载中...',
            months: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],
            shortMonths: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],
            weekdays: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
            exportButtonTitle: '导出',
            printButtonTitle: '打印',
            rangeSelectorFrom: '从',
            rangeSelectorTo: '到',
            rangeSelectorZoom: "缩放",
            downloadPNG: '下载PNG格式',
            downloadJPEG: '下载JPEG格式',
            downloadPDF: '下载PDF格式',
            downloadSVG: '下载SVG格式'
        }
    });
   

    $.getJSON('/Product/GetNetValues/?productId=100', function (data) {
        var chartBuilder = new TimeSeriesChartBuilder(data,GLOBAL_NETVALUE_CHART,true);
        chartBuilder.buildChart();
    });

    $.getJSON('/Product/GetPiggyBackDist/?productId=100', function (data) {
        var chartBuilder = new TimeSeriesChartBuilder(data, GLOBAL_PIGGYBACK_DIST_CHART,false);
        chartBuilder.buildChart();
    });

    $.getJSON('/Product/GetReturnDist/?productId=100', function (data) {
        var chartBuilder = new CategoryChartBuilder(data, GLOBAL_RETURN_DIST_CHART, false);
        chartBuilder.buildChart();
    });

    $.getJSON('/Product/GetPnlDist/?productId=100', function (data) {
        var chartBuilder = new CategoryChartBuilder(data, GLOBAL_PNL_DIST_CHART, false);
        chartBuilder.buildChart();
    });
   
});