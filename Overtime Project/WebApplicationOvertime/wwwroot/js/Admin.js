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
            "url": "https://localhost:44324/API/account/userdata",

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
                    return '<button type="button" class="btn btn-secondary" data-toggle="modal"' +
                        'data-id="' + row.nik + '" data-target="#exampleModal"> Detail </button > ' +
                        '<button type="button" id="btnDel" class="btn btn-danger"> Delete </button > ' +
                        '<button type="button" class="btn btn-primary" onclick="Update()"> Update </button > '
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

    var data = $("#myTable").DataTable().row($(this).parents('tr')).data();
   // console.log(data.IsDeleted);
        var obj1 = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
        obj1.NIK = data.nik;
        obj1.FirstName = data.firstname;
        obj1.LastName = data.lastname;
        obj1.Phone = data.phone;
        obj1.BirthDate = data.birthdate;
        obj1.Salary = data.salary;
        obj1.Email = data.email;
        obj1.IsDeleted = 1;
        
        $.ajax({
            type: "PUT",
            url: "https://localhost:44324/API/person",
            data: JSON.stringify(obj1),
            contentType: "application/json; charset=utf-8",
            datatype: "json"
            //beforeSend: function () {
            //    $("#edit").val("Saving...");
            //},
            //success: function (data) {
            //    //$('#insert').val("Insert");
            //    //t.ajax.reload();
            //    alert("Success submit");
            //}
        }).done((result) => {
            $("#myTable").DataTable().ajax.reload();
            //$("myTable").DataTable().ajax.reload();
        }).fail((error) => {
            alert("Delete Error");
        })
   
})