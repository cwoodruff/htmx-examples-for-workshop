// Selects page functionality
document.addEventListener('DOMContentLoaded', function() {
    var modelsSelect = document.getElementById('models');
    if (modelsSelect) {
        modelsSelect.addEventListener('change', function() {
            if(this.value) {
                alert('You selected: ' + this.value);
            }
        });
    }
});
