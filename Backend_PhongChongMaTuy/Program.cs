using Backend_PhongChongMaTuy.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend_PhongChongMaTuy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ BẮT BUỘC: Lắng nghe địa chỉ toàn cục trong Docker
            builder.WebHost.UseUrls("http://0.0.0.0:80");

            builder.Services.AddDbContext<DrugPreventionDBContext>(options =>
                options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnectionStringDB"]));

            builder.Services.AddScoped<JwtService>();
            builder.Services.AddScoped<AuthService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("https://songkhoemanh-website.vercel.app")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors("AllowFrontend");

            // ✅ Luôn bật Swagger mọi môi trường để test nhanh
            app.UseSwagger();
            app.UseSwaggerUI();

            // ❌ Comment HTTPS redirection khi dùng Docker
            // app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
