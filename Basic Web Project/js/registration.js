document.addEventListener('DOMContentLoaded', function() {
    var today = new Date();
    var month = today.getMonth() + 1; 
    var year = today.getFullYear();
    var tdate = today.getDate();
  
    if (month < 10) {
      month = "0" + month;
    }
    if (tdate < 10) {
      tdate = "0" + tdate;
    }
    minyear=year-20;
    console.log(minyear)
    var maxdate = year + "-" + month + "-" + tdate;
    var mindate= minyear + "-" + month + "-" + tdate;
    document.getElementById("dob").setAttribute("max", maxdate);
    document.getElementById("dob").setAttribute("min",mindate);
  });
  
function validdates(){
    var today = new Date();
    var month = today.getMonth() + 1; 
    var year = today.getFullYear();
    var tdate = today.getDate();
    var maxdate = year + "-" + month + "-" + tdate;
    minyear=year-20;
    var mindate= minyear + "-" + month + "-" + tdate;
    selecteddate=document.getElementById("dob").value;
    if (selecteddate==""){
        document.getElementById("datewarn").innerHTML="Please select date of birth";
        document.getElementById("dob").value="";

    }
    else if (selecteddate>maxdate){
        
        document.getElementById("datewarn").innerHTML="Please select a valid date of birth";
        document.getElementById("dob").value="";

    }
    else if(selecteddate<mindate){
        document.getElementById("datewarn").innerHTML="Maximum age is 20";
        document.getElementById("dob").value="";

    }
    else{
    var birth = new Date(selecteddate);
    var bdate=birth.getDay();
    var bmonth=birth.getMonth();
    var byear=birth.getFullYear();

    if(bdate<=tdate){
        bmonth=bmonth+1;

    }
    if(bmonth>month){
        byear=byear+1;
    }
    age=year-byear;
   
    document.getElementById("age").value=age;
}

}

function validatefirstname() {
    
    const firstname = document.getElementById("firstname").value;
    const validfirstname = /^[A-Za-z]+$/; 

    if (firstname == "") {
        document.getElementById("fnamewarn").innerHTML = "Please enter your firstname";
    } else if (!validfirstname.test(firstname)) {
        document.getElementById("fnamewarn").innerHTML = "First should contain only letters";
    } else {
        document.getElementById("fnamewarn").innerHTML = "";
    }
}




const statevisecity={
    Kerala:["Kochi","Calicut","Tiruvanadapuram","Malappuram"],
    Tamilnadu:["Chennai","Coimbatore","Vellore","Madurai"],
    Karnataka:["Bangalore","Mysuru","Chikmagalur","Kalaburagi"]
}
function selectcity(value){
    if(value.length==0){
        document.getElementById("cityselect").innerHTML="<option></option>"
    }
    else{
        allcitys="";
        for(city in statevisecity[value]){
            allcitys+="<option>"+statevisecity[value][city]+"</option>"
        }
        document.getElementById("cityselect").innerHTML=allcitys;
    }
}


function validatelastname() {
    
    const lastname = document.getElementById("lastname").value;
    const validfirstname = /^[A-Za-z]+$/; 

    if (lastname == "") {
        document.getElementById("lnamewarn").innerHTML = "Please enter your lastname";
    } else if (!validfirstname.test(lastname)) {
        document.getElementById("lnamewarn").innerHTML = "Lastname should contain only letters";
    } else {
        document.getElementById("lnamewarn").innerHTML = "";
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

function validatemail(){
    
    var validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*$/;
    const email = document.getElementById("email").value;
    if (email == "") {
        document.getElementById("warnemail").innerHTML="Please enter your e-mail"
    }
    else if (!email.match(validRegex)) {
            document.getElementById("warnemail").innerHTML=" Please enter a valid e-mail"
            }
    else{
        document.getElementById("warnemail").innerHTML=""
    }
}

function validusername()
{
    user=document.getElementById("username").value

    if (user.length==""){
        document.getElementById("usernamewarn").innerHTML="Please enter username ";
    }
    else if (user.length<3){
        document.getElementById("usernamewarn").innerHTML="Username must need atleast three character long "
    }
    else{
        document.getElementById("usernamewarn").innerHTML=""
    }
}


function validapassword(){
    const passwordPattern = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$/;
    password=document.getElementById("password").value;
    if (password.length=="") {
        document.getElementById("passwordwarn").innerHTML="Please enter your password"
       
    } 

    else if (passwordPattern.test(password)) {
        document.getElementById("passwordwarn").innerHTML=""
       
    } 
    else {
       
    document.getElementById("passwordwarn").innerHTML = "Password must contain at least one digit, one lowercase letter, one uppercase letter, and be at least 8 characters long";
    }
}
function validapassword2(){
    const passwordPattern = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$/;
    password1=document.getElementById("password").value;
    password=document.getElementById("password2").value;
    if (password.length=="") {
        document.getElementById("passwordwarn2").innerHTML="Please enter your password"
       
    } 
    else if(password1!=password){
        document.getElementById("passwordwarn2").innerHTML="Passwords doesn't match"

    }

    else if (passwordPattern.test(password)) {
        document.getElementById("passwordwarn2").innerHTML=""
       
    } 
    else {
       
    document.getElementById("passwordwarn2").innerHTML = "Password must contain at least one digit, one lowercase letter, one uppercase letter, and be at least 8 characters long";
    }
}


function validateadr(){
    address= document.getElementById("address").value;
    if (address.length=""){
        document.getElementById("addresswarn").innerHTML="Please enter your address"

    }
     else if (address.length<6){
        document.getElementById("addresswarn").innerHTML="Address need atleast six character long";
    }
    else{
        document.getElementById("addresswarn").innerHTML=""

    }
}
const dobInput = document.getElementById('dob');
const ageInput = document.getElementById('age');

// Define a function to calculate age based on the date of birth
function calculateAge(dateOfBirth) {
    const dobDate = new Date(dateOfBirth);
    const today = new Date();
    const age = today.getFullYear() - dobDate.getFullYear();
    return age;
}

// Add an onfocusout event listener to the date of birth input
dobInput.addEventListener('focusout', function() {
    // Get the selected date of birth value
    const dobValue = dobInput.value;

    // Calculate age and update the age input
    if (dobValue) {
        const age = calculateAge(dobValue);
        ageInput.value = age;
    } else {
        // Clear the age input if no date of birth is selected
        ageInput.value = '';
    }
});






    function valform(){
        fname=document.getElementById("fname").value
        lname=document.getElementById("lname").value
        dob=document.getElementById("dob").value
        age=document.getElementById("age").value
       
        gndr=document.getElementById("gndr").value
        phone=document.getElementById("phone").value

        email=document.getElementById("email").value
        adr=document.getElementById("adr").value
        state=document.getElementById("state").value
        city=document.getElementById("cityselect").value

        username=document.getElementById("username").value
        pas1=document.getElementById("ps1").value
        pas2=document.getElementById("ps2").value
        if(fname.length<3){
            alert("First Name must be at least 3 characters long.")
          
        }
        else if(lname==""){
            alert("Enter Last_Name")
        }
        else if(dob==""){
            alert("Enter Date Of Birth")
        }
        else if(age==""){
            alert("Enter Age")
        }
        else if(gndr==""){
            alert("Choose your gender")
        }
        else if(phone.length<10){
            alert("Enter Phone Number Correctly")
        }
        else if(email==""){
            alert("Enter Email")
        }
        else if(adr==""){
            alert("Enter Address")
        }
        else if(state==""){
            alert("Choose State")
        }
        else if(username==""){
            alert("Enter username")
        }
        else if(pas1.length<6){
            alert("password is too short")
        }
        else if(pas2.length<6){
            alert("password is too short")
        }
        if(pas1!=pas2 ){
            alert("Passwords Not Match")
        }
    }
