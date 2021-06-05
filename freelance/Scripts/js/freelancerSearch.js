function searchToggle(obj, evt) {
    var container = $(obj).closest('.search-wrapper');
    if (!container.hasClass('active')) {
        container.addClass('active');
        evt.preventDefault();
    }
    else if (container.hasClass('active') && $(obj).closest('.input-holder').length == 0) {
        container.removeClass('active');
        // clear input
        container.find('.search-input').val('');
    }
}

let searchBar = document.getElementById('postsSearch');
let posts = document.querySelectorAll('.card');
searchBar.addEventListener("keyup", function (event) {
    const q = event.target.value.toLowerCase();
    posts.forEach((post) => {
        console.log(post.querySelector('.description p').textContent);
        if (post.querySelector('.description p').textContent.toLowerCase().includes(q) ||
            post.querySelector('.description h1').textContent.toLowerCase().includes(q) ||
            post.querySelector('.description h2').textContent.toLowerCase().includes(q) ||
            post.querySelector('.description h4').textContent.toLowerCase().includes(q)) {
            post.className = 'card';
        } else {
            post.className = 'holeOut';
        }
    });
});
console.log(posts);