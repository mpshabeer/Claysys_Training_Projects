
function validateusername(){
    
    var validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*$/;
    const email = document.getElementById("username").value;
    if (email == "") {
        document.getElementById("warnemail").innerHTML="Please enter e-mail"
    }
    else if (!email.match(validRegex)) {
            document.getElementById("warnemail").innerHTML=" Please enter a valid e-mail"
            }
    else{
        document.getElementById("warnemail").innerHTML=""
    }
}
function validatepassword(){
    const password = document.getElementById("password").value;
    if (password == "") {
        document.getElementById("warnpassword").innerHTML="Please enter your password"
    }
    else{
        document.getElementById("warnpassword").innerHTML=""
    }
}