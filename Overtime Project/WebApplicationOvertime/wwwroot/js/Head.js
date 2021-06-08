

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
            { "data": "statusName" },
            { "data": "totalReimburse" },
            {
                "data": null, "sortable": false,
                //"wrap": true,educationID 
                //onclick="Delete(' + "'" + row.nik + "'" + ',' + "'" + row.overtimeId + "'" + ')"
                "render": function (data, type, row, item, column) {
<<<<<<< Updated upstream
                    return '<button id="btnDetailOvertimeHead" type="button" class="btn btn-secondary" data-bs-toggle="modal"' +
                        'data-bs-target="#modalDetail"> Detail </button > ' +
                        '<button type="button" id="btnAccHead" class="btn btn-primary"> Approval </button >'
=======
                    return '<button id="btnDetail" type="button" class="btn btn-secondary" data-bs-toggle="modal"' +
                        'data-bs-target="#modalDetail"> Detail </button > ' +
                        '<button type="button" id="btnValidate" class="btn btn-danger"> Validation </button >'
>>>>>>> Stashed changes
                }
            }
            
            
        ]
    });
    h.on('order.dt search.dt', function () {
        h.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
<<<<<<< Updated upstream
});

=======

});

//detail
$("#headTable").on('click', '#btnDetail', function () {
    var data2 = $("#headTable").DataTable().row($(this).parents('tr')).data();
    console.log(data);
    $.ajax({
        url: "https://localhost:44324/api/Account/Profile/" + data2.nik + "/"
        //type: "PUT"
    }).done((result) => {
       
        email = result[0].email
        console.log(email);
    }).fail((error) => {
        alert("error");
    });
    var data = $("#headTable").DataTable().row($(this).parents('tr')).data();
    //alert("tes aaaaaa dong bro");
    $("#staticBackdropLabel").text(data.firstName + " " + data.lastName);
    $('#modalDetail').find(".modal-body").html("<p>NIK : " + data.nik
        + "</p> <p>First Name : " + data.firstName
        + "</p> <p>Last Name  : " + data.lastName
        + "</p> <p>Role       : " + data.role
        + "</p> <p>Email      : " + email + "</p>");
        //+ "</p> <p>Phone      : " + data.phone
        //+ "</p> <p>Birth Date : " + data.birthDate
        //+ "</p> <p>Salary     : " + data.salary
        //+ "</p> <p>Email      : " + data.email + "</p>");
});

//UPDATE
$("#headTable").on('click', '#btnValidate', function () {
    var data = $("#headTable").DataTable().row($(this).parents('tr')).data();
    //alert(data.nik);
    console.log(data);
    //var FormattedDate = (data.date).ToShortDateString();
    $("#nikE").val(data.nik);
    $("#firstNameE").val(data.firstName);
    $("#lastNameE").val(data.lastName);
    $("#overtimeDateE").val(data.date);
    $("#totalReimburseE").val(data.totalReimburse);
    $("#overtimeDescE").val(data.descEmp);
    $("#validDescE").val(data.descHead);
    
   // $("#emailE").val(data.email);

    $("#editModal").modal("show");
    /*$("#editModal").on('click', '#edit', function () {*/

        //var obj1 = new Object();
        //obj1.NIK = $("#nikE").val();
        //obj1.FirstName = $("#firstNameE").val();
        //obj1.LastName = $("#lastNameE").val();
        //obj1.Phone = $("#phoneE").val();
        //obj1.BirthDate = $("#birthDateE").val();
        //obj1.Salary = $("#salaryE").val();
        //obj1.Email = $("#emailE").val();
        //obj1.IsDeleted = 0;

        //$.ajax({
        //    type: "PUT",
        //    url: "https://localhost:44324/API/person",
        //    data: JSON.stringify(obj1),
        //    contentType: "application/json; charset=utf-8",
        //    datatype: "json"

        //}).done((result) => {
        //    alert("Update Success");
        //    $("#myTable").DataTable().ajax.reload();
        //}).fail((error) => {
        //    alert("Update Error");

        //})
    //})
})
>>>>>>> Stashed changes
