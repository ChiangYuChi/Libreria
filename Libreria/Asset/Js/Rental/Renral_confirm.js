﻿// Example starter JavaScript for disabling form submissions if there are invalid fields
(function () {
    'use strict';
    window.addEventListener('load', function () {
        // Fetch all the forms we want to apply custom Bootstrap validation styles to
        let forms = document.getElementsByClassName('needs-validation');
        // Loop over them and prevent submission
        let validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();

//上傳圖片顯示
$('#upload-image input').change(function () {
    let file = $('#upload-image input')[0].files[0];
    let reader = new FileReader;
    reader.onload = function (e) {
        $('#upload-image label').html('');
        $('#upload-image').addClass('upload-image');
        $('#upload-image').css('text-align', 'center');
        $('.fixd-narrow-only').css('bottom', '-890px');
        $('#upload-image label').css('background-image', 'url("' + e.target.result + '")');
    };
    reader.readAsDataURL(file);
});

function ConfirmReservation() {
    let name = `${$('#last-name').val()} ${$('#first-name').val()}`
    let phone = $('#phoneNumber').val();
    let email = $('#email').val();
    let organizer = $('#organizer').val();
    let fare = $('#fare').val();
    let introduction = $('#introduction').val();
    let pic = $('#pic').val();
    let exName = $('#exName').val();
    let startDate = $('#StartDate').val();
    let endDate = $('#EndDate').val();
    let exhibitionStartTime = $('#ExhibitionStartTime').val();
    let exhibitionEndTime = $('#ExhibitionEndTime').val();
    $.ajax({
        method: "post",
        url: "/rental/ConfirmBooling",
        data: {
            EndDate: endDate,
            StartDate: startDate,
            ExhibitionStartTime: exhibitionStartTime,
            ExhibitionEndTime: exhibitionEndTime,
            ExCustomerName: name,
            ExCustomerPhone: phone,
            ExCustomerEmail: email,
            ExhibitionIntro: organizer,
            MasterUnit: introduction,
            ExhibitionPrice: pic,
            ExPhoto: fare,
            ExName: exName
        }
    }).done(function (msg) {
        alert("預定完成!"); 
    });
}
$('#confirm').click(ConfirmReservation)