$('#netValueChartCtrlBtn').click(function () {
    $.ajax({
        type: "GET",
        url: "/Product/GetNetValues/?productId=100",
        dataType: "JSON",
        async: false,
        sucess: function (data) {
            alert('OK');
            //alert(textStatus);
            //var chartBuilder = new netvalueChartBuilder(data, Global_NetValue_Chart);
            //chartBuilder.processData();
        },
        cache: true,
        error: function (jqXHR, textStatus, errorThrown) {
            alert('Error!' + textStatus);
        }
    });
    $.getJSON('/Product/GetNetValues/?productId=100', function (data) {
        alert('OK');
        //var chartBuilder = new netvalueChartBuilder(data, Global_NetValue_Chart);
        //chartBuilder.processData();
    });
});

$(window).resize(function () {
    //var realVal = $('#netValueDiv').width();
    //Global_NetValue_Chart.setSize(realVal + 100, realVal + 200, false)
});

jQuery(document).ready(function ($) {
   
});