// fetches list of symptoms from controller
fetch('/Home/GetSymptomsList') // Adjust the URL to match your application's route
    .then(response => response.json())
    .then(data => {

        // data is list of symptoms from SQL
        for (let i = 0; i < data.length; i++) {
            document.getElementById(data[i]["symptomName"]).addEventListener("click", function () {
                myFunction(data[i]["symptomName"]);
            });
        }
    })
    .catch(error => console.error('Error:', error));

function scrollToSymptoms() {
    const symptomsSection = document.querySelector('.col-sm-6');
    symptomsSection.scrollIntoView({ behavior: 'smooth' });
}
function myFunction(checkboxId) {
    var checkBox = document.getElementById(checkboxId);
    var onsetText = document.getElementById(checkBox.dataset.onset);
    var locationText = document.getElementById(checkBox.dataset.location);
    var durationText = document.getElementById(checkBox.dataset.duration);
    var characteristicsText = document.getElementById(checkBox.dataset.characteristics);
    var aggravationText = document.getElementById(checkBox.dataset.aggravation);
    var reliefText = document.getElementById(checkBox.dataset.relief);
    var timingText = document.getElementById(checkBox.dataset.timing);
    var severityText = document.getElementById(checkBox.dataset.severity);

    if (checkBox.checked) {
        onsetText.style.display = "block";
        locationText.style.display = "block";
        durationText.style.display = "block";
        characteristicsText.style.display = "block";
        aggravationText.style.display = "block";
        reliefText.style.display = "block";
        timingText.style.display = "block";
        severityText.style.display = "block";
    } else {
        onsetText.style.display = "none";
        locationText.style.display = "none";
        durationText.style.display = "none";
        characteristicsText.style.display = "none";
        aggravationText.style.display = "none";
        reliefText.style.display = "none";
        timingText.style.display = "none";
        severityText.style.display = "none";
    }
}

var closebtns = document.getElementsByClassName("close");
var i;

for (i = 0; i < closebtns.length; i++) {
    closebtns[i].addEventListener("click", function () {
        this.parentElement.style.display = 'none';
    });
}