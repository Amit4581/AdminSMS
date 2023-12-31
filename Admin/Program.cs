//using Admin.Interface.Infrastructure;
//using Admin.Repository.Infrastructure;
//using Admin.Web.Interface.Implementation;
//using Admin.Web.Repository.Implementation;

using Admin.Contract.Interface.Implementation;
using Admin.Contract.Interface.Infrastructure;
using Admin.Services.Repository.Implementation;
using Admin.Services.Repository.Infrastructure;
using Admin.Services.Services.Infrastructure;
using Admin.Web.Models.ChatHub;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllers();

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddTransient<IConnectionFactory, ConnectionFactory>();
builder.Services.AddTransient<IUnitOfWork, SqlUnitOfWork>();
builder.Services.AddTransient<IUserRepositry, UserRepository>();
builder.Services.AddTransient<IMasterDataService, MasterDataService>();

// Example code to set the Secure attribute for cookies in ASP.NET Core

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
    options.HttpOnly = HttpOnlyPolicy.Always;
    options.Secure = CookieSecurePolicy.Always; // Ensure cookies are sent only over HTTPS
});


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Index";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });


var app = builder.Build();


if (!app.Environment.IsDevelopment() || app.Environment.IsProduction())
{

    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseExceptionHandler("/Error");
    //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
    .UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HRMS.API V1");
    });

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(); // Consider specifying CORS policy

app.UseAuthentication();
app.UseAuthorization();


//app.UseEndpoints(endpoints =>
//{
//    //endpoints.MapHub<ChatHub>("/chathub");
//    endpoints.MapAreaControllerRoute(
//     name: "User",
//     areaName: "User",
//     pattern: "User/{controller=Amit}/{action=Index}/{id?}"
//   );
//    endpoints.MapAreaControllerRoute(
//     name: "Masters",
//     areaName: "Masters",
//     pattern: "Masters/{controller=ManageMaster}/{action=Index}/{id?}"
//   );

//        endpoints.MapControllerRoute(
//            name: "examRoute",
//            pattern: "{area:exists}/{controller=MCQ}/{action=Index}/{id?}"
//        );


//    endpoints.MapControllerRoute(
//     name: "default",
//        pattern: "{controller=Account}/{action=Login}/{id?}"
//   );
//});
app.UseEndpoints(endpoints =>
{
    // Map signalR hub if needed
    // endpoints.MapHub<ChatHub>("/chathub");

    endpoints.MapControllers(); // Map attribute-routed controllers

    endpoints.MapControllerRoute(
        name: "User",
        pattern: "{area:exists}/User/{controller=Amit}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "Masters",
        pattern: "{area:exists}/Masters/{controller=ManageMaster}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "examRoute",
        pattern: "{area:exists}/{controller=MCQ}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Login}/{id?}"
    );

    endpoints.MapRazorPages(); // Map Razor Pages if needed
});

// The terminal middleware
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Page not found");
});




app.Run();
