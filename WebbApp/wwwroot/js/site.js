// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function goBack() {
    window.history.back();
}


const validateName = (event) => {
    const regEx = /^[a-zA-Z -]+$/;
/*    console.log(event);*/

    if (!regEx.test(event.target.value)) {
        event.target.nextElementSibling.innerText = "Endast bokstäver tack!";
    }
    else {
        event.target.nextElementSibling.innerText = "";
    }
}

const validateEmail = (event) => {
    const regExEmail = /^[\w\u0021-\u0039\u003b-\u007e]+\u0040[\w.-]+\.[\w-]+$/;

    if (!regExEmail.test(event.target.value)) {
        event.target.nextElementSibling.innerText = "Ange giltig email tack!";
    }
    else {
        event.target.nextElementSibling.innerText = "";
    }
}
const validateMessage = (event) => {


    if (event.target.value == "") {
        document.getElementById("messageError").innerText = "Ange ett meddelande tack";
    }
    else {
        document.getElementById("messageError").innerText = "";
    }
}
const validateNumber = (event) => {
    const regExNumber = `\d+`;

    if (!regExNumber.Test(event.target.value)) {
        event.target.nextElementSibling.innerText = "Endast nummer tack!";
    }
    else {
        event.target.nextElementSibling.innerText = "";
    }

}