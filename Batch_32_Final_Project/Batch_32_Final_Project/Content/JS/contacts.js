      
 function validatename() {
    
    const name = document.getElementById("name").value;
    const validname = /^[A-Za-z]+$/; 

    if (name == "") {
        document.getElementById("namewarn").innerHTML = "Please enter your name";
    } else if (!validname.test(name)) {
        document.getElementById("namewarn").innerHTML = "Name should contain only letters";
    } else {
        document.getElementById("namewarn").innerHTML = "";
    }
}

function validemail(){
    
    var validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*$/;
    const email = document.getElementById("mail").value;
    if (email == "") {
        document.getElementById("mailwarn").innerHTML="Please enter your e-mail"
    }
    else if (!email.match(validRegex)) {
            document.getElementById("mailwarn").innerHTML=" Please enter a valid e-mail"
            }
    else{
        document.getElementById("mailwarn").innerHTML=""
    }
}

           
                      

    function validatephone() {
    const phoneInput = document.getElementById("phone");
    const phonePattern = /^[6789][0-9]{9}$/;

    if (phonePattern.test(phoneInput.value)) {
       
    } else {
        
        document.getElementById("phonewarn").innerHTML = "Please enter a valid 10-digit phone number starting with 6, 7, 8, or 9.";
       
    }
}
        