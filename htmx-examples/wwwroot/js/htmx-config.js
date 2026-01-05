// htmx Configuration
htmx.logAll();

document.addEventListener("htmx:configRequest", (evt) => {
    // Only add antiforgery token to headers for non-multipart requests
    // Multipart forms automatically include the token in form data
    const contentType = evt.detail.headers['Content-Type'];
    if (!contentType || !contentType.includes('multipart/form-data')) {
        let token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
        if (token) {
            evt.detail.headers['RequestVerificationToken'] = token;
        }
    }
});
