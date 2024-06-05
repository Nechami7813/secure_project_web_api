using FirstProject;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.user;
using Repository.product;

using Service.user;
using Service.product;
using Repository.category;
using Service.category;
using Repository.order;
using NLog.Web;
using Service;
using FirstProject.middlewares;
using PresidentsApp.Middlewares;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();

builder.Services.AddDbContext<Product326075108Context>(option => option.UseSqlServer("Data Source=srv2\\PUPILS;Initial Catalog=Product_326075108;Trusted_Connection=True;TrustServerCertificate=True"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("key").Value);

builder.Services
    .AddAuthentication(option =>
    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme

   )
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents()
        {

            //get cookie value
            OnMessageReceived = context =>
            {
                var a = "";
                context.Request.Cookies.TryGetValue("X-Access-Token", out a);
                context.Token = a;
                return Task.CompletedTask;
            }
        };
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
        };
    });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseNLog();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseErrorHandlingMiddleware();


// Configure the HTTP request pipeline.
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRatingMiddleware();

app.MapControllers();

app.Run();
