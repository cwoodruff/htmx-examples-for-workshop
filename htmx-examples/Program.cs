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

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Content-Security-Policy",
        "default-src 'self'; " +
        "script-src 'self' 'unsafe-inline' 'unsafe-eval' https://unpkg.com; " +
        "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com; " +
        "font-src 'self' https://fonts.gstatic.com; " +
        "img-src 'self' data:; " +
        "connect-src 'self' https://unpkg.com;");
    await next();
});

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();