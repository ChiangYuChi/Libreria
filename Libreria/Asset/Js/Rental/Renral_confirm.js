//檢視表單是否存在無效字段
(function () {
    'use strict';
    window.addEventListener('load', function () {
        // 提取要驗證的所有表單
        let forms = document.getElementsByClassName('needs-validation');

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



$(document).ready(function () {

    $.validator.addMethod("isChcek", function (value, element) {
        return $(element).is(':checked');
    }, '請勾選我同意');

    $.validator.addMethod("accept", function (value, element) {
        var validExts = $(element).attr('DataValExtension').split(',');

        var fileExt = element.value;
        fileExt = fileExt.substring(fileExt.lastIndexOf('.') + 1);
        if (validExts.indexOf(fileExt) < 0) {
            
            return false;
        }
        else return true;
    },'這不是圖片檔格式!');

    $('#confirm').click(function () {
        if ($('#confirmForm').valid()) {
            $('#confirmForm').submit();
        }
    });

})



//上傳圖片顯示
$('#upload-image input').change(function () {
    let file = $('#upload-image input')[0].files[0];
    let reader = new FileReader;
    reader.onload = function (e) {
        $('#upload-image label').html('');
        $('#upload-image').addClass('upload-image');
        $('#upload-image').css('text-align', 'center');
        $('.fixd-narrow-only').css('bottom', '-840px');
        $('#upload-image label').toggleClass('changed');
        $('#upload-image label').css('background-image', 'url("' + e.target.result + '")');
    };
    reader.readAsDataURL(file);
});






