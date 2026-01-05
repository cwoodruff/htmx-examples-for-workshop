// File upload progress tracking
document.addEventListener('DOMContentLoaded', function() {
    // JavaScript upload form progress
    var form = document.getElementById('form');
    if (form) {
        form.addEventListener('htmx:xhr:progress', function(evt) {
            var progress = document.getElementById('js_progress');
            if (progress) {
                progress.setAttribute('value', evt.detail.loaded / evt.detail.total * 100);
            }
        });
    }

    // Hyperscript upload form progress
    var form2 = document.getElementById('form2');
    if (form2) {
        form2.addEventListener('htmx:xhr:progress', function(evt) {
            var progress = document.getElementById('progress');
            if (progress) {
                progress.setAttribute('value', evt.detail.loaded / evt.detail.total * 100);
            }
        });
    }
});
