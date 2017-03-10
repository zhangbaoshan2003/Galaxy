var Global_oTable;
$('#btnTabOne').click(function () {
    setTimeout(function () { Global_oTable.draw(); }, 1);
});

function format(d) {
    // `d` is the original data object for the row
    return '<table cellpadding="5" cellspacing="0" border="2" style="margin-left:50px;">' +
		'<tr>' +
			'<td>停牌前收盘价:</td>' +
			'<td>33.99</td>' +

            '<td>最近交易日:</td>' +
			'<td> 2013-1-1 </td>' +

            '<td>停牌前收盘价:</td>' +
			'<td> 40 </td>' +

            '<td>停牌前行业指数:</td>' +
			'<td> 200 </td>' +

          '<td>最新行业指数:</td>' +
			'<td> 200 </td>' +

        '<td>指数涨跌幅:</td>' +
			'<td> 200 </td>' +

        '<td>股票价格预估:</td>' +
			'<td> 200 </td>' +

        '<td>股票市值预估:</td>' +
			'<td> 200 </td>' +

         '<td>市值减少额:</td>' +
			'<td> 200 </td>' +

		'</tr>' +
	'</table>';
}

$(function () {
    //initialize data table 
    
    Global_oTable = $('#prodcutDetailTable').DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": false,
        "autoWidth": false,
        "scrollX": true,
        "sScrollX": "100%",
        "sScrollXInner": "110%",
        "columnDefs": [
            { "width": "20px", "targets": 0 }
        ]
    });
    
    //$('#prodcutDetailTable tbody td img').click(function () {
    //    var nTr = this.parentNode.parentNode;
    //    if (this.src.match('details_close')) {
    //        /* This row is already open - close it */
    //        this.src = "/Styles/resources/details_open.png";
    //    }
    //    else {
    //        /* Open this row */
    //        this.src = "/Styles/resources/details_close.png";
    //    }
    //});

    $('#prodcutDetailTable tbody').on('click', 'td.details-control', function () {
      
        var tr = $(this).closest('tr');
        var row = Global_oTable.row(tr);
        //alert(row);
        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
        }
        else {
            // Open this row
            row.child(format(row.data())).show();
            tr.addClass('shown');
        }
    });
});