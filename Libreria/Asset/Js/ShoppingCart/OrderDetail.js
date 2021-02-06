let title = document.querySelector(".active_title")
let input = document.querySelector(".active_input")
let txt = document.querySelector(".active_txt")
let active = document.querySelector("#invoice_profile_active")
let _circle = document.querySelector(".circle")
let counties = document.querySelector(".recipient_option_counties")



window.onload = () => {
    // // getAddressJSON()
    // findTown()
    document.querySelector("#select").addEventListener("change", selectChange);
}

let select;
function selectChange() {
    let select = document.querySelector("#select").value;

    if (select == "1") {
        title.innerHTML = "";
        input.innerHTML = "";
        txt.innerHTML = "";
    }
    if (select == "2") {
        title.innerHTML = "<p>載具號碼</p>";
        input.innerHTML = "<input placeholder='手機載具號碼'>";
        txt.innerHTML = "<p>（限大寫英數字。使用電子發票，您需持有手機載具，詳見<span>財政部說明</span>。）</p>"
    }
    if (select == "3") {
        title.innerHTML = "<p>憑證號碼</p>"
        input.innerHTML = "<input placeholder='自然人憑證載具號碼'>"
        txt.innerHTML = "<p>（使用自然人憑證電子發票，您需辦理自然人憑證，詳見<span>財政部說明</span>。）</p>"
    }
    if (select == "4") {
        title.innerHTML = "";
        input.innerHTML = "";
        txt.innerHTML = "";
    }
    if (select == "5") {
        title.innerHTML = "<p>統一編號</p>"
        input.innerHTML = "<input placeholder='統一編號'>"
        txt.innerHTML = "<p>※ 配合財政部新版電子發票格式，電子發票證明聯無抬頭欄位</p>"
    }
    if (select == "6") {
        title.innerHTML = ""
        input.innerHTML = ""
        txt.innerHTML = "<p> 將發票捐贈給財團法人伊甸社會福利基金會 (依據法令規定，已捐贈的發票無法索回，如有退換貨需求，該張發票將予以作廢。)</p>"
    }
}


let op7 = document.querySelector("#option_7-11")
let opOK = document.querySelector("#option_OK")
let opDe = document.querySelector("#option_delivery")
let opPick = document.querySelector("#option_pickup")
let _step2 = document.querySelector(".step2")
let deOP = document.querySelector(".delivery_option")
let _click = document.querySelectorAll(".circle")

let clicked = null;
let clicked2 = null;
_step2.querySelectorAll(".click_options").forEach((x) => {
    x.addEventListener("click", function () {
        let temp = clicked;
        if (temp != null) {
            let target = document.querySelector(`#${temp} .fa-dot-circle`);
            target.classList.remove('fa-dot-circle');
            target.classList.add('fa-circle');
        }
        // clicked.innerHTML = "<i class='far fa-circle'></i>"
        let newTarget = document.querySelector(`#${x.id} .fa-circle`);
        newTarget.classList.remove('fa-circle');
        newTarget.classList.add('fa-dot-circle');
        clicked = x.id;


        // let temp = clicked;
        // clicked.innerHTML = "<i class='far fa-dot-circle'></i>";

    })
});
_step2.querySelectorAll(".click_options2").forEach((x) => {
    x.addEventListener("click", function () {
        let temp = clicked2;
        if (temp != null) {
            let target = document.querySelector(`#${temp} .fa-dot-circle`);
            target.classList.remove('fa-dot-circle');
            target.classList.add('fa-circle');
        }
        // clicked.innerHTML = "<i class='far fa-circle'></i>"
        let newTarget = document.querySelector(`#${x.id} .fa-circle`);
        newTarget.classList.remove('fa-circle');
        newTarget.classList.add('fa-dot-circle');
        clicked2 = x.id;


        // let temp = clicked;
        // clicked.innerHTML = "<i class='far fa-dot-circle'></i>";

    })
});
/* <i class="far fa-dot-circle"></i> */
/* <i class="far fa-circle"></i> */


// let url = "https://raw.githubusercontent.com/eric861129/BuildSchoolHW/main/TwArea.json";
// let Town = document.getElementById("Town");
// let taiwanAddress = [];
// function getAddressJSON() {
//     let xhr = new XMLHttpRequest();
//     xhr.onload = function () {
//         taiwanAddress = JSON.parse(this.responseText);
//     }
//     xhr.open("GET", url);
//     xhr.send();
// }

// function findTown() {
//     Town.innerHTML = '<option selected>選取鄉政市區</option>';

//     _area = document.querySelector("#Region").value;
//     document.querySelector("#Town").disabled = null;
//     areaArray.forEach(x => {
//         if (x.Counties == _area) {
//             taiwanAddress = x.township;
//             CountiesLat = x.lat;
//             CountiesLng = x.lng;
//             townArray.forEach(item => {
//                 let newOption = document.createElement('option');
//                 texNode = document.createTextNode(item);
//                 newOption.appendChild(texNode);
//                 Town.appendChild(newOption);
//             });
//         }
//     });
// }
