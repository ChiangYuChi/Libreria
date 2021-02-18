let btn = document.querySelector("#btn_yes");
let input = document.getElementById("input");
btn.setAttribute("disabled", true);

input.onclick = function () {
    btn.removeAttribute("disabled");
}