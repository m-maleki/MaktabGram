using Hangfire;
using MaktabGram.Domain.ApplicationServices.FollowAgg;
using MaktabGram.Domain.ApplicationServices.PostAgg;
using MaktabGram.Domain.ApplicationServices.UserAgg;
using MaktabGram.Domain.Core.FollowerAgg.Contracts;
using MaktabGram.Domain.Core.PostAgg.Contracts;
using MaktabGram.Domain.Core.UserAgg.Contracts.Otp;
using MaktabGram.Domain.Core.UserAgg.Contracts.User;
using MaktabGram.Domain.Services.FollowerAgg;
using MaktabGram.Domain.Services.PostAgg;
using MaktabGram.Domain.Services.UserAgg;
using MaktabGram.Framework;
using MaktabGram.Infrastructure.EfCore.Persistence;
using MaktabGram.Infrastructure.EfCore.Repositories.FollowerAgg;
using MaktabGram.Infrastructure.EfCore.Repositories.PostAgg;
using MaktabGram.Infrastructure.EfCore.Repositories.UserAgg;
using MaktabGram.Infrastructure.FileService.Contracts;
using MaktabGram.Infrastructure.FileService.Services;
using MaktabGram.Infrastructure.Notifications.Services;
using MaktabGram.Presentation.RazorPages.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

#region Log

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()

    // Info → Seq
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
        .WriteTo.Seq("http://localhost:5341"))

    // Error → File
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
        .WriteTo.File(@"C:\ApplicationLog\log-.txt", rollingInterval: RollingInterval.Day))

// Warning → SQL Server
.WriteTo.Logger(lc => lc
    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetValue<string>("ConnectionStrings:LogConnectionString"),
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "Logs",
            AutoCreateSqlTable = true,
            BatchPostingLimit = 100,
            BatchPeriod = TimeSpan.FromSeconds(2)
        },
        columnOptions: null
    ))

    .CreateBootstrapLogger();

builder.Host.UseSerilog();
#endregion

// Add services to the container.

builder.Services.AddHttpContextAccessor();

// Add Hangfire services.
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetValue<string>("ConnectionStrings:HangfireConnectionString")));

builder.Services.AddHangfireServer();

#region RegisterServices



builder.Services.AddScoped<ISmsService, SmsService>();

builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFollowerService, FollowerService>();

builder.Services.AddScoped<IOtpRepository, OtpRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFollowerRepository, FollowerRepository>();

builder.Services.AddScoped<IOtpApplicationService, OtpApplicationService>();
builder.Services.AddScoped<IPostApplicationService, PostApplicationService>();
builder.Services.AddScoped<IUserApplicationService, UserApplicationService>();
builder.Services.AddScoped<IFollowerApplicationService, FollowerApplicationService>();

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer("Server=localhost;Database=MaktabGramDb;user id=sa;password=25915491;trust server certificate=true"));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetValue<string>("ConnectionStrings:AppConnectionString")));


builder.Services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(
    options =>
    {
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 4;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddErrorDescriber<PersianIdentityErrorDescriber>()
    .AddEntityFrameworkStores<AppDbContext>();

#endregion

builder.Host.UseSerilog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard("/hangfire");
app.UseMiddleware<LoggingMiddleware>();

app.Run();
