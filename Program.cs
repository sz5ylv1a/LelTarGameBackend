using Asp.Versioning;
using LelTarGameBackend.Services;
using LelTarGameBackend.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// API versioning setup
builder.Services.AddApiVersioning(o =>
{
	o.DefaultApiVersion = new ApiVersion(1, 0);
	o.AssumeDefaultVersionWhenUnspecified = true;
	o.ReportApiVersions = true;
})
.AddApiExplorer(o =>
{
	o.GroupNameFormat = "'v'VVV";
	o.SubstituteApiVersionInUrl = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lel.tar Backend API v1", Version = "v1.0" });
	c.SwaggerDoc("v2", new OpenApiInfo { Title = "Lel.tar Backend API v2", Version = "v2.0" });
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "Add JWT token: Bearer {token}"
	});
	c.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
	{
		[new OpenApiSecuritySchemeReference("Bearer", doc)] = []
	});
});

// connect this shit to database
builder.Services.AddDbContext<AppDbContext>(o => o.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!));

// JWT auth config
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["Key"];

builder.Services.AddAuthentication(o =>
{
	o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
	o.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = jwtSettings["Issuer"],
		ValidAudience = jwtSettings["Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
		ClockSkew = TimeSpan.Zero
	};
});

// add auth
builder.Services.AddAuthorization();
builder.Services.AddScoped<TokenService>();

// CORS config, dunno what would this be used for LOL
builder.Services.AddCors(o =>
{
	o.AddPolicy("AllowFrontend", p =>
	{
		p.AllowAnyOrigin()
		 .AllowAnyMethod()
		 .AllowAnyHeader();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger(c =>
	{
		c.RouteTemplate = "/swagger/{documentName}/swagger.json";
	});
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lel.tar Backend API v1.x");
		c.SwaggerEndpoint("/swagger/v2/swagger.json", "Lel.tar Backend API v2.x");
		c.RoutePrefix = "swagger";
	});
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	try
	{
		context.Database.EnsureCreated();
		// or context.Database.Migrate(); if migrations are used
	}
	catch (Exception ex)
	{
		var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
		logger.LogError(ex, "An error occurred while creating the database.");
	}
}

app.Run();