using Microsoft.EntityFrameworkCore;
using Model;
using Service.RoleService;

namespace ShareMoney
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ShareMoneyContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Thêm các service cần thiết dưới đây
            builder.Services.AddScoped<IRoleService, RoleService>();


            // Thêm các dịch vụ cần thiết cho Session
            builder.Services.AddDistributedMemoryCache(); // Lưu Session trong bộ nhớ
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(300); // Thời gian hết hạn của Session
                options.Cookie.HttpOnly = true; // Cookie chỉ có thể được truy cập từ phía server
                options.Cookie.IsEssential = true; // Cần thiết cho ứng dụng hoạt động
            });



            var app = builder.Build();

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
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
