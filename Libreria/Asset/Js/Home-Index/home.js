$(".slider").slick({
    centerMode: true,
    centerPadding: '0px',
    slidesToShow: 3,
    speed: 1000,
    autoplay: true,
    arrows: false,
    dots: true,
    responsive: [
        {
            breakpoint: 1100,
            settings: {
                arrows: false,
                centerMode: true,
                centerPadding: '100px',
                slidesToShow: 2,
                speed: 1000,
                autoplay: true,
            }
        },
        {
            breakpoint: 1024,
            settings: {
                arrows: false,
                centerMode: true,
                centerPadding: '0px',
                slidesToShow: 2,
                speed: 1000,
                autoplay: true,
            }
        },
        {
            breakpoint: 480,
            settings: {
                arrows: false,
                centerMode: true,
                centerPadding: '0px',
                slidesToShow: 1,
                speed: 1000,
                autoplay: true,
                dots: true
            }
        }
    ]
});
let mediaQueryList = window.matchMedia("(min-width:992px)");
        function screenTest(x) {
            if (x.matches) {
                document.getElementById("navbarToggler").classList.remove("show");
            }
        }
        mediaQueryList.addListener(screenTest);
