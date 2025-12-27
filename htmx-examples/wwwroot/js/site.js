// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.body.addEventListener('htmx:configRequest', (event) => {
    let tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');
    if (tokenElement) {
        event.detail.parameters['__RequestVerificationToken'] = tokenElement.value;
    }
});

// Call the dataTables jQuery plugin
$(document).ready(function () {
    $('#dataTable').DataTable();
    $('#dataTableInvoices').DataTable();
    $('#dataTableInvoiceLines').DataTable();
});

$(document).ready(function () {
    $("#dataTableInvoices tbody tr").click(function () {
        var selected = $(this).hasClass("highlight");
        $("#dataTableInvoices tr").removeClass("highlight");
        if (!selected)
            $(this).addClass("highlight");
    });
});