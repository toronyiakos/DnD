
using Dnd_Api.Models;
using Dnd_Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		builder.Services.AddDbContext<AppDbContext>(options =>
			options.UseMySql(
				builder.Configuration.GetConnectionString("DefaultConnection"),
				ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
			)
		);
		

		builder.Services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			var jwt = builder.Configuration.GetSection("Jwt");

			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = jwt["Issuer"],
				ValidAudience = jwt["Audience"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!))
			};
		});

		builder.Services.AddAuthorization(options =>
		{
			options.AddPolicy("UserOnly", p => p.RequireRole("user", "game_master", "admin"));
			options.AddPolicy("GameMasterOnly", p => p.RequireRole("game_master", "admin"));
			options.AddPolicy("AdminOnly", p => p.RequireRole("admin"));
		});


		builder.Services.AddScoped<IJwtService, JwtService>();
		builder.Services.AddScoped<IAuthService, AuthService>();

		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(options =>
		{
			var securityScheme = new OpenApiSecurityScheme
			{
				Name = "Authorization",
				Description = "Enter: Bearer {token}",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.Http,
				Scheme = "Bearer",
				BearerFormat = "JWT"
			};
			options.AddSecurityDefinition("Bearer", securityScheme);
			options.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
				}, new string[]{}
			}
	});
		});

		builder.Services.AddControllers();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseAuthentication();
		app.UseAuthorization();
		app.MapControllers();

		app.Run();
	}
}