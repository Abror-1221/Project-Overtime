//var IdNik = "rsendusr";

$(document).ready(function () {

    var e = $('#employeeTableHistory').DataTable({

        "ajax": {
            "url": "/Accounts/getUserOvertimeHistory/", //"https://localhost:44324/api/overtime/overtimedatahistory/" + IdNik,//"/Accounts/getUserOvertimeHistory/nikcobalog",//"https://localhost:44351/accounts/get",//"Accounts/get/", //ngambil dari form login

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
                    return data.slice(8, 10) + "/" + data.slice(5, 7) + "/" + data.slice(0, 4);
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return row.startTime.slice(8, 10) + "/" + row.startTime.slice(5, 7) + "/" + row.startTime.slice(0, 4) + " - " + row.endTime.slice(8, 10) + "/" + row.endTime.slice(5, 7) + "/" + row.endTime.slice(0, 4);
                }
            },
            { "data":"statusName"},
            {
                "data": null,
                //"wrap": true,educationID 
                //onclick="Delete(' + "'" + row.nik + "'" + ',' + "'" + row.overtimeId + "'" + ')"
                "render": function (data, type, row, item, column) {
                    return '<button id="btnDetailOvertimeEmpHistory" type="button" class="btn btn-secondary" data-bs-toggle="modal" data-bs-placement="top" title="Detail"' +
                        'data-bs-target="#modalDetailEmpHistory"> <i class="fas fa-info-circle"></i> </button > ' //+
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
//$("#employeeTableHistory").on('click', '#btnDetailOvertimeEmpHistory', function () {
//    var data = $("#employeeTableHistory").DataTable().row($(this).parents('tr')).data();
//    console.log(data);
//    //alert("tes aaaaaa dong bro");
//    $('#modalDetailEmpHistory').find(".modal-body").html('<p>Day type               : ' + data.dayTypeName
//        + '</p> <p>Overtime Report        : ' + data.descEmp
//        + '</p> <p value="Unfilled">Validation Description : ' + data.descHead + '</p>');
//});

$("#employeeTableHistory").on('click', '#btnDetailOvertimeEmpHistory', function () {
    var data = $("#employeeTableHistory").DataTable().row($(this).parents('tr')).data();
    //console.log(data);
    //alert("tes aaaaaa dong bro");
    //$('#modalDetailEmployeeOvertime').find(".modal-body").html('<p>Day type               : ' + data.dayTypeName
    //    + '</p> <p>Overtime Report        : ' + data.descEmp
    //    + '</p> <p value="Unfilled">Validation Description : ' + data.descHead + '</p>');
    var detailAct = data.descEmp.split('#');
    console.log(detailAct);
    if (detailAct[0] != "Invalid date-Invalid date" && detailAct[0] != "@Invalid date-Invalid date") {
        $("#overtimeTimeDE1").val(detailAct[0].split('@')[1]);
        $("#overtimeReportDE1").val(detailAct[0].split('@')[0]);
    }
    else {
        $("#overtimeTimeDE1").val("-");
        $("#overtimeReportDE1").val("-");
    }

    if (detailAct[1] != "Invalid date-Invalid date" && detailAct[1] != "@Invalid date-Invalid date") {
        $("#overtimeTimeDE2").val(detailAct[1].split('@')[1]);
        $("#overtimeReportDE2").val(detailAct[1].split('@')[0]);
    }
    else {
        $("#overtimeTimeDE2").val("-");
        $("#overtimeReportDE2").val("-");
    }

    if (detailAct[2] != "Invalid date-Invalid date" && detailAct[2] != "@Invalid date-Invalid date") {
        $("#overtimeTimeDE3").val(detailAct[2].split('@')[1]);
        $("#overtimeReportDE3").val(detailAct[2].split('@')[0]);
    }
    else {
        $("#overtimeTimeDE3").val("-");
        $("#overtimeReportDE3").val("-");
    }

    $("#dayTypeDE").val(data.dayTypeName);
    //$("#overtimeTimeDE").val(data.startTime.slice(11) + " - " + data.endTime.slice(11));
    //$("#overtimeReportDE").val(data.descEmp);
    $("#validationDescDE").val(data.descHead);
});


