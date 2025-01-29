using GpgTimesheetEmailSender.Application.Services;
using GpgTimesheetEmailSender.Domain.Interfaces;
using GpgTimesheetEmailSender.Infrastructure;
using GpgTimesheetEmailSender.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<TimesheetService>();
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}")
    .WithStaticAssets();


app.Run();
