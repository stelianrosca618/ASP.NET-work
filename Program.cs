global using MyRazorPagesApp.Components;
global using MyRazorPagesApp.ViewModel;
global using MyRazorPagesApp.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyRazorPagesApp.Data;
using Pomelo.EntityFrameworkCore;
using MyRazorPagesApp.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MyRazorPagesApp.Dtos;
using MyRazorPagesApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("mysql") ?? throw new InvalidOperationException("Connection string 'Azure' not found.");
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlServer(connectionString));
//     options.Use


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 26)), mySqlOptions =>
    {
        mySqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
    });
});

builder.Services.AddQuickGridEntityFrameworkAdapter();;
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddHttpClient();
builder.Services.AddAuthentication();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(op =>
{
    
});

builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<CustomCircuitHandler>();
builder.Services.AddScoped<IVoteDto, VoteDto>();
builder.Services.AddScoped<IQuestionDto, QuestionDto>();
builder.Services.AddScoped<IPostDto, PostDto>();
builder.Services.AddScoped<IAnswerDto, AnswerDto>();
builder.Services.AddScoped<ICommentsDto, CommentsDto>();
builder.Services.AddScoped<IViewsDto, ViewDto>();
builder.Services.AddRazorComponents()
     .AddInteractiveServerComponents();   
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
            //app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

            
app.UseAuthentication();
          
app.UseAuthorization();
app.UseAntiforgery();


//app.UseAuthorization();

//app.UseHttpsRedirection();
//app.UseBlazorFrameworkFiles();
//app.UseStaticFiles();

//app.UseRouting();
app.MapControllers();
app.MapRazorPages();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.Run();
