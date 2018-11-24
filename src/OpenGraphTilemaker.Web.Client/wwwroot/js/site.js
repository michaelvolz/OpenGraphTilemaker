"use strict";

window.blazorDemo = {
    showAlert: (data) => {
        alert(data);

        return true;
    },

    say: (data) => {
        console.dir(data);

        return true;
    },

    getWindowWidth: () => {
        return window.innerWidth;
    },

    focusElement: function(element) {
        element.focus();

        return true;
    },

    onInit: (dotnetClassInstance) => {
        console.info("JS onInit!");
    },

    onParametersSet: (dotnetClassInstance) => {
        console.info("JS onAfterRender!");

        var resizeName = "resize";
        var resizer = function() {
            if (typeof window.resizeTimer !== "undefined") {
                clearTimeout(window.resizeTimer);
            }

            window.resizeTimer = setTimeout(function() {
                    dotnetClassInstance.invokeMethodAsync("FromJSWindowResizedAsync", window.innerWidth)
                        .then(data => { console.log(data); });
                },
                250);
        };

        window.removeEventListener(resizeName, resizer, true);
        window.addEventListener(resizeName, resizer, true);
    }, 
    
    onAfterRender: (dotnetClassInstance) => {
        console.info("JS onAfterRender!");
    }
};