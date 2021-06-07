

$(document).ready(function () {

    var h = $('#headTable').DataTable({

        "ajax": {
            "url": "https://localhost:44324/API/Overtime/OvertimeDataAll",

            "datatype": "json",
            "dataSrc": "",
            //    "columnDefs": [{
            //        "targets": [0],
            //        "orderable": false
            //    }]
            //},
        },
        "columnDefs": [
            //    {
            //        "targets": -1,
            //        render: function (data, type, row, meta) {
            //            return '<input type="button" class="name" id=n-"' + row.nik + '" value="salary"/>';
            //        },
            //    //"data": null,
            //    //"defaultContent": "<button>Click!</button>"
            //},
            {
                "targets": 0,
                "searchable": false,
                "orderable": false
            }

        ],
        "order": [[1, 'asc']], //mengihlangkan tanda arrow sorting di kolomn 0, jadi mulai dari kolm 1
        "columns": [
            {
                "data": null, "sortable": false,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "nik" },
            { "data": "firstName" },
            { "data": "lastName" },
            {
                "data": "date", render: function (data, type, row) {
                    return data.slice(0,10);
                }
            },
            {
                "data": null,
                //"wrap": true,educationID 
                //onclick="Delete(' + "'" + row.nik + "'" + ',' + "'" + row.overtimeId + "'" + ')"
                "render": function (data, type, row, item, column) {
                    return '<button id="btnDetailOvertimeHead" type="button" class="btn btn-secondary" data-bs-toggle="modal"' +
                        'data-bs-target="#modalDetail"> Detail </button > ' +
                        '<button type="button" id="btnAccHead" class="btn btn-primary"> Approval </button >'
                }
            },
            { "data": "statusName"}
            
        ]
    });
    h.on('order.dt search.dt', function () {
        h.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
});

