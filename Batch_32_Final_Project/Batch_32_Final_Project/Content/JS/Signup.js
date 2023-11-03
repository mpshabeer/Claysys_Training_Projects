
document.addEventListener('DOMContentLoaded', function () {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    today = yyyy + '-' + mm + '-' + dd;
    document.getElementById("Dateofbirth").setAttribute("max", today);
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





