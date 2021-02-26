﻿
//訂購人資料同收件人資料
let SubscriberEqualRecipient = document.getElementById("SubscriberEqualRecipient");

SubscriberEqualRecipient.addEventListener("change", function () { SubscriberToRecipient() })

function SubscriberToRecipient() {

    if (SubscriberEqualRecipient.checked == true) {

        //訂購人資料
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

        RecipientName.value = SubscriberName.value;
        RecipientCellphone.value = SubscriberCellphone.value;
        RecipientTelephone.value = SubscriberTelephone.value;
        AddressCitySelect.value = SubscriberAddressCitySelect.value;

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

}




//縣市轄區
let AddressCitySelect = document.getElementById("recipient_option recipient_option_counties Region");
let AddressRegionSelect = document.getElementById("recipient_option_district Town");
let SubscriberAddressCitySelect = document.getElementById("subscriber_option subscriber_option_counties Region");
let SubscriberAddressRegionSelect = document.getElementById("subscriber_option_district Town");

AddressCitySelect.addEventListener("change", function () { changeAddressRegion(AddressCitySelect, AddressRegionSelect) })
SubscriberAddressCitySelect.addEventListener("change", function () { changeAddressRegion(SubscriberAddressCitySelect, SubscriberAddressRegionSelect) })

function changeAddressRegion(addressCitySelect, addressRegionSelect) {
    addressRegionSelect.options.length = 0;
    addressRegionSelect.add(new Option(text = "請選擇", value = ""));

    let index = addressCitySelect.selectedIndex;
    let regionList = [];

    if (index == 0) //請選擇
    {

    }
    else if (index == 1) //臺北市
    {
        regionList = ["中正區", "大同區", "中山區", "松山區", "大安區", "萬華區", "信義區", "士林區", "北投區", "內湖區", "南港區", "文山區"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })

    }
    else if (index == 2) //新北市
    {
        regionList = ["板橋區", "新莊區", "中和區", "永和區", "土城區", "樹林區", "三峽區", "鶯歌區", "三重區", "蘆洲區", "五股區", "泰山區", "林口區", "八里區", "淡水區", "三芝區", "石門區", "金山區", "萬里區", "汐止區", "瑞芳區", "貢寮區", "平溪區", "雙溪區", "新店區", "深坑區", "石碇區", "坪林區", "烏來區"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 3) //桃園市
    {
        regionList = ["桃園區", "中壢區", "平鎮區", "八德區", "楊梅區", "蘆竹區", "大溪區", "龍潭區", "龜山區", "大園區", "觀音區", "新屋區", "復興區"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 4) //臺中市
    {
        regionList = ["中區", "東區", "南區", "西區", "北區", "北屯區", "西屯區", "南屯區", "太平區", "大里區", "霧峰區", "烏日區", "豐原區", "后里區", "石岡區", "東勢區", "新社區", "潭子區", "大雅區", "神岡區", "大肚區", "沙鹿區", "龍井區", "梧棲區", "清水區", "大甲區", "外埔區", "大安區", "和平區"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 5) //臺南市
    {
        regionList = ["中西區", "東區", "南區", "北區", "安平區", "安南區", "永康區", "歸仁區", "新化區", "左鎮區", "玉井區", "楠西區", "南化區", "仁德區", "關廟區", "龍崎區", "官田區", "麻豆區", "佳里區", "西港區", "七股區", "將軍區", "學甲區", "北門區", "新營區", "後壁區", "白河區", "東山區", "六甲區", "下營區", "柳營區", "鹽水區", "善化區", "大內區", "山上區", "新市區", "安定區"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 6) //高雄市
    {
        regionList = ["楠梓區", "左營區", "鼓山區", "三民區", "鹽埕區", "前金區", "新興區", "苓雅區", "前鎮區", "旗津區", "小港區", "鳳山區", "大寮區", "鳥松區", "林園區", "仁武區", "大樹區", "大社區", "岡山區", "路竹區", "橋頭區", "梓官區", "彌陀區", "永安區", "燕巢區", "田寮區", "阿蓮區", "茄萣區", "湖內區", "旗山區", "美濃區", "內門區", "杉林區", "甲仙區", "六龜區", "茂林區", "桃源區", "那瑪夏區"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 7) //基隆市
    {
        regionList = ["仁愛區", "中正區", "信義區", "中山區", "安樂區", "暖暖區", "七堵區"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 8) //新竹市
    {
        regionList = ["東區", "北區", "香山區"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 9) //嘉義市
    {
        regionList = ["東區", "西區"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 10) //新竹縣
    {
        regionList = ["竹北市", "竹東鎮", "新埔鎮", "關西鎮", "湖口鄉", "新豐鄉", "峨眉鄉", "寶山鄉", "北埔鄉", "芎林鄉", "橫山鄉", "尖石鄉", "五峰鄉"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 11) //苗栗縣
    {
        regionList = ["苗栗市", "頭份市", "竹南鎮", "後龍鎮", "通霄鎮", "苑裡鎮", "卓蘭鎮", "造橋鄉", "西湖鄉", "頭屋鄉", "公館鄉", "銅鑼鄉", "三義鄉", "大湖鄉", "獅潭鄉", "三灣鄉", "南庄鄉", "泰安鄉"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 12) //彰化縣
    {
        regionList = ["彰化市", "員林市", "和美鎮", "鹿港鎮", "溪湖鎮", "二林鎮", "田中鎮", "北斗鎮", "花壇鄉", "芬園鄉", "大村鄉", "永靖鄉", "伸港鄉", "線西鄉", "福興鄉", "秀水鄉", "埔心鄉", "埔鹽鄉", "大城鄉", "芳苑鄉", "竹塘鄉", "社頭鄉", "二水鄉", "田尾鄉", "埤頭鄉", "溪州鄉"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 13) //南投縣
    {
        regionList = ["南投市", "埔里鎮", "草屯鎮", "竹山鎮", "集集鎮", "名間鄉", "鹿谷鄉", "中寮鄉", "魚池鄉", "國姓鄉", "水里鄉", "信義鄉", "仁愛鄉"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 14) //雲林縣
    {
        regionList = ["斗六市", "斗南鎮", "虎尾鎮", "西螺鎮", "土庫鎮", "北港鎮", "林內鄉", "古坑鄉", "大埤鄉", "莿桐鄉", "褒忠鄉", "二崙鄉", "崙背鄉", "麥寮鄉", "臺西鄉", "東勢鄉", "元長鄉", "四湖鄉", "口湖鄉", "水林鄉"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 15) //嘉義縣
    {
        regionList = ["太保市、朴子市、布袋鎮、大林鎮、民雄鄉、溪口鄉、新港鄉、六腳鄉、東石鄉、義竹鄉、鹿草鄉、水上鄉、中埔鄉、竹崎鄉、梅山鄉、番路鄉、大埔鄉、阿里山鄉"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 16) //屏東縣
    {
        regionList = ["屏東市、潮州鎮、東港鎮、恆春鎮、萬丹鄉、長治鄉、麟洛鄉、九如鄉、里港鄉、鹽埔鄉、高樹鄉、萬巒鄉、內埔鄉、竹田鄉、新埤鄉、枋寮鄉、新園鄉、崁頂鄉、林邊鄉、南州鄉、佳冬鄉、琉球鄉、車城鄉、滿州鄉、枋山鄉、霧臺鄉、瑪家鄉、泰武鄉、來義鄉、春日鄉、獅子鄉、牡丹鄉、三地門鄉"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 17) //宜蘭縣
    {
        regionList = ["宜蘭市、頭城鎮、羅東鎮、蘇澳鎮、礁溪鄉、壯圍鄉、員山鄉、冬山鄉、五結鄉、三星鄉、大同鄉、南澳鄉"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 18) //花蓮縣
    {
        regionList = ["花蓮市、鳳林鎮、玉里鎮、新城鄉、吉安鄉、壽豐鄉、光復鄉、豐濱鄉、瑞穗鄉、富里鄉、秀林鄉、萬榮鄉、卓溪鄉"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 19) //臺東縣
    {
        regionList = ["臺東市、成功鎮、關山鎮、長濱鄉、池上鄉、東河鄉、鹿野鄉、卑南鄉、大武鄉、綠島鄉、太麻里鄉、海端鄉、延平鄉、金峰鄉、達仁鄉、蘭嶼鄉"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 20) //澎湖縣
    {
        regionList = ["馬公市、湖西鄉、白沙鄉、西嶼鄉、望安鄉、七美鄉"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 21) //金門縣
    {
        regionList = ["金城鎮、金湖鎮、金沙鎮、金寧鄉、烈嶼鄉、烏坵鄉"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
    else if (index == 22) //連江縣
    {
        regionList = ["南竿鄉、北竿鄉、莒光鄉、東引鄉"];
        regionList.forEach(function (region) {
            addressRegionSelect.add(new Option(region, region));
        })
    }
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





