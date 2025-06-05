using TripWebLast.Data;
using TripWebLast.ServiceOnChanger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ������ݿ�������
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// �޸ĺ�������֤�������� - ������Ĵ����������������
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // �����������
    options.Password.RequireDigit = true;          
    options.Password.RequireLowercase = true;      
    options.Password.RequireNonAlphanumeric = true; 
    options.Password.RequireUppercase = true;     
    options.Password.RequiredLength = 6;           
    options.Password.RequiredUniqueChars = 1;      
    // �û�����
    options.User.RequireUniqueEmail = true;        // Ҫ��Ψһ����
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddErrorDescriber<ChineseIdentityChanger>() // ������Ĵ�������
.AddDefaultTokenProviders();

// �����������
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(options => {
    options.Conventions.AuthorizeFolder("/Account/Manage");
    options.Conventions.AuthorizePage("/Account/Logout");
});

var app = builder.Build();

// �����м���ܵ�
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