var Global_NetValue_Chart;

$(function () {
    var realVal = $('#tab_2').width();
    //alert(realVal)

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
   
    $.getJSON('https://www.highcharts.com/samples/data/jsonp.php?filename=aapl-c.json&callback=?', function (data) {
        // Create the chart
        Global_NetValue_Chart = Highcharts.stockChart('netvalueChartContainer', {
            credits: {
                enabled: false
            },
            chart: {
                // Edit chart spacing
                spacingBottom: 5,
                spacingTop: 5,
                spacingLeft: 5,
                spacingRight: 5,
                backgroundColor :null,

                // Explicitly tell the width and height of a chart
                width: realVal+100,
                //width:400,
                height: null
            },

            rangeSelector: {
                selected: 1
            },

            series: [{
                name: '履泰2期',
                data: data,
                tooltip: {
                    valueDecimals: 2
                }
            }]
        });
    });

     new Highcharts.chart('assetDistChartContainer', {
        credits: {
            enabled: false
        },
        chart: {
            spacingBottom: 5,
            spacingTop: 5,
            spacingLeft: 5,
            spacingRight: 25,
            backgroundColor :null,

            // Explicitly tell the width and height of a chart
            width: null,
            height: null,

            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },

        title: {
            text: null
        },
      
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                    style: {
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                    },
                    connectorColor: 'silver'
                }
            }
        },
        series: [{
            name: '金额',
            data: [
                { name: '股票', y: 56.33 },
                {
                    name: '债券',
                    y: 24.03,
                    sliced: true,
                    selected: true
                },
                { name: '现金', y: 10.38 },
                { name: '其他资产', y: 4.77 }
            ]
        }]
   });

    Highcharts.chart('pigbackRateChartContainer', {
        chart: {
            spacingBottom: 5,
            spacingTop: 5,
            spacingLeft: 5,
            spacingRight: 25,
            backgroundColor: null,
            width: realVal + 100,
            type: 'area'
        },

        title: {
            text: null
        },
       
        xAxis: {
            categories: ['Apples', 'Pears', 'Oranges', 'Bananas', 'Grapes', 'Plums', 'Strawberries', 'Raspberries']
        },
        yAxis: {
            title: {
                text: 'Y-Axis'
            },
            labels: {
                formatter: function () {
                    return this.value;
                }
            }
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.series.name + '</b><br/>' +
                    this.x + ': ' + this.y;
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
        series: [{
            name: 'John',
            data: [0, 1, 4, 4, 5, 2, 3, 7]
        }]
    });
   
    //Global_NetValue_Chart.setSize(realVal - 100, realVal - 200, false)
});