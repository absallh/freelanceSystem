const firstName = document.querySelector('#f_name');
const lastName = document.querySelector('#l_name');
const email = document.querySelector('#userEmail');
const freelancerRadio = document.querySelector('#freelancerRadio');
const clientRadio = document.querySelector('#clientRadio');
const password = document.querySelector('#signup_password');
const conmfirmPass = document.querySelector('#ComfirmPassword');
const singupForm = document.querySelector('.signUp_form');


$('.signupPassword').focusin(function () {
    $('.signUp_form').addClass('up')
});
$('.signupPassword').focusout(function () {
    $('.signUp_form').removeClass('up');
});

// Panda Eye move
$(document).on("mousemove", function (event) {
    var dw = $(document).width() / 15;
    var dh = $(document).height() / 15;
    var x = event.pageX / dw;
    var y = event.pageY / dh;
    $('.eye-ball').css({
        width: x,
        height: y
    });
});

// validation


function validate() {
    $('.signUp_form').addClass('wrong-entry');
    setTimeout(function () {
        $('.signUp_form').removeClass('wrong-entry');
    }, 3000);
}

function nextName() {
    if (firstName.value == '' || lastName.value == '') {
        validate();
    } else {
        $('#Names').hide(500);
        $('#Emails').show(500);
    }
}
function nextEmail() {
    console.log(email.value.includes('@'));
    if ((email.value == '' || !email.value.includes('@')) || (!freelancerRadio.checked && !clientRadio.checked)) {
        validate();
    } else {
        $('#Emails').hide(500);
        $('#Password').show(500);
    }
}

singupForm.addEventListener('submit', (e) => {
    if (password.value == '' || conmfirmPass.value == '' || password.value != conmfirmPass.value) {
        validate();
        e.preventDefault();
    }
});