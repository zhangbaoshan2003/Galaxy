$(function () {
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
   
    $.getJSON('https://www.highcharts.com/samples/data/jsonp.php?filename=aapl-c.json&callback=?', function (data) {
        // Create the chart
        Highcharts.stockChart('netvalueChartContainer', {
            credits: {
                enabled: false
            },
            chart: {
                // Edit chart spacing
                spacingBottom: 5,
                spacingTop: 5,
                spacingLeft: 5,
                spacingRight: 25,
                backgroundColor :null,

                // Explicitly tell the width and height of a chart
                width: null,
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

    Highcharts.chart('assetDistChartContainer', {
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

});