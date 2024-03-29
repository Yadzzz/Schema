using DataAccessLibrary.Context;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Schema.Authentication;
using Schema.Calendar;
using Schema.Services;
using Serilog;

namespace Schema
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<ProtectedSessionStorage>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddSingleton<UserAccountService>();
            builder.Services.AddScoped<PageNavigationService>();
            builder.Services.AddTransient<CalendarService>();
            //builder.Services.AddTransient<BevakningContext>();
            builder.Services.AddDbContext<BevakningContext>(ServiceLifetime.Transient);
            builder.Services.AddTransient<UsersService>();
            builder.Services.AddTransient<BookingsService>();
            //builder.Services.AddTransient<SessionService>();
            builder.Services.AddScoped<UserDataService>();
            builder.Services.AddTransient<AvailabilityService>();
            builder.Services.AddTransient<NotificationService>();
            builder.Services.AddTransient<BookingRequestsService>();
            builder.Services.AddTransient<EmailService>();
            builder.Services.AddTransient<JobsService>();
            builder.Services.AddTransient<ScheduleFiltersService>();

            builder.Services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });

            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.File(@"logs/log.txt", shared: true)
            .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}