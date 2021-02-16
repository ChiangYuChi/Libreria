let previewBtn = document.querySelectorAll(".preview-btn");
let lightbox = document.querySelector(".lightbox-container")
let lightboxItem = document.querySelector(".lightbox-item");
let lightboxClose = document.querySelector(".lightbox-close");
let images = document.querySelectorAll(".book-img");
let imageList = [];
let imageCounter = 0;
let btnLeft = document.querySelector('.btnLeft');
let btnRight = document.querySelector('.btnRight');
previewBtn.forEach(function (item) {
    item.addEventListener("click", function () {
        let img = document.querySelector(".preview-img").src;
        lightboxItem.style.backgroundImage = `url(${img})`;
        lightbox.classList.add("show");
    })
})

lightboxClose.addEventListener("click", function () {
    lightbox.classList.remove("show");
})

images.forEach(function (image) {
    imageList.push(image.src);

})


btnLeft.addEventListener('click', function () {
    imageCounter--;
    if (imageCounter < 0) {
        imageCounter = imageList.length - 1;
    }
    lightboxItem.style.backgroundImage = `url(${imageList[imageCounter]})`;
})

btnRight.addEventListener('click', function () {
    imageCounter++;
    if (imageCounter >= imageList.length) {
        imageCounter = 0;
    }
    lightboxItem.style.backgroundImage = `url(${imageList[imageCounter]})`;
})
