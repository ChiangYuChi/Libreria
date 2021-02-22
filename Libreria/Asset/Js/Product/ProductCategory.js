var bookList = document.querySelectorAll(".book-list");
var searchResult = document.querySelector(".search-result-total");
searchResult.innerHTML = bookList.length;
var sortPriceBtn = document.querySelector(".sort-btn-price");


(function (window) {

    'use strict';

    // class helper functions from bonzo https://github.com/ded/bonzo

    function classReg(className) {
        return new RegExp("(^|\\s+)" + className + "(\\s+|$)");
    }

    // classList support for class management
    // altho to be fair, the api sucks because it won't accept multiple classes at once
    var hasClass, addClass, removeClass;

    if ('classList' in document.documentElement) {
        hasClass = function (elem, c) {
            return elem.classList.contains(c);
        };
        addClass = function (elem, c) {
            elem.classList.add(c);
        };
        removeClass = function (elem, c) {
            elem.classList.remove(c);
        };
    } else {
        hasClass = function (elem, c) {
            return classReg(c).test(elem.className);
        };
        addClass = function (elem, c) {
            if (!hasClass(elem, c)) {
                elem.className = elem.className + ' ' + c;
            }
        };
        removeClass = function (elem, c) {
            elem.className = elem.className.replace(classReg(c), ' ');
        };
    }

    function toggleClass(elem, c) {
        var fn = hasClass(elem, c) ? removeClass : addClass;
        fn(elem, c);
    }

    var classie = {
        // full names
        hasClass: hasClass,
        addClass: addClass,
        removeClass: removeClass,
        toggleClass: toggleClass,
        // short names
        has: hasClass,
        add: addClass,
        remove: removeClass,
        toggle: toggleClass
    };

    // transport
    if (typeof define === 'function' && define.amd) {
        // AMD
        define(classie);
    } else {
        // browser global
        window.classie = classie;
    }

})(window);

(function () {

    var container = document.getElementById('cbp-vm'),
        optionSwitch = Array.prototype.slice.call(container.querySelectorAll('div.cbp-vm-options > a'));

    function init() {
        optionSwitch.forEach(function (el, i) {
            el.addEventListener('click', function (ev) {
                ev.preventDefault();
                _switch(this);
            }, false);
        });
    }

    function _switch(opt) {
        // remove other view classes and any any selected option
        optionSwitch.forEach(function (el) {
            classie.remove(container, el.getAttribute('data-view'));
            classie.remove(el, 'cbp-vm-selected');
        });
        // add the view class for this option
        classie.add(container, opt.getAttribute('data-view'));
        // this option stays selected
        classie.add(opt, 'cbp-vm-selected');
    }

    init();

})();