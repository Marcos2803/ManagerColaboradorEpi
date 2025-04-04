using Gestao.Epi_Domain.Entities.Account;
using Gestao.Epi_Domain.Interface;
using GestaoEpiRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using gestao.EpiData.Context;
using gestao.EpiData.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configuração da conexão com o banco de dados
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var environment = builder.Environment;
var envName = builder.Environment.EnvironmentName;

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{envName}.json", optional: true, reloadOnChange: true);

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada.");
}

var secret = Encoding.ASCII.GetBytes(builder.Configuration["JwtConfiguration:Secret"]
    ?? throw new InvalidOperationException("Chave JWT não configurada!"));

// 🔹 Configuração do contexto do banco
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("DataContext");
        sqlOptions.EnableRetryOnFailure(5);
    });
});

// 🔹 Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:3000", "https://teacher.com.br")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());
});

// 🔹 Configuração de autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secret),
            ValidateIssuer = !string.IsNullOrEmpty(builder.Configuration["JwtConfiguration:Issuer"]),
            ValidateAudience = !string.IsNullOrEmpty(builder.Configuration["JwtConfiguration:Audience"]),
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JwtConfiguration:Issuer"],
            ValidAudience = builder.Configuration["JwtConfiguration:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // 🚀 Somente HTTPS
    options.Cookie.SameSite = SameSiteMode.None; // 🚀 Permite cross-origin
});


// 🔹 Configuração do Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<DataContext>()
.AddUserManager<UserManager<User>>()
.AddRoleManager<RoleManager<IdentityRole>>()
.AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
    options.TokenLifespan = TimeSpan.FromHours(3));

// 🔹 Injeção de dependências
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthenticationJwtServices, GerarTokenRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Manager Epi",
        Description = "API responsável pelo gerenciamento de autenticação e regras de negócio",
        Contact = new OpenApiContact() { Name = "felipe rodrigues", Email = "felipemercadopago@gmail.com" },
        Version = "v1"
    });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no campo de texto: Bearer {token}"
    };

    var securityRequirement = new OpenApiSecurityRequirement
    {
        { securityScheme, new[] { "Bearer" } }
    };

    config.AddSecurityDefinition("Bearer", securityScheme);
    config.AddSecurityRequirement(securityRequirement);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentityInitializer.CriarRoles(services);
    await IdentityInitializer.CriarUsuarioAdmin(services);
}

app.MapControllers();

app.Run();
