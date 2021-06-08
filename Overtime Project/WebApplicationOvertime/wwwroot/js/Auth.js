function Login() {
    var Login = new Object();
    Login.Email = $("#Email").val();
    Login.Password = $("#Password").val();
    console.log(Login);
    $.ajax({
        type: 'post',
        url: '/Authenticate/Login',
        data: Login
    }).done((result) => {
        console.log("ok", result);
        if (result == '/TestCORS/Employee' || result == '/TestCORS/Head' || result == '/' ) {
            alert("Successed to Login");
            localStorage.setItem('LoginRes', JSON.stringify(result));
            window.location.href = result;
        }
        else {
            alert("Failed to Login");
            $("#Email").val(null);
            $("#Password").val(null);
        }
    }).fail((result) => {
        console.log(result);
        alert("Failed to Login");
    })
}