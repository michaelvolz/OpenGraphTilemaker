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

    onInit: () => {
        console.info("JS onInit!");
    },

    onParametersSet: () => {
        console.info("JS onParametersSet!");

        var resizeName = "resize";
        var resizer = function() {
            if (typeof window.resizeTimer !== "undefined") {
                clearTimeout(window.resizeTimer);
            }

            window.resizeTimer = setTimeout(function() {
                    var win = { Width: window.innerWidth, Height: window.innerHeight };
                    DotNet.invokeMethodAsync("OpenGraphTilemaker.Web.Client", "FromJSWindowResizedAsync", win)
                        .then(data => { console.log(data); });
                },
                250);
        };

        window.removeEventListener(resizeName, resizer, true);
        window.addEventListener(resizeName, resizer, true);
    },

    onAfterRender: () => {
        console.info("JS onAfterRender!");
    }
};