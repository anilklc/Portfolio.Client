let menu = document.querySelector('#menu-bar');
let navbar = document.querySelector('.menu');

menu.addEventListener('click', () => {
    menu.classList.toggle('fa-times');
    navbar.classList.toggle('active');    
});
