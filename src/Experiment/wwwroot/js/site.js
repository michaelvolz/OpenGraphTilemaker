window.exampleJsFunctions = {
    showPrompt: function (text) {
        return prompt(text, 'Type your name here');
    },
    displayWelcome: function (welcomeMessage) {
        document.getElementById('welcome').innerText = welcomeMessage;
    },
    returnArrayAsyncJs: function () {
        DotNet.invokeMethodAsync('BlazorSample', 'ReturnArrayAsync')
            .then(data => {
                data.push(4);
                console.log(data);
            });
    },
    sayHello: function (dotnetHelper) {
        return dotnetHelper.invokeMethodAsync('SayHello')
            .then(r => console.log(r));
    }
};

window.blazorDemo = {
    showAlert: (data) => {
        alert(data);

        return true;
    },

    navigateTo: (url) => {
        window.location.href = url;
    },

    say: (data) => {
        console.dir(data);

        return true;
    },

    getWindowWidth: () => {
        return window.innerWidth;
    },

    focusElement: function (element) {
        element.focus();

        return true;
    },

    initializeWindowResizeEvent: () => {
        console.info("JS initializeWindowResizeEvent!");

        var onResize = "resize";
        var resizer = function () {
            if (typeof window.resizeTimer !== "undefined") {
                clearTimeout(window.resizeTimer);
            }

            window.resizeTimer = setTimeout(function () {
                console.info("JS resizeTimer timeout!");

                var win = { width: window.innerWidth, height: window.innerHeight };

                window.DotNet.invokeMethodAsync("Experiment", "FromJSWindowResizedAsync", win)
                    .then(data => { console.log(data); })
                    .catch(err => { console.error(err); });
            }, 250);
        };

        window.removeEventListener(onResize, resizer, true);
        window.addEventListener(onResize, resizer, true);
    }
};