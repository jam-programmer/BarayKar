function modalOtp(){
    $("#otpLogin").modal('toggle');
}

function sendOtp() {
    var userName = $("#userName").val();
    if (userName == "") {
        ShowWarningAlert("لطفا نام کاربری معتبر را وارد نمایید");
    } else if (userName.length != 11 && userName.length != 10) {
        ShowWarningAlert("لطفا نام کاربری معتبر را وارد نمایید");
    } else {
        $("#spinner-send-otp").removeClass("d-none");
        $.ajax({
            url: "/Identity/SendOtpCode?userName=" + userName,
            type: 'Post',
            contentType: 'application/json; charset=utf-8',
            data: "",
            success: function (data) {
                $("#userName").attr('disabled', true);
                ShowSuccessAlert(data.data);
                $("#otpCode").attr('disabled', false);
                $("#spinner-send-otp").addClass("d-none");
                $("#btn-otp-send").attr('disabled', true);
                $("#div-counter").removeClass("d-none");
                CounterDownBtn();
            },
            error: function (result) {
                ShowWarningAlert(result.responseJSON.message[0]);
                $("#spinner-send-otp").addClass("d-none");
            },
        });
    }
}

function ShowWarningAlert(title) {
    Swal.fire({
        icon: "error",
        title: "خطا",
        text: title
    });
}

function ShowSuccessAlert(title) {
    Swal.fire({
        icon: "success",
        title: "موفق",
        text: title
    });
}

function CounterDownBtn() {
    let timeLeft = 120;
     var intervalCounter = setInterval(function (){
        timeLeft--;
         if (timeLeft > 0) {
             $("#counter-spn").text(timeLeft);
        } else {
             $("#btn-otp-send").attr('disabled', false);
             $("#div-counter").addClass("d-none");
            clearInterval(intervalCounter);
        }
    }, 1000);
}

function login() {
    var userName = $("#userName").val();
    var code = $("#otpCode").val();
    if (userName == "") {
        ShowWarningAlert("لطفا نام کاربری معتبر را وارد نمایید");
    } else if (code == "") {
        ShowWarningAlert("لطفا کد فعال سازی معتبر را وارد نمایید");
    } else {
        $("#spinner-send-otp-btn").removeClass("d-none");
        $("#btn-send-otp-code-login").attr('disabled', true);
        $.ajax({
            url: "/Identity/LoginOtp?userName=" + userName + "&code=" + code,
            type: 'Post',
            contentType: 'application/json; charset=utf-8',
            data: "",
            success: function (data) {
                $("#spinner-send-otp-btn").addClass("d-none");
                $("#btn-send-otp-code-login").attr('disabled', false);
                window.location.href = data.data;
            },
            error: function (result) {
                $("#spinner-send-otp-btn").addClass("d-none");
                $("#btn-send-otp-code-login").attr('disabled', false);
                ShowWarningAlert(result.responseJSON.message[0]);
            },
        });
    }
}