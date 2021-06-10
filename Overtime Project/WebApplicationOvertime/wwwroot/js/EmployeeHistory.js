var IdNik = 2;

$(document).ready(function () {

    var e = $('#employeeTableHistory').DataTable({

        "ajax": {
            "url": "https://localhost:44324/api/overtime/overtimedatahistory/" + IdNik,//"/Accounts/getUserOvertimeHistory/nikcobalog",//"https://localhost:44351/accounts/get",//"Accounts/get/", //ngambil dari form login

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
        "order": [[1, 'desc']], //mengihlangkan tanda arrow sorting di kolomn 0, jadi mulai dari kolm 1
        "columns": [
            {
                "data": null, "sortable": false,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "date", render: function (data, type, row) {
                    return data.slice(0, 10);
                }
            },
            {
                "data": "startTime", render: function (data, type, row) {
                    return data.slice(0, 10) + ", Time : " + data.slice(11);
                } },
            {
                "data": "endTime", render: function (data, type, row) {
                    return data.slice(0, 10) + ", Time : " + data.slice(11);
                } ,
            },
            { "data":"statusName"},
            {
                "data": null,
                //"wrap": true,educationID 
                //onclick="Delete(' + "'" + row.nik + "'" + ',' + "'" + row.overtimeId + "'" + ')"
                "render": function (data, type, row, item, column) {
                    return '<button id="btnDetailOvertimeEmpHistory" type="button" class="btn btn-secondary" data-bs-toggle="modal"' +
                        'data-bs-target="#modalDetailEmpHistory"> Detail </button > ' //+
                        //'<button type="button" id="btnUpdateEmployee" class="btn btn-primary"> Update </button > '
                }
            }
        ]
    });
    e.on('order.dt search.dt', function () {
        e.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
});

//Request (Insert)

//Detail
$("#employeeTableHistory").on('click', '#btnDetailOvertimeEmpHistory', function () {
    var data = $("#employeeTableHistory").DataTable().row($(this).parents('tr')).data();
    console.log(data);
    //alert("tes aaaaaa dong bro");
    $('#modalDetailEmpHistory').find(".modal-body").html('<p>Day type               : ' + data.dayTypeName
        + '</p> <p>Overtime Report        : ' + data.descEmp
        + '</p> <p value="Unfilled">Validation Description : ' + data.descHead + '</p>');
});


