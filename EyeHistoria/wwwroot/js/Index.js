function myFunction(checkboxId) {
    var checkBox = document.getElementById(checkboxId);
    var onsetText = document.getElementById(checkBox.dataset.onset);
    var locationText = document.getElementById(checkBox.dataset.location);

    if (checkBox.checked) {
        onsetText.style.display = "block";
        locationText.style.display = "block";
    } else {
        onsetText.style.display = "none";
        locationText.style.display = "none";
    }
}

document.getElementById("eyeRedness").addEventListener("click", function () {
    myFunction("eyeRedness");
});

document.getElementById("eyeItch").addEventListener("click", function () {
    myFunction("eyeItch");
});

document.getElementById("eyeSwelling").addEventListener("click", function () {
    myFunction("eyeSwelling");
});

var closebtns = document.getElementsByClassName("close");
var i;

for (i = 0; i < closebtns.length; i++) {
    closebtns[i].addEventListener("click", function () {
        this.parentElement.style.display = 'none';
    });
}