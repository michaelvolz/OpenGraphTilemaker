window.showAlert = (data) => {
    alert(data);

    return true;
};

window.say = (data) => {
    console.dir(data);

    return true;
};

window.getWindowsWidth = () => {
    return window.innerWidth;
};