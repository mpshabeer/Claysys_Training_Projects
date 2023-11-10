// This function validates the input for the first name
function validatefirstname() {
    const firstname = document.getElementById("Firstname").value;
    // Regular expression to check if the first name contains only letters
    const validfirstname = /^[A-Za-z ]+$/;
    if (firstname == "") {
        document.getElementById("fnamewarn").innerHTML = "Please enter your Firstname";
    } else if (!validfirstname.test(firstname)) {         
        document.getElementById("fnamewarn").innerHTML = "Firstname should contain only letters";
    } 
    else {
        document.getElementById("fnamewarn").innerHTML = "";
    }
}
// This function validates the input for the Last name
function validatelastname() {

    const lastname = document.getElementById("Lastname").value; 
    const validfirstname = /^[A-Za-z ]+$/;
    if (lastname == "") {
        document.getElementById("lnamewarn").innerHTML = "Please enter your Lastname";
    } else if (!validfirstname.test(lastname)) {
        document.getElementById("lnamewarn").innerHTML = "Lastname should contain only letters";
    } else {
        document.getElementById("lnamewarn").innerHTML = "";
    }

}
// This function validates the input for the date of birth and disabled the future date
    function validdates() {
        var today = new Date();
        var month = today.getMonth() + 1;
        var year = today.getFullYear();
        var tdate = today.getDate();
        var maxdate = year + "-" + month + "-" + tdate;
        minyear = year - 20;
        var mindate = minyear + "-" + month + "-" + tdate;
        selecteddate = document.getElementById("Dateofbirth").value;
        if (selecteddate == "") {
            document.getElementById("datewarn").innerHTML = "Please select Date of birth";
            document.getElementById("Dateofbirth").value = "";
        }
        else {
            document.getElementById("datewarn").innerHTML = "";
        }
}

// This function validates the input for the Email 
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

// This function validates the input for the phone number 
function validatephone() {
    const phoneInput = document.getElementById("Phone");
    const phonePattern = /^[6789][0-9]{9}$/;

    if (phonePattern.test(phoneInput.value)) {
        document.getElementById("warnphone").innerHTML = "";

    } else {

        document.getElementById("warnphone").innerHTML = "Please enter a valid 10-digit Phone number starting with 6, 7, 8, or 9.";

    }
}
// This function validates the input for the Address
function validateaddress() {
    address = document.getElementById("Address").value;
    if (address.length = "") {
        document.getElementById("addresswarn").innerHTML = "Please enter your Address"

    }
    else if (address.length < 6) {
        document.getElementById("addresswarn").innerHTML = "Address need atleast six character long";
    }
    else {
        document.getElementById("addresswarn").innerHTML = ""

    }
}

// This function validates the input for the Password
function validatepassword() {
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
// This function validates the input for the Confirm password
function validateconfirmpassword() {
    const passwordPattern = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$/;
    password1 = document.getElementById("Password").value;
    password = document.getElementById("Confirmpassword").value;
    if (password.length == "") {
        document.getElementById("confirmpasswordwarn").innerHTML = "Please enter your Password"

    }
    else if (password1 != password) {
        document.getElementById("confirmpasswordwarn").innerHTML = "Passwords doesn't match"

    }

    else if (passwordPattern.test(password)) {
        document.getElementById("confirmpasswordwarn").innerHTML = ""

    }
    else {

        document.getElementById("confirmpasswordwarn").innerHTML = "Password must contain at least one digit, one lowercase letter, one uppercase letter, and be at least 8 characters long";
    }
}

function validateName() {
    const firstname = document.getElementById("Name").value;
    const validFirstname = /^[A-Za-z ]+$/;

    if (firstname.trim() === "") {
        document.getElementById("namewarn").innerHTML = "Please enter your Firstname";
    } else if (!validFirstname.test(firstname)) {
        document.getElementById("namewarn").innerHTML = "Firstname should contain only letters";
    } else {
        document.getElementById("namewarn").innerHTML = "";
    }
}