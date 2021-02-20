
let passwordInput = document.getElementById("passwordInput");
let checkPasswordInput = document.getElementById("checkPasswordInput");

let submitInput = document.getElementById("submitInput");

submitInput.addEventListener("click", checkPassword);

function checkPassword() {

    if (checkPasswordInput.value === passwordInput.value) {
        checkPasswordInput.setCustomValidity("");
    }
    else {
        checkPasswordInput.setCustomValidity("密碼必須相同。");
    }

    return false;
}
