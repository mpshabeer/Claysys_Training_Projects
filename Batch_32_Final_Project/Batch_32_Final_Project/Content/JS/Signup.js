document.addEventListener('DOMContentLoaded', function() {
    var today = new Date();
    var month = today.getMonth() + 1; 
    var year = today.getFullYear();
    var tdate = today.getDate();
  
  
    var maxdate = year + "-" + month + "-" + tdate;

    document.getElementById("Dateofbirth").setAttribute("max", maxdate);

  });
  






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





