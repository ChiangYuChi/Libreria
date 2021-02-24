
$('#inquire').click(function(){ 
    
    $('.search__list').slideToggle('slow');
});


$('.search__list li').click(function(){
    $('.search__title p').html( $(this).text());
    if($(this).text() == "六個月內訂單(含出貨後)"){
        $('.content__title em').text("所有訂單")
    }
    else{
        $('.content__title em').html( $(this).text());
    }
    $('.search__list').slideToggle('hide');
});


//$('.order__inquire label').click(function () {
//    if ($(this).text() == "六個月內訂單" && false) {
//        $('.content__title em').text("所有訂單")
        
//    }
//    else if($(this).html() == "&nbsp;"){
//        $('.content__title em').text("訂單查詢")
        

//    }
//    else{
//        $('.content__title em').html( $(this).text());
//    }
//});


$('input[name=transa-ctionid]:text').click(function(){
    $('.content__title em').text("訂單查詢")
    document.getElementById("transa-ctionid_radio").click();
})



