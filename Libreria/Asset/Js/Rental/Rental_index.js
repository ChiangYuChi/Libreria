﻿$('#myModal').on('shown.bs.modal', function () {
    $('#myInput').trigger('focus')
})

window.onload = () => {
    let calender = new Calendar();
    calender.generateDaysOfWeek();
}

//選擇展覽時間
let startTime = document.querySelectorAll('[name = "start-time"]');
let endTime = document.querySelectorAll('[name = "end-time"]');
let startValue, endValue;
startTime.forEach(x => {
    x.addEventListener("change", function () {
        startValue = x.value;
    })
});
endTime.forEach(y => {
    y.addEventListener("change", function () {
        endValue = y.value;
        if (endValue <= startValue) {
            y[endValue].setAttribute('disabled', true)
            y[endValue].className = 'cannot-select';
        }
    })
});



function Calendar() {
    let currentDate = getFirstDate();
    let container = $('.space__calendar');
    let ymText = $('.calendar__title p', container)[0]; // 顯示 yyyy年MM月 
    let tbody = $('div > table > tbody', container)[0];
    var pickStartDate = null, pickEndDate = null;
    // 上個月 
    $('.back', container).click(function () {
        currentDate.setMonth(currentDate.getMonth() - 1);
        generateDaysOfWeek();
    });
    // 下個月 
    $('.next', container).click(function () {
        currentDate.setMonth(currentDate.getMonth() + 1);
        generateDaysOfWeek();
    });
    function getFirstDate() {
        // 本月份第一天 
        let firstDate = new Date();
        firstDate.setDate(0);
        firstDate.setDate(firstDate.getDate() + 1);
        return firstDate;
    }
    function generateDaysOfWeek() {
        // 產生日期依據週數存放
        tbody.innerHTML = '';
        ymText.innerText = `${currentDate.getFullYear()}年${currentDate.getMonth() + 1}月`;

        for (let weekIndex = 0; weekIndex < 6; weekIndex++) {
            let tr = document.createElement('tr');
            // 補第一天之前空的td 
            let prevDaysOfWeek = currentDate.getDay();
            while (weekIndex == 0 && prevDaysOfWeek > 0) {
                var td = document.createElement('td');
                td.innerHtml = '&emsp;';
                td.className = 'no-appointment';
                tr.appendChild(td);
                prevDaysOfWeek--;
            }
            for (let week = currentDate.getDay(); week < 7; week++) {
                if (weekIndex != 0 && currentDate.getDate() == 1) {
                    // 計算到下個月第一天,則跳過 
                    continue;
                }
                var td = document.createElement('td');
                if (currentDate <= new Date()) {
                    // 不可預約 
                    td.className = 'no-appointment';
                }
                td.innerHTML = currentDate.getDate();
                tr.appendChild(td);
                currentDate.setDate(currentDate.getDate() + 1);
            }
            tbody.appendChild(tr);
        }
        currentDate.setMonth(currentDate.getMonth() - 1);
        currentDate.setDate(0);
        currentDate.setDate(currentDate.getDate() + 1);
    }
    this.generateDaysOfWeek = generateDaysOfWeek;
}

