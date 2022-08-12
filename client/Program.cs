using System.Globalization;
using client;
using client.Pages;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AddFolderRouteModelConvention("/", model =>
        {
            foreach (var selector in model.Selectors)
            {
                selector.AttributeRouteModel.Order = -1;
                selector.AttributeRouteModel.Template = AttributeRouteModel.CombineTemplates("{culture?}", selector.AttributeRouteModel.Template);
            }
        });
    });
builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en"),
        new CultureInfo("fr")
    };
    opt.DefaultRequestCulture = new RequestCulture("fr");
    opt.SupportedCultures = supportedCultures;
    opt.SupportedUICultures = supportedCultures;
    opt.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider()
    {
        RouteDataStringKey = "culture",
        UIRouteDataStringKey = "culture",
        Options = opt
    });
});

builder.Services.Configure<RouteOptions>(opt =>
{
    opt.LowercaseUrls = true;
    opt.AppendTrailingSlash = true;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var options = ((IApplicationBuilder)app).ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

var opt1 = new RewriteOptions().Add(new FirstLoadRewriteRule());
app.UseRewriter(opt1);

app.UseAuthorization();

app.MapRazorPages();

app.Run();
