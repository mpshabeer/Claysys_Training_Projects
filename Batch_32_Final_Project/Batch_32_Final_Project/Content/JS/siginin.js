
function validateemail() {

    var validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*$/;
    const email = document.getElementById("Email").value;
    if (email == "") {
        document.getElementById("warnemail").innerHTML = "Please enter your Email"
    }
    else if (!email.match(validRegex)) {
        document.getElementById("warnemail").innerHTML = " Please enter a valid Email"
    }
    else {
        document.getElementById("warnemail").innerHTML = ""
    }
}


function validatepssword() {
    const passwordPattern = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$/;
    password = document.getElementById("Password").value;
    if (password.length == "") {
        document.getElementById("passwordwarn").innerHTML = "Please enter your password"

    }

    else if (passwordPattern.test(password)) {
        document.getElementById("passwordwarn").innerHTML = ""

    }
    else {

        document.getElementById("passwordwarn").innerHTML = "Password must contain at least one digit, one lowercase letter, one uppercase letter, and be at least 8 characters long";
    }
}