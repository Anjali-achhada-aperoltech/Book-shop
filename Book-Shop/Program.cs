using Book.Business.DTO;
using Book.Domain.Context;
using Book.Domain.Models;
using Book.Interfaces.Repositiory;
using Book.Interfaces.Services;
using Book.Reposittiory;
using Book.Services;
using Book.UOW;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BookDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoryReposititory, CategoryRepositiory>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBookItemsRepositiory,BookItemsRepostitory>();
builder.Services.AddScoped<IBookItemService, BookItemsService>();
builder.Services.AddScoped<ICartReposititory, CartRepostitory>();
builder.Services.AddScoped<IAboutUsRepostitory, AboutUsRepositiory>();
builder.Services.AddScoped<IAboutUsService, AboutUsService>();
builder.Services.AddScoped<ICartService,CartService>();
builder.Services.AddScoped<IBookLanguageRepostiory, BookLanguageRepository>();
builder.Services.AddScoped<IBookLanguageService, BookLanguageService>();
builder.Services.AddScoped<IOrderHeaderService, OrderHeaderService>();
builder.Services.AddScoped<IOrderHeaderRepositiory, OrderHeaderRepositiory>();
builder.Services.AddScoped<IOrderDetailRepositiory, OrderDetailRepositiory>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("PayementSettings:SecretKey"));
builder.Services.AddIdentity<Applicationuser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddDbContext<Book.Domain.Context.ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.ConfigureApplicationCookie(option =>
{
    option.AccessDeniedPath = "/Account/Login";
    option.LoginPath = "/Account/Login";
    option.LogoutPath = "/Account/Logout";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    option.SlidingExpiration = true;
    option.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter; 
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
StripeConfiguration.ApiKey = builder.Configuration.GetSection("PaymentSettings:SecretKey").Get<string>();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
