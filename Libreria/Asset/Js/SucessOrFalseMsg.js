
window.onload= () => {
    window.setTimeout(
        function () {
            var dom = document.getElementById("Message");
            dom.style.transition = '.5s';
            dom.style.opacity = "0";
            dom.style.visibility = "hidden";
        }, 1250)
}