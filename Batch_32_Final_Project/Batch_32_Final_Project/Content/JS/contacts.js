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

function validatephone() {
    const phoneInput = document.getElementById("Phone");
    const phonePattern = /^[6789][0-9]{9}$/;

    if (phonePattern.test(phoneInput.value)) {
        document.getElementById("warnphone").innerHTML = ""; 

    } else {

        document.getElementById("warnphone").innerHTML = "Please enter a valid 10-digit Phone number starting with 6, 7, 8, or 9.";

    }
}