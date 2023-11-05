var myList = ["Eye Redness", "Eye Irritation", "Blurred Vision", "Double Vision", "Eye Floaters", "Eye Twitching", "Light Sensitivity", "Eye Pain", "Eye Tearing", "Change In Iris Colour", "Night Blindness", "Tunnel Vision", "Eye Discharge", "Misalignment Of Eyes", "Distorted Vision", "Eye Bleeding"];


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

// based on myList, for now myList is fixed
for (let i = 0; i < myList.length; i++) {
    document.getElementById(myList[i]).addEventListener("click", function () {
        myFunction(myList[i]);
    });
}

var closebtns = document.getElementsByClassName("close");
var i;

for (i = 0; i < closebtns.length; i++) {
    closebtns[i].addEventListener("click", function () {
        this.parentElement.style.display = 'none';
    });
}