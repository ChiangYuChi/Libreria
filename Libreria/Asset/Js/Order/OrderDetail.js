
//訂購人資料同收件人資料
let SubscriberEqualRecipient = document.getElementById("SubscriberEqualRecipient");

SubscriberEqualRecipient.addEventListener("change", function () { SubscriberToRecipient() })

function SubscriberToRecipient() {

    let SubscriberName = document.getElementById("SubscriberName");
    let SubscriberCellphone = document.getElementById("SubscriberCellphone");
    let SubscriberTelephone = document.getElementById("SubscriberTelephone");
    let SubscriberAddressCitySelect = document.getElementById("subscriber_option subscriber_option_counties Region");
    let SubscriberAddressRegionSelect = document.getElementById("subscriber_option_district Town");
    let SubscriberAddress = document.getElementById("SubscriberAddress");
    let SubscriberPostalCode = document.getElementById("SubscriberPostalCode")

    //收件人資料
    let RecipientName = document.getElementById("RecipientName");
    let RecipientCellphone = document.getElementById("RecipientCellphone");
    let RecipientTelephone = document.getElementById("RecipientTelephone");
    let AddressCitySelect = document.getElementById("recipient_option recipient_option_counties Region");
    let AddressRegionSelect = document.getElementById("recipient_option_district Town");
    let RecipientAddress = document.getElementById("RecipientAddress");
    let RecipientPostalCode = document.getElementById("RecipientPostalCode");

    if (SubscriberEqualRecipient.checked == true) {

        RecipientName.value = SubscriberName.value;
        RecipientCellphone.value = SubscriberCellphone.value;
        RecipientTelephone.value = SubscriberTelephone.value;

        AddressCitySelect.add(new Option(
            text = SubscriberAddressCitySelect.value,
            value = SubscriberAddressCitySelect.value));
        AddressCitySelect.value = SubscriberAddressCitySelect.value;

        AddressRegionSelect.add(new Option(
            text = SubscriberAddressRegionSelect.value,
            value = SubscriberAddressRegionSelect.value));
        AddressRegionSelect.value = SubscriberAddressRegionSelect.value;

        RecipientAddress.value = SubscriberAddress.value;
        RecipientPostalCode.value = SubscriberPostalCode.value;

    }
    else {
        RecipientName.value = "";
        RecipientCellphone.value = "";
        RecipientTelephone.value = "";

        RecipientAddress.value = "";
        RecipientPostalCode.value = "";
    }

}




//縣市轄區資料
let CityRegionUrl = "https://gist.githubusercontent.com/abc873693/2804e64324eaaf26515281710e1792df/raw/a1e1fc17d04b47c564bbd9dba0d59a6a325ec7c1/taiwan_districts.json";
let CityRegionList = [];
fetch(CityRegionUrl)
    .then(response => response.json())
    .then(result => {
        CityRegionList = result;
        autoPostalCode(SubscriberAddressCitySelect, SubscriberAddressRegionSelect, SubscriberPostalCode);
        autoPostalCode(AddressCitySelect, AddressRegionSelect, RecipientPostalCode);
    })

//縣市轄區郵遞區號
let AddressCitySelect = document.getElementById("recipient_option recipient_option_counties Region");
let AddressRegionSelect = document.getElementById("recipient_option_district Town");
let RecipientPostalCode = document.getElementById("RecipientPostalCode")
let SubscriberAddressCitySelect = document.getElementById("subscriber_option subscriber_option_counties Region");
let SubscriberAddressRegionSelect = document.getElementById("subscriber_option_district Town");
let SubscriberPostalCode = document.getElementById("SubscriberPostalCode")

AddressCitySelect.addEventListener("change", function () { changeAddressRegion(AddressCitySelect, AddressRegionSelect) })
SubscriberAddressCitySelect.addEventListener("change", function () { changeAddressRegion(SubscriberAddressCitySelect, SubscriberAddressRegionSelect) })

AddressRegionSelect.addEventListener("change", function () { autoPostalCode(AddressCitySelect, AddressRegionSelect, RecipientPostalCode) })
SubscriberAddressRegionSelect.addEventListener("change", function () { autoPostalCode(SubscriberAddressCitySelect, SubscriberAddressRegionSelect, SubscriberPostalCode) })

function changeAddressRegion(addressCitySelect, addressRegionSelect) {
    //清空轄區選項
    addressRegionSelect.options.length = 0;
    addressRegionSelect.add(new Option(text = "請選擇", value = ""));

    //查找轄區選項
    let citySelectValue = addressCitySelect.value;
    let regionList = [];
    CityRegionList.some(function (item) {
        if (item.name == citySelectValue) {
            regionList = item.districts;
            return true; //迴圈中止
        }
        else {
            return false; //迴圈繼續
        }
    });

    //放入轄區選項
    regionList.forEach(function (region) {
        addressRegionSelect.add(new Option(region.name, region.name));
    })
}

function autoPostalCode(addressCitySelect, addressRegionSelect, postalCode) {
    //查找轄區選項
    let citySelectValue = addressCitySelect.value;
    let regionList = [];
    CityRegionList.some(function (item) {
        if (item.name == citySelectValue) {
            regionList = item.districts;
            return true; //迴圈中止
        }
        else {
            return false; //迴圈繼續
        }
    });

    //查找所選轄區郵遞區號
    let regionSelectValue = addressRegionSelect.value;
    regionList.some(function (item) {
        if (item.name == regionSelectValue) {
            postalCode.value = item.zip;
            return true;
        }
        else {
            return false;
        }
    })
}




//表單驗證
let submit = document.getElementById("submit");
submit.addEventListener("click", checkForm);

function checkForm() {
    let recipientCellphone = document.getElementById("RecipientCellphone")
    let recipientTelephone = document.getElementById("RecipientTelephone")
    let subscriberCellphone = document.getElementById("SubscriberCellphone")
    let subscriberTelephone = document.getElementById("SubscriberTelephone")
    let recipientPostalCode = document.getElementById("RecipientPostalCode")
    let subscriberPostalCode = document.getElementById("SubscriberPostalCode")

    if (recipientCellphone.validity.patternMismatch == true) {
        recipientCellphone.setCustomValidity("請輸入十個數字");
    }
    else {
        recipientCellphone.setCustomValidity("");
    }

    if (recipientTelephone.validity.patternMismatch == true) {
        recipientTelephone.setCustomValidity("請輸入數字");
    }
    else {
        recipientTelephone.setCustomValidity("");
    }

    if (subscriberCellphone.validity.patternMismatch == true) {
        subscriberCellphone.setCustomValidity("請輸入十個數字");
    }
    else {
        subscriberCellphone.setCustomValidity("");
    }

    if (subscriberTelephone.validity.patternMismatch == true) {
        subscriberTelephone.setCustomValidity("請輸入數字");
    }
    else {
        subscriberTelephone.setCustomValidity("");
    }

    if (recipientPostalCode.validity.patternMismatch == true) {
        recipientPostalCode.setCustomValidity("請輸入數字");
    }
    else {
        recipientPostalCode.setCustomValidity("");
    }

    if (subscriberPostalCode.validity.patternMismatch == true) {
        subscriberPostalCode.setCustomValidity("請輸入數字");
    }
    else {
        subscriberPostalCode.setCustomValidity("");
    }
}


