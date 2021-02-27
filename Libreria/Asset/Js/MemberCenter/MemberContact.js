//import index from "../../../Scripts/esm/popper-utils";

//import { forEach } from "../../../Scripts/fontawesome/v4-shims";

//動態調整服務類型的選單
let serviceClassSelect = document.getElementById("service-class");
let serviceClassDetailSelect = document.getElementById("service-class-detail");

serviceClassSelect.onchange = changeDetailOption;

function changeDetailOption() {
    serviceClassDetailSelect.options.length = 0;
    serviceClassDetailSelect.add(new Option("請選擇", "0"));
    let index = serviceClassSelect.selectedIndex;

    if (index == 0) //請選擇
    {

    }
    else if (index == 1) //會員登入
    {
        serviceClassDetailSelect.add(new Option("辦卡諮詢", "card-consultation"));
        serviceClassDetailSelect.add(new Option("權益查詢", "rights-query"));
        serviceClassDetailSelect.add(new Option("資料異動", "data-change"));
        serviceClassDetailSelect.add(new Option("電子報時務", "Newsletter"));
        serviceClassDetailSelect.add(new Option("活動訊息", "event-message"));
        serviceClassDetailSelect.add(new Option("服務建議", "service-suggestion"));
        serviceClassDetailSelect.add(new Option("服務讚美", "service-praise"));
    }
    else if (index == 2) //註冊登入
    {
        serviceClassDetailSelect.add(new Option("APP註冊登入", "APP-registration-login"));
        serviceClassDetailSelect.add(new Option("迷Liberia網站註冊登入", "Liberia-website-registration-login"));
        serviceClassDetailSelect.add(new Option("網路書店註冊登入", "online-bookstore-registration-login"));
        serviceClassDetailSelect.add(new Option("其它平台註冊登入", "register-on-other-platforms"));
        serviceClassDetailSelect.add(new Option("手機驗證", "phone-verification"));
    }
    else if (index == 3) //APP網站
    {
        serviceClassDetailSelect.add(new Option("APP門市預訂", "APP-store-booking"));
        serviceClassDetailSelect.add(new Option("電子券", "electronic-coupons"));
        serviceClassDetailSelect.add(new Option("線上點數兌換", "online-points-exchange"));
        serviceClassDetailSelect.add(new Option("eslite Pay", "eslite-Pay"));
        serviceClassDetailSelect.add(new Option("APP其它相關", "APP-other-related"));
        serviceClassDetailSelect.add(new Option("社群、官網", "community-official-website"));
        serviceClassDetailSelect.add(new Option("活動訊息", "event-message"));
    }
    else if (index == 4) //Liberia線上
    {
        serviceClassDetailSelect.add(new Option("註冊登入", "signup-login"));
        serviceClassDetailSelect.add(new Option("網書訂單", "online-book-order"));
        serviceClassDetailSelect.add(new Option("操作流程", "operating-procedures"));
        serviceClassDetailSelect.add(new Option("商品諮詢", "commodity-consultation"));
        serviceClassDetailSelect.add(new Option("電子報服務", "newsletter-service"));
        serviceClassDetailSelect.add(new Option("團購服務", "group-purchase-service"));
        serviceClassDetailSelect.add(new Option("活動訊息", "event message"));
        serviceClassDetailSelect.add(new Option("服務建議", "service-suggestion"));
        serviceClassDetailSelect.add(new Option("服務讚美", "service-praise"));
    }
    else if (index == 5) //書店相關
    {
        serviceClassDetailSelect.add(new Option("營業資訊", "business-information"));
        serviceClassDetailSelect.add(new Option("活動訊息", "event message"));
        serviceClassDetailSelect.add(new Option("商品諮詢", "commodity-consultation"));
        serviceClassDetailSelect.add(new Option("服務建議", "service-suggestion"));
        serviceClassDetailSelect.add(new Option("服務讚美", "service-praise"));
    }
    else if (index == 6) //商店相關
    {
        serviceClassDetailSelect.add(new Option("營業資訊", "business-information"));
        serviceClassDetailSelect.add(new Option("活動訊息", "event message"));
        serviceClassDetailSelect.add(new Option("商品諮詢", "commodity-consultation"));
        serviceClassDetailSelect.add(new Option("服務建議", "service-suggestion"));
        serviceClassDetailSelect.add(new Option("服務讚美", "service-praise"));
    }
    else if (index == 7) //電影院、表演廳 <!-- 刪除 -->
    {
        serviceClassDetailSelect.add(new Option("營業資訊", "business-information"));
        serviceClassDetailSelect.add(new Option("票務相關", "ticket-related"));
        serviceClassDetailSelect.add(new Option("服務建議", "service-suggestion"));
        serviceClassDetailSelect.add(new Option("服務讚美", "service-praise"));
    }
    else if (index == 8) //行旅、時尚、酒窖 <!-- 刪除 -->
    {
        serviceClassDetailSelect.add(new Option("營業資訊", "business-information"));
        serviceClassDetailSelect.add(new Option("活動訊息", "event message"));
        serviceClassDetailSelect.add(new Option("商品諮詢", "commodity-consultation"));
        serviceClassDetailSelect.add(new Option("服務建議", "service-suggestion"));
        serviceClassDetailSelect.add(new Option("服務讚美", "service-praise"));
    }
    else if (index == 9) //異界合作 <!-- 刪除 -->
    {
        serviceClassDetailSelect.add(new Option("活動企劃", "event-planning"));
        serviceClassDetailSelect.add(new Option("設店合作", "store-cooperation"));
        serviceClassDetailSelect.add(new Option("上架/設櫃", "rack-setup"));
    }
    else if (index == 10) //外籍旅客
    {
        serviceClassDetailSelect.add(new Option("外籍旅客", "foreign-travelers"));
    }
    else if (index == 11) //其他
    {
        serviceClassDetailSelect.add(new Option("實體門市相關問題", "related-physical-stores"));
        serviceClassDetailSelect.add(new Option("團購服務", "group-purchase-service"));
        serviceClassDetailSelect.add(new Option("退稅服務", "tax-refund-service"));
        serviceClassDetailSelect.add(new Option("服務建議", "service-suggestion"));
        serviceClassDetailSelect.add(new Option("服務讚美", "service-praise"));
        serviceClassDetailSelect.add(new Option("其它", "other"));
    }
}




//reset按鈕
let resetBtn = document.getElementById("reset-form");
let needResetText = document.querySelectorAll(".contact-block input[type='text']");
let needResetTextarea = document.querySelectorAll(".contact-block textarea");

resetBtn.onclick = resetInput;

function resetInput() {
    for (let i = 0; i < needResetText.length; i++) {
        needResetText[i].value = "";
    }

    for (let i = 0; i < needResetTextarea.length; i++) {
        needResetTextarea[i].value = "";
    }
}




let tags = [];
tags[0] = document.getElementById("tag1");
tags[1] = document.getElementById("tag2");
tags[2] = document.getElementById("tag3");

let tagTargets = [];
tagTargets[0] = document.getElementById("contact-block");
tagTargets[1] = document.getElementById("commonProblem-block");

tags.forEach(function (tag, index) {
    tag.addEventListener("change", function () { changeTag(tag, tagTargets, index); })
})

function changeTag(tag, tagTargets, index) {
    tagTargets.forEach(function (tagTarget) {
        tagTarget.style.display = "none";
    })

    if (tag.checked == true && tagTargets[index] != null) {
        tagTargets[index].style.display = "block";
    }
}





let ProblemClassSelect = document.getElementById("ProblemClass");

let AnswerBlock = [];
for (let i = 0; i < ProblemClassSelect.length; i++) {
    AnswerBlock[i] = document.getElementById("Answer-block-" + i);
    if (AnswerBlock[i] != null) {
        AnswerBlock[i].style.display = "none";
    }
}
if (AnswerBlock[0] != null) {
    AnswerBlock[0].style.display = "block";
}

ProblemClassSelect.addEventListener("change", function () { PloblemChoose(); })

function PloblemChoose() {
    for (let i = 0; i < AnswerBlock.length; i++) {
        if (AnswerBlock[i] != null) {
            AnswerBlock[i].style.display = "none";
        }
    }

    let index = ProblemClassSelect.selectedIndex;
    if (AnswerBlock[index] != null) {
        AnswerBlock[index].style.display = "block";
    }
}


