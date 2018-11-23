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

    getWindowsWidth: () => {
        return window.innerWidth;
    },

    focusElement: function(element) {
        element.focus();
    },

    initialize: (dotnetClassInstance) => {
        console.log("JS initialize start!");

        var leftMouseButtonOnlyDown = false;

        function setLeftButtonState(e) {
            leftMouseButtonOnlyDown = e.buttons === undefined
                ? e.which === 1
                : e.buttons === 1;
        }

        document.body.onmousedown = setLeftButtonState;
        document.body.onmousemove = setLeftButtonState;
        document.body.onmouseup = setLeftButtonState;

        var resizeName = "resize";
        var resizer = function() {
            if (typeof window.resizeTimer !== "undefined") {
                clearTimeout(window.resizeTimer);
            }

            window.resizeTimer = setTimeout(function() {
                    // Run code here, resizing has "stopped"
                    dotnetClassInstance.invokeMethodAsync("WindowResizedAsync", window.innerWidth)
                        .then(data => {
                            console.log(data);
                        });
                },
                250);
        };

        window.removeEventListener(resizeName, resizer, true);
        window.addEventListener(resizeName, resizer, true);

        console.log("JS initialize finished!");
    }
};