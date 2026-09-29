using HeroesWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("conn")
    ?? throw new InvalidOperationException("No se encontro la conexion 'conn'.");

// Heroes y SuperPoderes quedan protegidas: solo entra un usuario autenticado.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Heroes");
    options.Conventions.AuthorizeFolder("/SuperPoderes");
});

builder.Services.AddDbContext<HeroesContext>(options =>
    options.UseSqlServer(connectionString));

// Mismo HeroesDb, pero en un contexto separado solo para las tablas de Identity.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services
    .AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

// Primero se identifica al usuario (Authentication) y luego se decide si puede entrar (Authorization).
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
