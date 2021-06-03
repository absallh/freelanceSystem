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
let posts = document.querySelectorAll('li.d-flex');
searchBar.addEventListener("keyup", function (event) {
    const q = event.target.value.toLowerCase();
    posts.forEach((post) => {
        console.log(post.querySelector('div div p').textContent);
        if (post.querySelector('div div p').textContent.toLowerCase().startsWith(q)) {
            post.className = 'd-flex justify-content-between';
        } else {
            post.className = 'holeOut';
        }
    });
});
console.log(posts);