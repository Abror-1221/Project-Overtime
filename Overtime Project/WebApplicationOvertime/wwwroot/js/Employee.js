var IdNik = "rsendusr";//asasaff

$(document).ready(function () {

    var e = $('#employeeTable').DataTable({

        "ajax": {
            "url": "/Accounts/getUserOvertime/" + IdNik,//"https://localhost:44351/accounts/get",//"Accounts/get/", //ngambil dari form login

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
                }
            },
            { "data": "statusName" },
            {
                "data": null,
                //"wrap": true,educationID 
                //onclick="Delete(' + "'" + row.nik + "'" + ',' + "'" + row.overtimeId + "'" + ')"
                "render": function (data, type, row, item, column) {
                    return '<button id="btnDetailOvertimeEmployee" type="button" class="btn btn-secondary" data-bs-toggle="modal"' +
                        'data-bs-target="#modalDetailEmployeeOvertime"> Detail </button > ' //+
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
$('#request_form').on("submit", function (event) {
    event.preventDefault();

    var dateOvertime = $("#demo-calendar").val();
    var timeOvertime = $("#demo-time").val();
    var tes = timeOvertime.split('-')[0];
    var tes2 = timeOvertime.split('-')[1];
    //if(tes2.lenght == )
    var dateRequest = new Date();
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //obj.Date = $("#date").val();
    //obj.Date = dateRequest.getMonth() + "/" + dateRequest.getDate() + "/" + dateRequest.getFullYear();
    //obj.StartTime = $("#startTime").val();
    //obj.EndTime = $("#endTime").val();
    obj.StartTime = dateOvertime.slice(0, 10) + " " + tes;
    //obj.StartTime = dateOvertime.slice(0, 10) + " " + timeOvertime.slice(0, 8);
   // obj.EndTime = dateOvertime.slice(13) + " " + timeOvertime.slice(11);
    obj.EndTime = dateOvertime.slice(13) + tes2;
    obj.DescEmp = $("#descEmp").val();
    console.log(obj.StartTime);
    console.log(obj.EndTime);
    //console.log(tes);
    //console.log(tes2);
    //obj.DayTypeId = $("#day").val();
    obj.StatusId = 1;
    //$.ajax({
    //    url: "https://localhost:44324/API/Overtime/ReqOvertime/" + IdNik,
    //    type: "POST",
    //    data: JSON.stringify(obj),
    //    headers: {
    //        "content-type": "application/json;charset=UTF-8" // Or add this line
    //    }, success: function (data) {
    //        alert("done");
    //        $('#request_form')[0].reset();
    //        $('#request').val("Request");
    //        $('#staticBackdropEmployeeOvertime').modal('hide');
    //        $("#employeeTable").DataTable().ajax.reload();
    //    }
    //})
});

mobiscroll.setOptions({
    theme: 'ios',
    themeVariant: 'light'
});

mobiscroll.datepicker('#demo-calendar', {
    controls: ['calendar'],
    select: 'range',
    display: 'anchored'
});
mobiscroll.datepicker('#demo-time', {
    controls: ['time'],
    select: 'range',
    display: 'anchored'
});
//Detail
$("#employeeTable").on('click', '#btnDetailOvertimeEmployee', function () {
    var data = $("#employeeTable").DataTable().row($(this).parents('tr')).data();
    console.log(data);
    //alert("tes aaaaaa dong bro");
    //$('#modalDetailEmployeeOvertime').find(".modal-body").html('<p>Day type               : ' + data.dayTypeName
    //    + '</p> <p>Overtime Report        : ' + data.descEmp
    //    + '</p> <p value="Unfilled">Validation Description : ' + data.descHead + '</p>');

    $("#dayTypeDE").val(data.dayTypeName);
    $("#overtimeReportDE").val(data.descEmp);
    $("#validationDescDE").val(data.descHead);
});

//Update Overtime
$("#employeeTable").on('click', '#btnUpdateEmployee', function () {
    var data = $("#employeeTable").DataTable().row($(this).parents('tr')).data();
    //alert(data.nik);
    $("#dateE").val(data.date.slice(0, 10));
    $("#startTimeE").val(data.startTime);
    $("#endTimeE").val(data.endTime);
    $("#descEmpE").val(data.descEmp);
    $("#dayE").val(data.dayTypeId);
    $("#editModalEmployeeOvertime").modal("show");

    $("#editModalEmployeeOvertime").on('submit', function (event) {
        event.preventDefault();
        var obj1 = new Object();
        obj1.Id = data.id;
        obj1.NIK = data.nik;
        obj1.Date = $("#dateE").val();
        obj1.StartTime = $("#startTimeE").val();
        obj1.EndTime = $("#endTimeE").val();
        obj1.DescEmp = $("#descEmpE").val();
        obj1.DescHead = data.descHead;
        obj1.TotalReimburse = data.totalReimburse
        obj1.DayTypeId = $("#dayE").val();
        obj1.StatusId = data.statusId;

        $.ajax({
            url: "https://localhost:44324/API/Overtime",
            type: "PUT",
            data: JSON.stringify(obj1),
            headers: {
                "content-type": "application/json;charset=UTF-8" // Or add this line
            }, success: function (data) {
                $('#editEmployeeOvertime').val("Save Changes");
                $('#editModalEmployeeOvertime').modal('hide');
                $("#employeeTable").DataTable().ajax.reload();
            }

        })
    })
})

