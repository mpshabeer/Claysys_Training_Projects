// This function validates the input for the Job title
function validatejobtitle() {
    const jobtitle = document.getElementById("JobTitle").value;
    if (jobtitle == "") {
        document.getElementById("titlewarn").innerHTML = "Please Enter Job Title";
    } else {
        document.getElementById("titlewarn").innerHTML = "";
    }
}
// This function validates the input for the Job desscription
function validatedescription() {
    const description = document.getElementById("JobDescription").value;
    if (description == "") {
        document.getElementById("descriptionwarn").innerHTML = "Please Enter Job Description";
    } else if (description.length < 6) {
        document.getElementById("descriptionwarn").innerHTML = "Please enter Job Description with at least two words";
    } else {
        document.getElementById("descriptionwarn").innerHTML = "";
    }
}
// This function validates the input for the Department
function validatedepartment() {
    const department = document.getElementById("Department").value;
    if (department == "") {
        document.getElementById("departmentwarn").innerHTML = "Please Enter Department";
    } else {
        document.getElementById("departmentwarn").innerHTML = "";
    }
}
// This function validates the input for the Location
function validatelocation() {
    const location = document.getElementById("Location").value;
    if (location == "") {
        document.getElementById("locationwarn").innerHTML = "Please Enter Location";
    } else {
        document.getElementById("locationwarn").innerHTML = "";
    }
}
// This function validates the input for the Number of opening 
function validatenumberofopenings() {
    const opening = document.getElementById("NumberOfOpenings").value;
    if (opening == "") {
        document.getElementById("noofopeningwarn").innerHTML = "Please Enter the count of opening";
    } else {
        document.getElementById("noofopeningwarn").innerHTML = "";
    }
}
// This function validates the input for the Qualification
function validatequalification() {
    const qualification = document.getElementById("Qualifications").value;
    if (qualification == "") {
        document.getElementById("qualificationwarn").innerHTML = "Please Enter the Qualification";
    } else {
        document.getElementById("qualificationwarn").innerHTML = "";
    }
}
// This function validates the input for the Responsibilities and duties
function validateduties() {
    const res = document.getElementById("ResponsibilitiesAndDuties").value;
    if (res == "") {
        document.getElementById("dutieswarn").innerHTML = "Please Enter the Responsibilities and Duties";
    } else {
        document.getElementById("dutieswarn").innerHTML = "";
    }
}
// This function validates the input for the Salary
function validatesalary() {
    const salry = document.getElementById("SalaryRange").value;
    if (salry == "") {
        document.getElementById("salarywarn").innerHTML = "Please Enter the Salary range";
    } else {
        document.getElementById("salarywarn").innerHTML = "";
    }
}
// This function validates the input for the Close date
function validateclosedate() {
    const close = document.getElementById("LastDateToApply").value;
    if (close == "") {
        document.getElementById("closedatewarn").innerHTML = "Please Enter the Close date";
    } else {
        document.getElementById("closedatewarn").innerHTML = "";
    }
}
// This function validates the input for the Recruiter in charge
function validaterecruiter() {
    const rqin = document.getElementById("RecruiterInCharge").value;
    if (rqin == "") {
        document.getElementById("recruiterwarn").innerHTML = "Please Enter the Recruiter InCharge";
    } else {
        document.getElementById("recruiterwarn").innerHTML = "";
    }
}
// This function validates the input for the Number of interview rounds
function validateinterviewrounds() {
    const inrounds = document.getElementById("InterviewRounds").value;
    if (inrounds == "") {
        document.getElementById("interviewwarn").innerHTML = "Please Enter the count of Interview rounds";
    } else {
        document.getElementById("interviewwarn").innerHTML = "";
    }
}
// This function validates the input for the Selection process
function validateselection() {
    const selection = document.getElementById("SelectionProcess").value;
    if (selection == "") {
        document.getElementById("selectionwarn").innerHTML = "Please Enter the Selection process";
    } else {
        document.getElementById("selectionwarn").innerHTML = "";
    }
}
