// Ensure Hyperscript works with htmx-loaded content
document.body.addEventListener('htmx:afterOnLoad', function(evt) {
    if (window._hyperscript) {
        window._hyperscript.processNode(evt.detail.target);
    }
});
