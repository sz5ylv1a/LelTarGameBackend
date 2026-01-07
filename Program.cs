using LelTarGameBackend.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// the piece of shit here just kept throwing bullshit fuckin errors so i said "fuck this" and commented all this shit out
builder.Services.AddSwaggerGen(//c =>
//{
//	c.SwaggerDoc("v0", new() { Title = "Lel.tar API", Version = "v0" });

//	// JWT auth config in Swagger
//	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//	{
//		Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
//		Name = "Authorization",
//		In = ParameterLocation.Header,
//		Type = SecuritySchemeType.ApiKey,
//		Scheme = "Bearer"
//	});

//	c.AddSecurityRequirement(new OpenApiSecurityRequirement()
//	{
//		{
//			new OpenApiSecurityScheme
//			{
//				Reference = new OpenApiReference
//				{
//					Type = ReferenceType.SecurityScheme,
//					Id = "Bearer"
//				},
//				Scheme = "oauth2",
//				Name = "Bearer",
//				In = ParameterLocation.Header,
//			},
//			new List<string>()
//		}
//	});
//}
);

// connect this shit to database
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!));

// JWT auth config
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

//builder.Services.AddAuthentication(options =>
//{
//	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//	options.TokenValidationParameters = new TokenValidationParameters
//	{
//		ValidateIssuer = true,
//		ValidateAudience = true,
//		ValidateLifetime = true,
//		ValidateIssuerSigningKey = true,
//		ValidIssuer = jwtSettings["Issuer"],
//		ValidAudience = jwtSettings["Audience"],
//		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
//		ClockSkew = TimeSpan.Zero
//	};
//});

// add auth
builder.Services.AddAuthorization();

// CORS config, dunno what would this be used for LOL
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowFrontend", policy =>
	{
		policy.AllowAnyOrigin()
			  .AllowAnyMethod()
			  .AllowAnyHeader();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
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
		// vagy context.Database.Migrate(); ha migration-őket használsz
	}
	catch (Exception ex)
	{
		var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
		logger.LogError(ex, "An error occurred while creating the database.");
	}
}

app.Run();