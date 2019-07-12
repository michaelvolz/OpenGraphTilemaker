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

    focusElement: function(element) {
        element.focus();

        return true;
    },

    initializeWindowResizeEvent: () => {
        console.info("JS initializeWindowResizeEvent!");

        var onResize = "resize";
        var resizer = function() {
            if (typeof window.resizeTimer !== "undefined") {
                clearTimeout(window.resizeTimer);
            }

            window.resizeTimer = setTimeout(function() {
                    console.info("JS resizeTimer timeout!");

                    var win = { Width: window.innerWidth, Height: window.innerHeight };
                    window.DotNet.invokeMethodAsync("OpenGraphTilemaker.Web.Client", "FromJSWindowResizedAsync", win)
                        .then(data => { console.log(data); })
                        .catch(err => { console.error(err); });
                },
                250);
        };

        window.removeEventListener(onResize, resizer, true);
        window.addEventListener(onResize, resizer, true);
    }
};