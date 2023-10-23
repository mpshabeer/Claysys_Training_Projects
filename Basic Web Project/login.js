function validateLogin() {
    const loginUsername = document.getElementById("username").value;
    const loginPassword = document.getElementById("password").value;

    if (loginUsername == "") {
        document.getElementById("p").innerHTML="Enter Email"
   
    }
    else if ( loginPassword=="") {
        document.getElementById("p").innerHTML="Enter password"
    }

    else if ( loginPassword.length<6) {
        document.getElementById("p").innerHTML="Password is too short"
   
    }
}