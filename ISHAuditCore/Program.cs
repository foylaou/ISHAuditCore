using ISHAuditCore.Context;
using ISHAuditCore.Models;

var builder = WebApplication.CreateBuilder(args);

// 加入 Session 服務
builder.Services.AddDistributedMemoryCache(); // 使用內存緩存來存儲會話數據
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // 設置會話過期時間
    options.Cookie.HttpOnly = true; // 設置 HttpOnly cookie 安全屬性
    options.Cookie.IsEssential = true; // 在 GDPR 合規的情況下，設置此 cookie 是必要的
});
// 註冊 IHttpContextAccessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISHAuditDbcontext>();
// ISHADB
builder.Services.AddScoped<Authority>();
// ISHA 權限
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<Codes>(); // 如果 Codes 是每次請求一個實例，使用 Scoped 生命週期

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 啟用 Session 中介軟件
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// 初始化 SessionHelper
SessionHelper.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());
app.Run();