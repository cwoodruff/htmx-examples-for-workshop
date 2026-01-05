var builder = WebApplication.CreateBuilder(args);

// Add services to the container.// Add services to the container.
builder.Services.AddHttpClient();
builder.Services
    .AddSingleton<htmx_examples.Pages.ClickToEdit.IContactService, htmx_examples.Pages.ClickToEdit.ContactService>();
builder.Services
    .AddSingleton<htmx_examples.Pages.BulkUpdate.IContactService, htmx_examples.Pages.BulkUpdate.ContactService>();
builder.Services
    .AddSingleton<htmx_examples.Pages.DeleteRow.IContactService, htmx_examples.Pages.DeleteRow.ContactService>();
builder.Services
    .AddSingleton<htmx_examples.Pages.EditRow.IContactService, htmx_examples.Pages.EditRow.ContactService>();
builder.Services.AddRazorPages();
builder.Services.AddRazorComponents();
builder.Services.AddAntiforgery();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Add security headers
app.Use(async (context, next) =>
{
    // Content Security Policy - restricts resource loading to prevent XSS
    context.Response.Headers.Append("Content-Security-Policy",
        "default-src 'self'; " +
        "script-src 'self'; " +
        "style-src 'self' 'unsafe-inline'; " +
        "img-src 'self' data:; " +
        "font-src 'self'; " +
        "connect-src 'self'; " +
        "frame-ancestors 'none'; " +
        "base-uri 'self'; " +
        "form-action 'self'");

    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Append("X-Frame-Options", "DENY");
    context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");

    // Permissions Policy - restricts browser features
    context.Response.Headers.Append("Permissions-Policy",
        "geolocation=(), microphone=(), camera=()");

    await next();
});

app.UseRouting();

app.UseAntiforgery();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();