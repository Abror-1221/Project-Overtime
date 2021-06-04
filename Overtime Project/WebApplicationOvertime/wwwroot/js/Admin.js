// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//validation bootstrap
(function () {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }

                form.classList.add('was-validated')
            }, false)
        })
})()

$(document).ready(function () {

    var t = $('#myTable').DataTable({

        "ajax": {
            "url": "/Accounts/GetUserData",

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
                "data": "phone", render: function (data, type, row) {
                    return '+62' + data.slice(1);
                }
            },
            {
                "data": null,
                //"wrap": true,educationID 
                //onclick="Delete(' + "'" + row.nik + "'" + ',' + "'" + row.overtimeId + "'" + ')"
                "render": function (data, type, row, item, column) {
                    return '<button id="btnDetail" type="button" class="btn btn-secondary" data-bs-toggle="modal"' +
                        'data-bs-target="#modalDetail"> Detail </button > ' +
                        '<button type="button" id="btnDel" class="btn btn-danger"> Delete </button > ' +
                        '<button type="button" id="btnEdit" class="btn btn-primary"> Update </button > '
                }
            }
        ]
    });
    t.on('order.dt search.dt', function () {
        t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    //$("#myTable tbody").on('click', '#btnDel', function () {
    //    alert("hello");

    //  })
});

//$("#myTable").on('click', '#btnDel', function () {
//    alert("hello");

//})

function Update() {

    alert("done");

}

$('#insert_form').on("submit", function (event) {
    event.preventDefault();

    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    obj.NIK = $("#nik").val();
    obj.FirstName = $("#firstName").val();
    obj.LastName = $("#lastName").val();
    obj.Phone = $("#phone").val();
    obj.BirthDate = $("#bdate").val();
    obj.Salary = parseInt($("#salary").val(), 10);
    obj.Email = $("#email").val();
    obj.Password = $("#password").val();

    $.ajax({
        url: "https://localhost:44324/API/account/register",
        type: "POST",
        data: JSON.stringify(obj),
        headers: {
            "content-type": "application/json;charset=UTF-8" // Or add this line
      }, success: function (data) {
            alert("done");
            $('#insert_form')[0].reset();
            $('#insert').val("Insert");
            $('#staticBackdrop').modal('hide');
            $("#myTable").DataTable().ajax.reload();
        }
    })
});



$("#myTable").on('click', '#btnDel', function () {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            var data = $("#myTable").DataTable().row($(this).parents('tr')).data();
            console.log(data.nik);
            console.log(data.firstName);
            var obj1 = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
            obj1.NIK = data.nik;
            obj1.FirstName = data.firstName;
            obj1.LastName = data.lastName;
            obj1.Phone = data.phone;
            obj1.BirthDate = data.birthDate;
            obj1.Salary = data.salary;
            obj1.Email = data.email;
            obj1.IsDeleted = 1;

            $.ajax({
                type: "PUT",
                url: "https://localhost:44324/API/person",
                data: JSON.stringify(obj1),
                contentType: "application/json; charset=utf-8",
                datatype: "json"


            }).done((result) => {
                $("#myTable").DataTable().ajax.reload();
            })
            Swal.fire(
                'Deleted!',
                'Your file has been deleted.',
                'success'
            )
        }
    })
    //var data = $("#myTable").DataTable().row($(this).parents('tr')).data();
    //console.log(data.nik);
    //console.log(data.firstName);
    //    var obj1 = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //    obj1.NIK = data.nik;
    //    obj1.FirstName = data.firstName;
    //    obj1.LastName = data.lastName;
    //    obj1.Phone = data.phone;
    //    obj1.BirthDate = data.birthDate;
    //    obj1.Salary = data.salary;
    //    obj1.Email = data.email;
    //    obj1.IsDeleted = 1;
        
    //    $.ajax({
    //        type: "PUT",
    //        url: "https://localhost:44324/API/person",
    //        data: JSON.stringify(obj1),
    //        contentType: "application/json; charset=utf-8",
    //        datatype: "json"
          
    //    }).done((result) => {
    //        $("#myTable").DataTable().ajax.reload();
    //    }).fail((error) => {
    //        alert("Delete Error");
    //    })
   
})

//detail
$("#myTable").on('click','#btnDetail', function () {
    var data = $("#myTable").DataTable().row($(this).parents('tr')).data();
    console.log(data);
    //alert("tes aaaaaa dong bro");
    $("#staticBackdropLabel").text(data.firstName + " " + data.lastName);
    $('#modalDetail').find(".modal-body").html("<p>NIK : " + data.nik
        + "</p> <p>First Name : " + data.firstName
        + "</p> <p>Last Name  : " + data.lastName
        + "</p> <p>Role       : " + data.role
        + "</p> <p>Phone      : " + data.phone
        + "</p> <p>Birth Date : " + data.birthDate
        + "</p> <p>Salary     : " + data.salary
        + "</p> <p>Email      : " + data.email + "</p>");
});

//UPDATE
$("#myTable").on('click', '#btnEdit', function () {
    var data = $("#myTable").DataTable().row($(this).parents('tr')).data();
    //alert(data.nik);
    $("#nikE").val(data.nik);
    $("#firstNameE").val(data.firstName);
    $("#lastNameE").val(data.lastName);
    $("#phoneE").val(data.phone);
    $("#birthDateE").val(data.birthDate);
    $("#salaryE").val(data.salary);
    $("#emailE").val(data.email);
   
    $("#editModal").modal("show");
    $("#editModal").on('click', '#edit', function () {

        var obj1 = new Object(); 
        obj1.NIK = $("#nikE").val();
        obj1.FirstName = $("#firstNameE").val();
        obj1.LastName = $("#lastNameE").val();
        obj1.Phone = $("#phoneE").val();
        obj1.BirthDate = $("#birthDateE").val();
        obj1.Salary = $("#salaryE").val();
        obj1.Email = $("#emailE").val();
        obj1.IsDeleted = 0;
        
        $.ajax({
            type: "PUT",
            url: "https://localhost:44324/API/person",
            data: JSON.stringify(obj1),
            contentType: "application/json; charset=utf-8",
            datatype: "json"
            
        }).done((result) => {
                alert("Update Success");
                $("#myTable").DataTable().ajax.reload();
            }).fail((error) => {
                alert("Update Error");
           
        })
    })
})