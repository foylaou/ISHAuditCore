using System.Text;
using ISHAuditCore.Models;
using ISHAuditCore.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
// 添加資料庫上下文到依賴注入容器
var connectionString = Environment.GetEnvironmentVariable("ISHAuditDatabase") 
                       ?? builder.Configuration.GetConnectionString("ISHAuditDatabase");

builder.Services.AddDbContext<ISHAuditDbcontext>(options =>
    options.UseSqlServer(connectionString));

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(443, listenOptions =>
    {
        // 配置 Kestrel 使用 HTTP/3
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
        listenOptions.UseHttps(); // 使用 HTTPS，HTTP/3 需要 HTTPS
    });
});
IConfiguration configuration = builder.Configuration;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["JWT_Auth:ValidIssuer"], // 從配置中讀取 ValidIssuer
            ValidAudience = configuration["JWT_Auth:ValidAudience"], // 設置接受的 Audience
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT_Auth:SecurityKey"]!)) // 從配置中讀取 SecretKey
        };
    });

// 檢查或創建日誌目錄
var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs");
if (!Directory.Exists(logDirectory))
{
    Directory.CreateDirectory(logDirectory);
}
// 配置 Serilog，並將日誌寫入文件
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose() // 設置記錄最小層級，Verbose 會記錄所有類型的日誌
    .WriteTo.Console() // 輸出到控制台
    .WriteTo.File($"logs/ISHA-LOG-.txt", rollingInterval: RollingInterval.Day) // 每天創建一個新文件
    .CreateLogger();

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
// ISHA_DB
builder.Services.AddScoped<ISHAuditDbcontext>();
// ISHA 權限
builder.Services.AddScoped<Authority>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<Codes>(); // 如果 Codes 是每次請求一個實例，使用 Scoped 生命週期
builder.Services.AddScoped<UserEditService>();
builder.Services.AddScoped<UserData>();
builder.Services.AddScoped<Audit>();
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