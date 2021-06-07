const btn = document.querySelector('.submit-button');
const form = document.querySelector('.add-product');
const msg = document.querySelector('#success');

btn.addEventListener('click', () => {
    console.log('fasdfasdf');
    form.classList.add('boingOutDown');
    msg.classList.add('boingInUp');
    msg.classList.add('pointerEvent');
});