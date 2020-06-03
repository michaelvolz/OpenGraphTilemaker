'use strict';

window.exampleJsFunctions = {
    showPrompt: function showPrompt(text) {
        return prompt(text, 'Type your name here');
    },
    displayWelcome: function displayWelcome(welcomeMessage) {
        document.getElementById('welcome').innerText = welcomeMessage;
    },
    returnArrayAsyncJs: function returnArrayAsyncJs() {
        DotNet.invokeMethodAsync('BlazorSample', 'ReturnArrayAsync').then(function (data) {
            data.push(4);
            console.log(data);
        });
    },
    sayHello: function sayHello(dotnetHelper) {
        return dotnetHelper.invokeMethodAsync('SayHello').then(function (r) {
            return console.log(r);
        });
    }
};

window.blazorDemo = {
    showAlert: function showAlert(data) {
        alert(data);

        return true;
    },

    navigateTo: function navigateTo(url) {
        window.location.href = url;
    },

    say: function say(data) {
        console.dir(data);

        return true;
    },

    getWindowWidth: function getWindowWidth() {
        return window.innerWidth;
    },

    focusElement: function focusElement(element) {
        element.focus();

        return true;
    },

    initializeWindowResizeEvent: function initializeWindowResizeEvent() {
        console.info("JS initializeWindowResizeEvent!");

        var onResize = "resize";
        var resizer = function resizer() {
            if (typeof window.resizeTimer !== "undefined") {
                clearTimeout(window.resizeTimer);
            }

            window.resizeTimer = setTimeout(function () {
                console.info("JS resizeTimer timeout!");

                var win = { width: window.innerWidth, height: window.innerHeight };

                window.DotNet.invokeMethodAsync("Experiment", "FromJSWindowResizedAsync", win).then(function (data) {
                    console.log(data);
                })['catch'](function (err) {
                    console.error(err);
                });
            }, 250);
        };

        window.removeEventListener(onResize, resizer, true);
        window.addEventListener(onResize, resizer, true);
    }
};

