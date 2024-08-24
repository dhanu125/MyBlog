using Microsoft.EntityFrameworkCore;
using MYBlog.Data;
using MYBlog.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BlogDBContext>(Options=>
Options.UseSqlServer(builder.Configuration.GetConnectionString("BlogPostConnectionString")));

builder.Services.AddScoped<ITagInterface,TagImplementation>();  
builder.Services.AddScoped<IBlogPostInterface, BlogPostsImplementation>();
builder.Services.AddScoped<IImageInterface, ImageImplementation>();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
