// This function validates the input for the Name
function validateName() {
    const firstname = document.getElementById("Name").value;
    const validFirstname = /^[A-Za-z ]+$/;

    if (firstname.trim() === "") {
        document.getElementById("namewarn").innerHTML = "Please enter your Name";
    } else if (!validFirstname.test(firstname)) {
        document.getElementById("namewarn").innerHTML = "Firstname should contain only letters";
    } else {
        document.getElementById("namewarn").innerHTML = "";
    }
}
// This function validates the input for the Phone number
function validatephone() {
    const phoneInput = document.getElementById("Phone");
    const phonePattern = /^[6789][0-9]{9}$/;

    if (phonePattern.test(phoneInput.value)) {
        document.getElementById("warnphone").innerHTML = ""; 

    } else {

        document.getElementById("warnphone").innerHTML = "Please enter a valid 10-digit Phone number starting with 6, 7, 8, or 9.";

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