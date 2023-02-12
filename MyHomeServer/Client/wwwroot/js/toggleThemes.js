function TurnOnDarkTheme() {
    document.head.innerHTML += '<link id="my-custom-dark-theme" rel="stylesheet" type="text/css" href="css/customDark.css">';
}
function TurnOffDarkTheme() {
    $('#my-custom-dark-theme').remove();
}