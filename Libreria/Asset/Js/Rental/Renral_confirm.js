// Example starter JavaScript for disabling form submissions if there are invalid fields
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



$(document).ready(function () {

    $.validator.addMethod("isChcek", function (value, element) {
        return $(element).is(':checked');
    }, '請勾選我同意');
    

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






