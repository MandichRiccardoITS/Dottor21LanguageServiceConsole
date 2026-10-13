using Azure;
using Azure.AI.TextAnalytics;
using BlazorWebApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<TextAnalyticsClient>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();

    var resourceUrl = new Uri(configuration[
        "text_analyzer_dottor:resource_url"
    ]!);

    var apiKey = new AzureKeyCredential(configuration[
        "text_analyzer_dottor:api_key"
    ]!);

    return new TextAnalyticsClient(resourceUrl, apiKey);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
