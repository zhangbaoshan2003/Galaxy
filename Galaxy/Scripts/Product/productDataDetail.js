var Global_oTable;
$('#btnTabOne').click(function () {
   
    setTimeout(function () {
        //Global_oTable.draw();
        var gridDataSource = $('#productHoldingGrid').data('kendoGrid').dataSource;
        gridDataSource.read();
    }, 1);
});

function format(d) {
    // `d` is the original data object for the row
    return '' +
    '<table cellpadding="0" cellspacing="0" border="1" style="margin-left:20px;">' +
		'<tr>' +
			'<td>停牌前收盘价:</td>' +
			'<td>'+d[13]+'</td>' +

            '<td>停牌前行业指数:</td>' +
			'<td>'+[14]+' </td>' +

            '<td>最新行业指数:</td>' +
			'<td> '+d[15]+' </td>' +

            '<td>指数涨跌幅:</td>' +
			'<td> '+d[16]+' </td>' +

          '<td>股票价格预估:</td>' +
			'<td> '+d[17]+' </td>' +

        '<td>股票市值预估:</td>' +
			'<td> '+d[18]+' </td>' + 

        '<td>市值减少额:</td>' +
			'<td> '+d[19]+' </td>' +

		'</tr>' +
	'</table>';
}

$(function () {
    //initialize data table 
    
    //Global_oTable = $('#prodcutDetailTable').DataTable({
    //    "paging": true,
    //    "lengthChange": true,
    //    "searching": true,
    //    "ordering": true,
    //    "info": false,
    //    "autoWidth": false,
    //    "scrollX": true,
    //    "sScrollX": "100%",
    //    "sScrollXInner": "110%",
    //    "columnDefs": [
    //        { "width": "20px", "targets": 0 }
    //    ],
    //    "createdRow": function (row, data, index) {
    //        if (data[4].replace(/[\$,]/g, '') * 1 > 30) {
    //            $('td', row).eq(0).removeClass('details-control');
    //        } else {
    //            $('td', row).eq(1).addClass('bg-red-gradient')
    //        }
    //    }
    //});

    //$('#prodcutDetailTable tbody').on('click', 'td.details-control', function () {
      
    //    var tr = $(this).closest('tr');
    //    var row = Global_oTable.row(tr);
    //    //alert(row);
    //    if (row.child.isShown()) {
    //        // This row is already open - close it
    //        row.child.hide();
    //        tr.removeClass('shown');
    //    }
    //    else {
    //        // Open this row
    //        row.child(format(row.data())).show();
    //        tr.addClass('shown');
    //    }
    //});
});