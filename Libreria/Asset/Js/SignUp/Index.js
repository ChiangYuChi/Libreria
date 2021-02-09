let radio = document.querySelector("#radio")
let radio2 = document.querySelector("radio2")
let input = document.querySelector(".valid_documents_input")
let input2 = document.querySelector(".birthday_input")
let input3 = document.querySelector(".password_input")
let input4 = document.querySelector(".password_input_confirm")


let reset = document.querySelector(".btn_reset")

window.onload = () => {
    document.querySelector("#radio").addEventListener("click", click);
    document.querySelector("#radio2").addEventListener("click", click2);
    document.querySelector(".btn_reset").addEventListener("click", resets);
}

function click() {
    input.innerHTML = "<input type='text' placeholder='  請輸入身分證字號' autofocus='autofocus' >"
}
function click2() {
    input.innerHTML = "<input type='text' placeholder='  請輸入護照或居留證號碼' autofocus='autofocus' >"
}
function resets() {
    input.innerHTML = "<input type='text' placeholder='  請輸入身分證字號' autofocus='autofocus' >"
    input2.innerHTML = "<input type='tel' placeholder='  請輸入生日(西元年月日，例:19931203)'>"
    input3.innerHTML = "<input type='password' placeholder='  6~24個英數或數字半形字元'>"
    input4.innerHTML = "<input type='password' placeholder='  確認密碼'>"
}