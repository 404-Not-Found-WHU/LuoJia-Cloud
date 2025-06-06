using TripWebLast.Data;
using TripWebLast.ServiceOnChanger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 添加数据库上下文
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 修改后的身份认证服务配置 - 添加中文错误描述和密码策略
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // 密码策略配置
    options.Password.RequireDigit = true;          
    options.Password.RequireLowercase = true;      
    options.Password.RequireNonAlphanumeric = true; 
    options.Password.RequireUppercase = true;     
    options.Password.RequiredLength = 6;           
    options.Password.RequiredUniqueChars = 1;      
    // 用户配置
    options.User.RequireUniqueEmail = true;        // 要求唯一邮箱
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddErrorDescriber<ChineseIdentityChanger>() // 添加中文错误描述
.AddDefaultTokenProviders();

// 添加其他服务
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(options => {
    options.Conventions.AuthorizeFolder("/Account/Manage");
    options.Conventions.AuthorizePage("/Account/Logout");
});

var app = builder.Build();

// 配置中间件管道
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();