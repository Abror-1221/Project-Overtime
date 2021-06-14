

$(document).ready(function () {
    var head = $('#headOvertimeTable').DataTable({
        retrieve: true, //ada potensi error
        paging: false,
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
            { "data": "nik" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return row.firstName + " " + row.lastName;
                }},
            
            { "data": "statusName" },
            {
                "data": null,
                //"wrap": true,educationID 
                //onclick="Delete(' + "'" + row.nik + "'" + ',' + "'" + row.overtimeId + "'" + ')"
                "render": function (data, type, row, item, column) {
                    return '<button id="btnDetailHead" type="button" class="btn btn-secondary" data-bs-toggle="modal" data-bs-placement="top" title="Detail"' +
                        'data-bs-target="#modalDetailHead"> <i class="fas fa-info-circle"></i> </button > ' +
                        '<button type="button" id="btnUpdateHead" class="btn btn-primary"> Approval </button > '
                }
            }
        ]
    });
    //$("#headOvertimeTable").ajax.reload(null, false);
    head.on('order.dt search.dt', function () {
        head.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

});

//Detail
$("#headOvertimeTable").on('click', '#btnDetailHead', function () {
    var data = $("#headOvertimeTable").DataTable().row($(this).parents('tr')).data();
    console.log(data);
    //alert("tes aaaaaa dong bro");
    //$('#modalDetailHead').find(".detailBody").html('<p>Day type            : ' + data.dayTypeName
    //    + '</p> <p>Start Time             : ' + data.startTime.slice(0, 10) + ", Time : " + data.startTime.slice(11)
    //    + '</p> <p>End Time               : ' + data.endTime.slice(0, 10) + ", Time : " + data.endTime.slice(11)
    //    + '</p> <p>Reimburse              : ' + data.totalReimburse
    //    + '</p> <p>Email                  : ' + data.email
    //    + '</p> <p>Phone                  : ' + data.phone
    //    + '</p> <p>Overtime Report        : ' + data.descEmp
    //    + '</p> <p>Validation Description : ' + data.descHead + '</p>');
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

    $("#dayTypeD").val(data.dayTypeName);
    $("#startTimeD").val(data.startTime.slice(8, 10) + "/" + data.startTime.slice(5, 7) + "/" + data.startTime.slice(0, 4) + " - " + data.endTime.slice(8, 10) + "/" + data.endTime.slice(5, 7) + "/" + data.endTime.slice(0, 4));
    //$("#endTimeD").val(data.startTime.slice(11) + " - " + data.endTime.slice(11));
    $("#paymentD").val(formatRupiah(data.totalReimburse.toString(), 'Rp. '));
    $("#emailD").val(data.email);
    $("#phoneD").val(data.phone);
    //$("#overtimeReportD").val(data.descEmp);
    $("#validationD").val(data.descHead);
});

function formatRupiah(angka, prefix) {
    var number_string = angka.replace(/[^,\d]/g, '').toString(),
        split = number_string.split(','),
        sisa = split[0].length % 3,
        rupiah = split[0].substr(0, sisa),
        ribuan = split[0].substr(sisa).match(/\d{3}/gi);

    // tambahkan titik jika yang di input sudah menjadi angka ribuan
    if (ribuan) {
        separator = sisa ? '.' : '';
        rupiah += separator + ribuan.join('.');
    }

    rupiah = split[1] != undefined ? rupiah + ',' + split[1] : rupiah;
    return prefix == undefined ? rupiah : (rupiah ? 'Rp. ' + rupiah : '');
}

//Update v.1

//$("#headOvertimeTable").on('click', '#btnUpdateHead', function () {
//    var data = $("#headOvertimeTable").DataTable().row($(this).parents('tr')).data();
//    $("#descHeadE").val(data.descHead);
//    $("#statusE").val(data.statusId);
//    $("#editModalHead").modal("show");

//    $("#editModalHead").on('submit', function (event) {
//        event.preventDefault();
//        var obj2 = new Object();
//        obj2.Id = data.id;
//        obj2.NIK = data.nik;
//        obj2.Date = data.date;
//        obj2.StartTime = data.startTime;
//        obj2.EndTime = data.endTime;
//        obj2.DescEmp = data.descEmp;
//        obj2.DescHead = $("#descHeadE").val();
//        obj2.TotalReimburse = data.totalReimburse
//        obj2.DayTypeId = data.dayTypeId;
//        obj2.StatusId = $("#statusE").val();

//        $.ajax({
//            url: "https://localhost:44324/API/Overtime",
//            type: "PUT",
//            data: JSON.stringify(obj2),
//            headers: {
//                "content-type": "application/json;charset=UTF-8" // Or add this line
//            }, success: function (data) {
//                $('#validated').val("Submit");
//                $('#editModalHead').modal('hide');
//                $("#headOvertimeTable").DataTable().ajax.reload();
//            }

//        })
//    })
//})

//Update v.2

//function reject() {
//    var obj2 = new Object();
//    obj2.Id = dataHead.id;
//    obj2.NIK = dataHead.nik;
//    obj2.Date = dataHead.date;
//    obj2.StartTime = dataHead.startTime;
//    obj2.EndTime = dataHead.endTime;
//    obj2.DescEmp = dataHead.descEmp;
//    obj2.DescHead = $("#descHeadE").val();
//    obj2.TotalReimburse = dataHead.totalReimburse
//    obj2.DayTypeId = dataHead.dayTypeId;
//    obj2.StatusId = 3;

//    $.ajax({
//        url: "https://localhost:44324/API/validating",
//        type: "PUT",
//        data: JSON.stringify(obj2),
//        headers: {
//            "content-type": "application/json;charset=UTF-8" // Or add this line
//        }
//    }).then((hasil) => {
//        $('#rejected').val("Rejected");
//        $('#editModalHead').modal('hide');
//        $("#headOvertimeTable").DataTable().ajax.reload();
//    })
//}


$("#editFormHead").on('click', '#validated', function () {
    var obj2 = new Object();
    obj2.Id = dataHead.id;
    obj2.NIK = dataHead.nik;
    obj2.Date = dataHead.date;
    obj2.StartTime = dataHead.startTime;
    obj2.EndTime = dataHead.endTime;
    obj2.DescEmp = dataHead.descEmp;
    obj2.DescHead = $("#descHeadE").val();
    obj2.TotalReimburse = dataHead.totalReimburse
    obj2.DayTypeId = dataHead.dayTypeId;
    obj2.StatusId = 2;

    $.ajax({
        url: "https://localhost:44324/API/Overtime/validating",
        type: "PUT",
        data: JSON.stringify(obj2),
        headers: {
            "content-type": "application/json;charset=UTF-8" // Or add this line
        
    }, success: function (data) {
           
       
        $('#editModalHead').modal('hide');
        $("#headOvertimeTable").DataTable().ajax.reload();
    }
    })
});

$("#editFormHead").on('click', '#rejected', function () {
    var obj2 = new Object();
    obj2.Id = dataHead.id;
    obj2.NIK = dataHead.nik;
    obj2.Date = dataHead.date;
    obj2.StartTime = dataHead.startTime;
    obj2.EndTime = dataHead.endTime;
    obj2.DescEmp = dataHead.descEmp;
    obj2.DescHead = $("#descHeadE").val();
    obj2.TotalReimburse = dataHead.totalReimburse
    obj2.DayTypeId = dataHead.dayTypeId;
    obj2.StatusId = 3;

    $.ajax({
        url: "https://localhost:44324/API/Overtime/validating",
        type: "PUT",
        data: JSON.stringify(obj2),
        headers: {
            "content-type": "application/json;charset=UTF-8" // Or add this line
        }, success: function (data) {
           
        
            $('#editModalHead').modal('hide');
            $("#headOvertimeTable").DataTable().ajax.reload();
        }
    })
    
});

//function valid() {
//    var obj2 = new Object();
//    obj2.Id = dataHead.id;
//    obj2.NIK = dataHead.nik;
//    obj2.Date = dataHead.date;
//    obj2.StartTime = dataHead.startTime;
//    obj2.EndTime = dataHead.endTime;
//    obj2.DescEmp = dataHead.descEmp;
//    obj2.DescHead = $("#descHeadE").val();
//    obj2.TotalReimburse = dataHead.totalReimburse
//    obj2.DayTypeId = dataHead.dayTypeId;
//    obj2.StatusId = 2;

//    $.ajax({
//        url: "https://localhost:44324/API/Overtime/validating",
//        type: "PUT",
//        data: JSON.stringify(obj2),
//        headers: {
//            "content-type": "application/json;charset=UTF-8" // Or add this line
//        }
//    }).then((hasil) => {
//        $('#validated').val("Validated");
//        $('#editModalHead').modal('hide');
//        $("#headOvertimeTable").DataTable().ajax.reload();
//    })
//}

$("#headOvertimeTable").on('click', '#btnUpdateHead', function () {
    dataHead = $("#headOvertimeTable").DataTable().row($(this).parents('tr')).data();
    $("#descHeadE").val(dataHead.descHead);
    $("#statusE").val(dataHead.statusId);
    $("#editModalHead").modal("show");
})





