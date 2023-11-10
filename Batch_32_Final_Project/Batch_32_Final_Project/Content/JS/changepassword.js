// This function validates the input for the password
function validateoldpassword() {
    const passwordPattern = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$/;
    password = document.getElementById("OldPassword").value;
    if (password.length == "") {
        document.getElementById("oldpasswordwarn").innerHTML = "Please enter your password"

    }

    else if (passwordPattern.test(password)) {
        document.getElementById("oldpasswordwarn").innerHTML = ""

    }
    else {

        document.getElementById("oldpasswordwarn").innerHTML = "Password must contain at least one digit, one lowercase letter, one uppercase letter, and be at least 8 characters long";
    }

}
// This function validates the input for the new password
function validatepassword() {
    const passwordPattern = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$/;
    password = document.getElementById("NewPassword").value;
    if (password.length == "") {
        document.getElementById("passwordwarn").innerHTML = "Please enter new password"

    }

    else if (passwordPattern.test(password)) {
        document.getElementById("passwordwarn").innerHTML = ""

    }
    else {

        document.getElementById("passwordwarn").innerHTML = "Password must contain at least one digit, one lowercase letter, one uppercase letter, and be at least 8 characters long";
    }
}
// This function validates the input for the Confirm password
function validateconfirmpassword() {
    const passwordPattern = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$/;
    password1 = document.getElementById("NewPassword").value;
    password = document.getElementById("Confirmpassword").value;
    if (password.length == "") {
        document.getElementById("comfirmpasswordwarn").innerHTML = "Please enter  Password"

    }
    else if (password1 != password) {
        document.getElementById("comfirmpasswordwarn").innerHTML = "Passwords doesn't match"

    }

    else if (passwordPattern.test(password)) {
        document.getElementById("comfirmpasswordwarn").innerHTML = ""

    }
    else {

        document.getElementById("comfirmpasswordwarn").innerHTML = "Password must contain at least one digit, one lowercase letter, one uppercase letter, and be at least 8 characters long";
    }
}