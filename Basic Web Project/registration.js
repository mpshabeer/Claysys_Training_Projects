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





function myFunction2()
{
    username=document.getElementById("username").value
    pas1=document.getElementById("ps1").value
    pas2=document.getElementById("ps2").value
    if(username==""){
        document.getElementById("warn").innerHTML="<b>Enter Username</b>"

    }

   else if(pas1=="" || pas2=="") {

        document.getElementById("warn").innerHTML="<b> Please enter Password </b>"
    }
    else if(pas1!=pas2){
        document.getElementById("warn").innerHTML="<b> Password Not Match </b>"
    }
   

    // if (email.match(validRegex)==false) {
    //     document.getElementById("warn").innerHTML="<b> Please Enter a valid E-mail</b>"
    //     }
    }



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
