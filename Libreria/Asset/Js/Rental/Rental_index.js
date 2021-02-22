$('#myModal').on('shown.bs.modal', function () {
    $('#myInput').trigger('focus')
})

Date.prototype.yyyymmdd = function () {
    var mm = this.getMonth() + 1;
    var dd = this.getDate();
    return [this.getFullYear(),
        '/',
    (mm > 9 ? '' : '0') + mm,
        '/',
    (dd > 9 ? '' : '0') + dd
    ].join('');
};

let calender;
window.onload = () => {
    calender = new Calendar();
    calender.generateDaysOfWeek();
}



//產出展覽開始日期
function generateStartDate(item) {
    let startDate = calender.getPickDateRange().pickStartDate;
    let endDate = calender.getPickDateRange().pickEndDate;
    let indexDate = new Date(startDate.getTime());
    $('option:eq(0)', item).nextAll().remove();
    for (let i = 0; i <= endDate.getDate() - startDate.getDate(); i++) {
        let option = document.createElement("option");
        option.value = indexDate.yyyymmdd();
        option.innerText = `${indexDate.getFullYear()}年${indexDate.getMonth() + 1}月${indexDate.getDate()}日`;
        $(item).append(option)
        indexDate.setDate(indexDate.getDate() + 1);
    }

    $(item).change(function () {
        let endDate = $(item).closest('div').next().find('select[name=end-data]');
        $('option:eq(0)', endDate).nextAll().remove();
        var optsionList = $('option:selected', this).nextAll().clone();
        endDate.append(optsionList);
    });
}   

//產生展覽結束時間
function generateEndTime() {
    let startValue = parseInt(this.value);
    let endSelect = $(this).closest('div').next('div').find('select[name^=end]')[0];
    $('option:eq(0)', endSelect).nextAll().remove();
    for (let i = startValue + 1; i < this.length; i++) {
        let option = document.createElement("option");
        option.value = i;
        option.innerText = this[i].innerText;
        endSelect.appendChild(option);
    }
}
document.getElementById('start').addEventListener("change", generateEndTime);
document.getElementById('start-modal').addEventListener("change", generateEndTime);

// 線上預訂
function bookingOnline() {
    let pickDateRange = calender.getPickDateRange();
    let checkOutTimeContainer = $(this).closest('div').prev();
    let startTime = $('select[name=start-time]', checkOutTimeContainer).find('option:selected').text();
    let endTime = $('select[name=end-time]', checkOutTimeContainer).find('option:selected').text();
    let ExhibitionStartTime = $('select[name=start-data]', checkOutTimeContainer.prev()).find('option:selected').val();
    let ExhibitionEndTime = $('select[name=end-data]', checkOutTimeContainer.prev()).find('option:selected').val();

    function getInput(name, value) {
        return $(document.createElement('input')).attr({
            'type': 'hidden',
            'name': name,
            'value': value,
        });
    }
    let form = $(document.createElement('form')).attr({
        'action': '/Rental/Confirm',
        'method': 'post',
    }).append(getInput('ExhibitionStartTime', ExhibitionStartTime))
        .append(getInput('ExhibitionEndTime', ExhibitionEndTime))
        .append(getInput('StartDate', pickDateRange.pickStartDate.yyyymmdd()))
        .append(getInput('EndDate', pickDateRange.pickEndDate.yyyymmdd()))
        .append(getInput('StartTime', startTime))
        .append(getInput('EndTime', endTime))
    $('body').append(form);
    form.submit();
}
$('a[name=bookingOnlineBtn]').click(bookingOnline);

function Calendar() {
    let currentDate = getFirstDate();
    let container = $('.space__calendar');
    let ymText = $('.calendar__title p', container)[0]; // 顯示 yyyy年MM月 
    let tbody = $('div > table > tbody', container)[0];
    let pickStartDate = null, pickEndDate = null;
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

        for (let weekIndex = 0; weekIndex < 5; weekIndex++) {
            let tr = document.createElement('tr');
            // 補第一天之前空的td 
            let prevDaysOfWeek = currentDate.getDay();
            while (weekIndex == 0 && prevDaysOfWeek > 0) {
                let td = document.createElement('td');
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
                let td = document.createElement('td');
                if (currentDate <= new Date()) {
                    // 不可預約 
                    td.className = 'no-appointment';
                }
                td.innerHTML = currentDate.getDate();
                tr.appendChild(td);

                //選取租借時間
                $(td).data('data', new Date(currentDate.getTime()));
                $(td).click(function () {
                    if (pickStartDate != null && pickEndDate != null && pickStartDate != pickEndDate) {
                        pickStartDate = null;
                        pickEndDate = null;
                        $('td', tbody).each(function () {
                            $('td').removeClass('pick-date');
                        })
                    }

                    pickStartDate = (pickStartDate) ?? $(this).data('data');
                     if(pickStartDate != null) {
                        pickEndDate = $(this).data('data');
                    }
                    if (pickStartDate != null && pickEndDate != null) {
                        $('td', tbody).each(function () {
                            let indexDate = $(this).data('data');
                            if (pickStartDate <= indexDate && indexDate <= pickEndDate) {
                                $(this).addClass('pick-date');
                            }
                        });

                        if (pickStartDate != null && pickEndDate != null && pickStartDate != pickEndDate) {
                            generateStartDate('#exhibition-startDate');
                            generateStartDate('#exhibition-startDateModel');
                            //document.getElementById('exhibition-startDate').addEventListener("change", generateEndTime);
                            //document.getElementById('exhibition-startDateModel').addEventListener("change", generateEndTime);
                        }
                    }
                    $('.space__check-out--data .start-data').text(`${pickStartDate.getFullYear()}年${pickStartDate.getMonth() + 1}月${pickStartDate.getDate()}日`);
                    $('.space__check-out--data .end-data').text(`${pickEndDate.getFullYear()}年${pickEndDate.getMonth() + 1}月${pickEndDate.getDate()}日`);
                    
                })
                
                currentDate.setDate(currentDate.getDate() + 1);
            }
            tbody.appendChild(tr);
            
        }
        
        currentDate.setMonth(currentDate.getMonth() - 1);
        currentDate.setDate(0);
        currentDate.setDate(currentDate.getDate() + 1);
        
    }
    function getPickDateRange() {
        return { pickStartDate, pickEndDate };
    }
    
    this.generateDaysOfWeek = generateDaysOfWeek;
    this.getPickDateRange = getPickDateRange;
}
