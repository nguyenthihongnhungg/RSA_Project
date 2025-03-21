using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using RSA_UI.Models.Entity;
using RSA_UI.Repositories;
using RSA_UI.Repositories.Context;
using RSA_UI.Repositories.RSA;
using RSA_UI.Services;

namespace RSA_UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services
                .AddDbContextPool<RsaContext>(options => 
                    options.UseSqlServer(builder.Configuration.GetConnectionString("TruongDH")));
            
            builder.Services.AddScoped<IRepository<Company>, CompanyRepository>();
            builder.Services.AddScoped<IService<Company>, CompanyService>();


            //File upload limit
            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = int.MaxValue;
            });
            builder.Services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = 104857600; // 100MB
            });

            //Session
            builder.Services.AddSession(configure =>
            {
                configure.IdleTimeout = TimeSpan.FromHours(3);
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<RsaContext>();
                context.Database.EnsureCreated();
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

            app.UseAuthorization();
            
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
