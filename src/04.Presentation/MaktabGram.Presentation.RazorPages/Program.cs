using MaktabGram.Domain.ApplicationServices.FollowAgg;
using MaktabGram.Domain.ApplicationServices.PostAgg;
using MaktabGram.Domain.ApplicationServices.UserAgg;
using MaktabGram.Domain.Core.FollowerAgg.Contracts;
using MaktabGram.Domain.Core.PostAgg.Contracts;
using MaktabGram.Domain.Core.UserAgg.Contracts;
using MaktabGram.Domain.Services.FollowerAgg;
using MaktabGram.Domain.Services.PostAgg;
using MaktabGram.Domain.Services.UserAgg;
using MaktabGram.Infrastructure.EfCore.Persistence;
using MaktabGram.Infrastructure.EfCore.Repositories.FollowerAgg;
using MaktabGram.Infrastructure.EfCore.Repositories.PostAgg;
using MaktabGram.Infrastructure.EfCore.Repositories.UserAgg;
using MaktabGram.Infrastructure.FileService.Contracts;
using MaktabGram.Infrastructure.FileService.Services;
using MaktabGram.Presentation.RazorPages.Extentions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddHttpContextAccessor();

#region RegisterServices

builder.Services.AddScoped<ICookieService, CookieService>();

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFollowerService, FollowerService>();

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFollowerRepository, FollowerRepository>();

builder.Services.AddScoped<IPostApplicationService, PostApplicationService>();
builder.Services.AddScoped<IUserApplicationService, UserApplicationService>();
builder.Services.AddScoped<IFollowerApplicationService, FollowerApplicationService>();

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer("Server=localhost;Database=MaktabGramDb;user id=sa;password=25915491;trust server certificate=true"));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=LAPTOP-6U51JF85\\SQL2022;Database=MaktabGram_V1;Trusted_Connection=True;TrustServerCertificate=True;"));

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapGet("/", () => Results.Redirect("/Posts/Feeds"));


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
