﻿////$(document).ready(function () {

////    var getNik = $.ajax({
////        url: "https://localhost:44351/accounts/getNik",
////        async: false,
////    }).done((result) => {
////        return result;
////    }).fail((error) => {

////    });
////    userID = getNik.responseText;
////    console.log(userID);

////    $.ajax({
////        url: "https://localhost:44324/API/Account/Profile/" + userID
////    }).done((result) => {
////        userId = `<label class="form-control-label" for="input-id">NIK</label>
////                  <input id="input-userId" class="form-control form-control-alternative" placeholder="Your Id" value="${result[0].nik}" type="text" readonly>`
////        fname = `<label class="form-control-label" for="input-first-name">First name</label>
////             <input type="text" id="input-first-name" class="form-control form-control-alternative" placeholder="First name" value="${result[0].firstName}" readonly>`
////        lname = `<label class="form-control-label" for="input-last-name">Last name</label>
////             <input type="text" id="input-last-name" class="form-control form-control-alternative" placeholder="Last name" value="${result[0].lastName}" readonly>`
////        birthDate = `<label class="form-control-label" for="input-birthdate">Birth Date</label>
////                 <input type="date" id="input-birthdate" class="form-control form-control-alternative" placeholder="Your Birth Date" value="${result[0].birthDate.slice(0, 10)}" readonly>`
////        gender = `<label class="form-control-label" for="input-gender">Gender</label>
////              <input type="text" id="input-gender" class="form-control form-control-alternative" placeholder="Your Gender" value="${result[0].gender}" readonly>`
////        email = `<label class="form-control-label" for="input-email">Email Address</label>
////             <input id="input-email" class="form-control form-control-alternative" placeholder="youremail@example.com" type="text" value="${result[0].email}" readonly>`
////        phone = `<label class="form-control-label" for="input-phone">Phone</label>
////             <input id="input-phone" class="form-control form-control-alternative" placeholder="+62xxx-xxxx-xxxx" type="text" value="${result[0].phone}" readonly>`
////        address = `<label class="form-control-label" for="input-address">Role</label>
////              <input id="input-role" class="form-control form-control-alternative" placeholder="Role" value="${result[0].role}" readonly>`
////        department = `<label class="form-control-label" for="input-department">Salary</label>
////                  <input id="input-salary" class="form-control form-control-alternative" placeholder="Salary" value="${formatRupiah(result[0].salary.toString(), 'Rp. ')}" type="text" readonly>`
////        $(".account-id").html(userId);
////        $(".fname").html(fname);
////        $(".lname").html(lname);
////        $(".bDate").html(birthDate);
////        $(".genderUser").html(gender);
////        $(".emailAddrs").html(email);
////        $(".contactPhone").html(phone);
////        $(".addrs").html(address);
////        $(".departmentName").html(department);
////    }).fail((error) => {
////        alert("error");
////    });
////});

////function EditProfile() {
////    $.ajax({
////        url: "https://localhost:44324/API/Account/Profile/" + userID
////    }).done((result) => {
////        fname = `<label class="form-control-label" for="input-first-name">First name</label>
////             <input type="text" id="input-first-name" class="form-control form-control-alternative" placeholder="First name" value="${result[0].firstName}">`
////        lname = `<label class="form-control-label" for="input-last-name">Last name</label>
////             <input type="text" id="input-last-name" class="form-control form-control-alternative" placeholder="Last name" value="${result[0].lastName}">`
////        birthDate = `<label class="form-control-label" for="input-birthdate">Birth Date</label>
////                 <input type="date" id="input-birthdate" class="form-control form-control-alternative" placeholder="Your Birth Date" value="${result[0].birthDate.slice(0, 10)}">`
////        gender = `<label class="form-control-label" for="input-gender">Gender</label>
////                  <select name="gender" id="input-gender" class="form-control form-control-alternative">
////                    <option value="${result[0].gender}" hidden></option>
////                    <option value="Male">Male</option>
////                    <option value="Female">Female</option>
////                  </select>`
////        email = `<label class="form-control-label" for="input-email">Email Address</label>
////             <input id="input-email" class="form-control form-control-alternative" placeholder="youremail@example.com" type="text" value="${result[0].email}">`
////        phone = `<label class="form-control-label" for="input-phone">Phone</label>
////             <input id="input-phone" class="form-control form-control-alternative" placeholder="+62xxx-xxxx-xxxx" type="text" value="${result[0].phone}">`
////        address = `<label class="form-control-label" for="input-role">Role</label>
////              <input id="input-address" class="form-control form-control-alternative" placeholder="Home Address" value="${result[0].role}">`
////        department = `<label class="form-control-label" for="input-department">Department Name</label>
////                      <input id="input-salary" class="form-control form-control-alternative" placeholder="Salary" value="${result[0].salary}" type="number">`
////        $(".fname").html(fname);
////        $(".lname").html(lname);
////        $(".bDate").html(birthDate);
////        $(".genderUser").html(gender);
////        $(".emailAddrs").html(email);
////        $(".contactPhone").html(phone);
////        $(".addrs").html(address);
////        $(".departmentName").html(department);
////        $(".btnFinishEdit").html(buttonFinishEdit);
////    }).fail((error) => {
////        alert("error");
////    });
////}

////function SubmitEdit() {
////    var editProfile = new Object();
////    editProfile.Id = $('#input-userId').val();
////    editProfile.FirstName = $('#input-first-name').val();
////    editProfile.LastName = $('#input-last-name').val();
////    editProfile.GenderId = $('#input-gender').val();
////    editProfile.BirthDate = $('#input-birthdate').val();
////    editProfile.Address = $('#input-address').val();
////    editProfile.Contact = $('#input-phone').val();
////    editProfile.Email = $('#input-email').val();
////    editProfile.DepartmentId = $('#input-department').val();
////    $.ajax({
////        type: "PUT",
////        url: "https://localhost:44324/API/Users/",
////        data: JSON.stringify(editProfile),
////        contentType: "application/json; charset=utf-8",
////        datatype: "json"
////    }).done((success) => {
////        Swal.fire(
////            'Good job!',
////            'Data successfully updated !',
////            'success'
////        );
////        $.ajax({
////            url: "https://localhost:44324/API/Accounts/Profile/" + userID
////        }).done((result) => {
////            fname = `<label class="form-control-label" for="input-first-name">First name</label>
////             <input type="text" id="input-first-name" class="form-control form-control-alternative" placeholder="First name" value="${result[0].firstName}" readonly>`
////            lname = `<label class="form-control-label" for="input-last-name">Last name</label>
////             <input type="text" id="input-last-name" class="form-control form-control-alternative" placeholder="Last name" value="${result[0].lastName}" readonly>`
////            birthDate = `<label class="form-control-label" for="input-birthdate">Birth Date</label>
////                 <input type="date" id="input-birthdate" class="form-control form-control-alternative" placeholder="Your Birth Date" value="${result[0].birthDate.slice(0, 10)}" readonly>`
////            gender = `<label class="form-control-label" for="input-gender">Gender</label>
////                  <input type="text" id="input-gender" class="form-control form-control-alternative" placeholder="Your Gender" value="${result[0].gender}" readonly>`
////            email = `<label class="form-control-label" for="input-email">Email Address</label>
////             <input id="input-email" class="form-control form-control-alternative" placeholder="youremail@example.com" type="text" value="${result[0].email}" readonly>`
////            phone = `<label class="form-control-label" for="input-phone">Phone</label>
////             <input id="input-phone" class="form-control form-control-alternative" placeholder="+62xxx-xxxx-xxxx" type="text" value="${result[0].contact}" readonly>`
////            address = `<label class="form-control-label" for="input-address">Address</label>
////              <input id="input-address" class="form-control form-control-alternative" placeholder="Home Address" value="${result[0].address}" readonly>`
////            department = `<label class="form-control-label" for="input-department">Department Name</label>
////                      <input id="input-department" class="form-control form-control-alternative" placeholder="Your Department" value="${result[0].department}" type="text" readonly>`
////            buttonFinishEdit = ``
////            $(".fname").html(fname);
////            $(".lname").html(lname);
////            $(".bDate").html(birthDate);
////            $(".genderUser").html(gender);
////            $(".emailAddrs").html(email);
////            $(".contactPhone").html(phone);
////            $(".addrs").html(address);
////            $(".departmentName").html(department);
////            $(".btnFinishEdit").html(buttonFinishEdit);
////        }).fail((error) => {
////            Swal.fire(
////                'Error!',
////                'Data failed updated !',
////                'error'
////            );
////        });
////    }).fail((notSuccess) => {
////        Swal.fire(
////            'Error!',
////            'Data failed updated !',
////            'error'
////        );
////    });
////}
$(document).ready(function () {

    var getNik = $.ajax({
        url: "https://localhost:44351/accounts/getNik",
        async: false,
    }).done((result) => {
        return result;
    }).fail((error) => {

    });
    userID = getNik.responseText;
    console.log(userID);

    $.ajax({
        url: "https://localhost:44324/API/Account/Profile/" + userID
    }).done((result) => {
        userId = `<label class="form-control-label" for="input-id">NIK</label>
                  <input id="input-userId" class="form-control form-control-alternative" placeholder="Your Id" value="${result[0].nik}" type="text" readonly>`
        fname = `<label class="form-control-label" for="input-first-name">First name</label>
             <input type="text" id="input-first-name" class="form-control form-control-alternative" placeholder="First name" value="${result[0].firstName}" readonly>`
        lname = `<label class="form-control-label" for="input-last-name">Last name</label>
             <input type="text" id="input-last-name" class="form-control form-control-alternative" placeholder="Last name" value="${result[0].lastName}" readonly>`
        birthDate = `<label class="form-control-label" for="input-birthdate">Birth Date</label>
                 <input type="date" id="input-birthdate" class="form-control form-control-alternative" placeholder="Your Birth Date" value="${result[0].birthDate.slice(0, 10)}" readonly>`
        gender = `<label class="form-control-label" for="input-gender">Gender</label>
              <input type="text" id="input-gender" class="form-control form-control-alternative" placeholder="Your Gender" value="${result[0].gender}" readonly>`
        email = `<label class="form-control-label" for="input-email">Email Address</label>
             <input id="input-email" class="form-control form-control-alternative" placeholder="youremail@example.com" type="text" value="${result[0].email}" readonly>`
        phone = `<label class="form-control-label" for="input-phone">Phone</label>
             <input id="input-phone" class="form-control form-control-alternative" placeholder="+62xxx-xxxx-xxxx" type="text" value="${result[0].phone}" readonly>`
        address = `<label class="form-control-label" for="input-address">Role</label>
              <input id="input-role" class="form-control form-control-alternative" placeholder="Role" value="${result[0].role}" readonly>`
        department = `<label class="form-control-label" for="input-department">Salary</label>
                  <input id="input-salary" class="form-control form-control-alternative" placeholder="Salary" value="${formatRupiah(result[0].salary.toString(), 'Rp. ')}" type="text" readonly>`
        $(".account-id").html(userId);
        $(".fname").html(fname);
        $(".lname").html(lname);
        $(".bDate").html(birthDate);
        $(".genderUser").html(gender);
        $(".emailAddrs").html(email);
        $(".contactPhone").html(phone);
        $(".addrs").html(address);
        $(".departmentName").html(department);
    }).fail((error) => {
        alert("error");
    });
});

//CHANGE PASS
$("#editFormPass").on('click', '#saveChanges', function (event) {
    event.preventDefault();
    var objP = new Object();
    objP.Email = $("#input-email").val();
    objP.Password = $("#passAdmin").val();
    objP.NewPassword = $("#passAdminNew").val();
    //console.log(objP.Email);
    //console.log(objP.Password);
    //console.log(objP.NewPassword);
    $.ajax({
        url: "https://localhost:44324/API/Account/Changepass",
        type: "POST",
        data: JSON.stringify(objP),
        headers: {
            "content-type": "application/json;charset=UTF-8" // Or add this line
        }, success: function (data) {
            $('#editModalPass').modal('hide');
        }
    })
})

$(".page-breadcrumb").on('click', '#btnChangePass', function () {
    $("#editModalPass").modal("show");
})

function EditProfile() {
    $.ajax({
        url: "https://localhost:44324/API/Account/Profile/" + userID
    }).done((result) => {
        fname = `<label class="form-control-label" for="input-first-name">First name</label>
             <input type="text" id="input-first-name" class="form-control form-control-alternative" placeholder="First name" value="${result[0].firstName}">`
        lname = `<label class="form-control-label" for="input-last-name">Last name</label>
             <input type="text" id="input-last-name" class="form-control form-control-alternative" placeholder="Last name" value="${result[0].lastName}">`
        birthDate = `<label class="form-control-label" for="input-birthdate">Birth Date</label>
                 <input type="date" id="input-birthdate" class="form-control form-control-alternative" placeholder="Your Birth Date" value="${result[0].birthDate.slice(0, 10)}">`
        gender = `<label class="form-control-label" for="input-gender">Gender</label>
                  <select name="gender" id="input-gender" class="form-control form-control-alternative">
                    <option value="${result[0].gender}" hidden></option>
                    <option value="Male">Male</option>
                    <option value="Female">Female</option>
                  </select>`
        email = `<label class="form-control-label" for="input-email">Email Address</label>
             <input id="input-email" class="form-control form-control-alternative" placeholder="youremail@example.com" type="text" value="${result[0].email}">`
        phone = `<label class="form-control-label" for="input-phone">Phone</label>
             <input id="input-phone" class="form-control form-control-alternative" placeholder="+62xxx-xxxx-xxxx" type="text" value="${result[0].phone}">`
        address = `<label class="form-control-label" for="input-role">Role</label>
              <input id="input-address" class="form-control form-control-alternative" placeholder="Home Address" value="${result[0].role}">`
        department = `<label class="form-control-label" for="input-department">Department Name</label>
                      <input id="input-salary" class="form-control form-control-alternative" placeholder="Salary" value="${result[0].salary}" type="number">`
        $(".fname").html(fname);
        $(".lname").html(lname);
        $(".bDate").html(birthDate);
        $(".genderUser").html(gender);
        $(".emailAddrs").html(email);
        $(".contactPhone").html(phone);
        $(".addrs").html(address);
        $(".departmentName").html(department);
        $(".btnFinishEdit").html(buttonFinishEdit);
    }).fail((error) => {
        alert("error");
    });
}

function SubmitEdit() {
    var editProfile = new Object();
    editProfile.Id = $('#input-userId').val();
    editProfile.FirstName = $('#input-first-name').val();
    editProfile.LastName = $('#input-last-name').val();
    editProfile.GenderId = $('#input-gender').val();
    editProfile.BirthDate = $('#input-birthdate').val();
    editProfile.Address = $('#input-address').val();
    editProfile.Contact = $('#input-phone').val();
    editProfile.Email = $('#input-email').val();
    editProfile.DepartmentId = $('#input-department').val();
    $.ajax({
        type: "PUT",
        url: "https://localhost:44324/API/person/",
        data: JSON.stringify(editProfile),
        contentType: "application/json; charset=utf-8",
        datatype: "json"
    }).done((success) => {
        Swal.fire(
            'Good job!',
            'Data successfully updated !',
            'success'
        );
        $.ajax({
            url: "https://localhost:44324/API/Accounts/Profile/" + userID
        }).done((result) => {
            fname = `<label class="form-control-label" for="input-first-name">First name</label>
             <input type="text" id="input-first-name" class="form-control form-control-alternative" placeholder="First name" value="${result[0].firstName}" readonly>`
            lname = `<label class="form-control-label" for="input-last-name">Last name</label>
             <input type="text" id="input-last-name" class="form-control form-control-alternative" placeholder="Last name" value="${result[0].lastName}" readonly>`
            birthDate = `<label class="form-control-label" for="input-birthdate">Birth Date</label>
                 <input type="date" id="input-birthdate" class="form-control form-control-alternative" placeholder="Your Birth Date" value="${result[0].birthDate.slice(0, 10)}" readonly>`
            gender = `<label class="form-control-label" for="input-gender">Gender</label>
                  <input type="text" id="input-gender" class="form-control form-control-alternative" placeholder="Your Gender" value="${result[0].gender}" readonly>`
            email = `<label class="form-control-label" for="input-email">Email Address</label>
             <input id="input-email" class="form-control form-control-alternative" placeholder="youremail@example.com" type="text" value="${result[0].email}" readonly>`
            phone = `<label class="form-control-label" for="input-phone">Phone</label>
             <input id="input-phone" class="form-control form-control-alternative" placeholder="+62xxx-xxxx-xxxx" type="text" value="${result[0].contact}" readonly>`
            address = `<label class="form-control-label" for="input-address">Address</label>
              <input id="input-address" class="form-control form-control-alternative" placeholder="Home Address" value="${result[0].address}" readonly>`
            department = `<label class="form-control-label" for="input-department">Department Name</label>
                      <input id="input-department" class="form-control form-control-alternative" placeholder="Your Department" value="${result[0].department}" type="text" readonly>`
            buttonFinishEdit = ``
            $(".fname").html(fname);
            $(".lname").html(lname);
            $(".bDate").html(birthDate);
            $(".genderUser").html(gender);
            $(".emailAddrs").html(email);
            $(".contactPhone").html(phone);
            $(".addrs").html(address);
            $(".departmentName").html(department);
            $(".btnFinishEdit").html(buttonFinishEdit);
        }).fail((error) => {
            Swal.fire(
                'Error!',
                'Data failed updated !',
                'error'
            );
        });
    }).fail((notSuccess) => {
        Swal.fire(
            'Error!',
            'Data failed updated !',
            'error'
        );
    });
}