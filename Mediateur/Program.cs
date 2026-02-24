using DataAccess.DbContext.Data;
using Mediateur.Interfaces.Repositories;
using Mediateur.Interfaces.Services;
using  Mediateur.Models;
using Mediateur.Repositories;
using Mediateur.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);




var password = Environment.GetEnvironmentVariable("DB_PASSWORD");


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddControllersWithViews();




#region Authentication + Cookies

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter());
});

#endregion

#region Entity Framework

builder.Services.AddDbContext<MediateurContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;

})
.AddEntityFrameworkStores<MediateurContext>()
.AddDefaultTokenProviders();

#endregion

#region Email Sender 

var emailAddress = builder.Configuration["Email:Address"];
var emailPassword = builder.Configuration["Email:Password"];

builder.Services.AddScoped<IEmailSender>(provider =>
    new GmailEmailSender(emailAddress, emailPassword));

#endregion
#region Custom Repositories 

builder.Services.AddScoped<ICategories, CategoriesRepository>();
builder.Services.AddScoped<IItem, ItemRepository>();
builder.Services.AddScoped<IItemImage, ItemImageRepository>();
builder.Services.AddScoped<IItemsType, ItemsTypeRepository>();
builder.Services.AddScoped<IO, ORepository>();
builder.Services.AddScoped<IPages, PagesRepository>();
builder.Services.AddScoped<ISetting, SttingRepository>();
builder.Services.AddScoped<IAccount, AccountRepository>();
#endregion



#region Coustom Services

builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IItemImageService, ItemImageService>();
builder.Services.AddScoped<IItemsTypeService, ItemsTypeService>();
builder.Services.AddScoped<IOService, OService>();
builder.Services.AddScoped<IPagesService, PagesService>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<IAccountService, AccountService>();
#endregion






builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});







builder.Services.AddLogging();

var app = builder.Build();




if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

#region Routing

app.MapControllerRoute(
name: "admin",
pattern: "{area:exists}/{Controller=Home}/{action=Index}");

app.MapControllerRoute(
    name: "default",
    pattern: "{Controller=Home}/{action=Index}/{id?}");

#endregion




app.Run();
