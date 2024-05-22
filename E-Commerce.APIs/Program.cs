using E_Commerce.BL;
using  E_Commerce.DAL;
using E_Commerce.DAL.Data.Context;
using E_Commerce.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace E_Commerce.APIs;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddBLServices();
        builder.Services.AddDALServices(builder.Configuration);


        builder.Services.AddIdentity<User, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
        })
       .AddEntityFrameworkStores<MyAppContext>()
       .AddDefaultTokenProviders();

        #region Authentication
        builder.Services.AddAuthentication(options =>
        {
            // configure used authentication
            options.DefaultAuthenticateScheme = "MyDefault";
            options.DefaultChallengeScheme = "MyDefault";
        })
        //define the authentication scheme
        .AddJwtBearer("MyDefault", options =>
        {
            var secretKey = builder.Configuration.GetValue<string>("SecretKey")!;
            var keyInBytes = Encoding.ASCII.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(keyInBytes);

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = key,

            };
        });


        #endregion

        #region Authorization
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminsOnly", policy => policy.RequireRole("Admin"));
        });
        #endregion

        static async Task EnsureRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            await EnsureRoles(services);
        }

        app.UseCors(options =>
        {
            options.AllowAnyOrigin();
            options.AllowAnyMethod();
            options.AllowAnyHeader();
        });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
