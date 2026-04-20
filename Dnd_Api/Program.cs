
using Dnd_Api.ConfigModels;
using Dnd_Api.Models;
using Dnd_Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Text;
using System.Threading.RateLimiting;
using Microsoft.Extensions.Localization;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddLocalization(options =>
		{
			options.ResourcesPath = "Resources";
		});

		builder.Configuration
			.AddJsonFile("appsettings.json", optional: true)
			.AddUserSecrets<Program>(optional: true)
			.AddEnvironmentVariables();

		builder.Services.AddOptions<JwtOptions>()
			.Bind(builder.Configuration.GetSection("Jwt"))
			.ValidateDataAnnotations()
			.Validate(o => !string.IsNullOrWhiteSpace(o.Secret), "JWT Secret missing")
			.ValidateOnStart();

		builder.Services.AddOptions<DatabaseOptions>()
			.Configure<IConfiguration>((opt, config) =>
			{
				opt.Default = config.GetConnectionString("Default")!;
			})
			.ValidateDataAnnotations()
			.ValidateOnStart();

		builder.Services.AddRateLimiter(options =>
		{
			options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
			{

				var key =
					context.User?.Identity?.Name ??
					context.Connection.RemoteIpAddress?.ToString() ??
					"anonymous";

				return RateLimitPartition.GetFixedWindowLimiter(
					key,
					_ => new FixedWindowRateLimiterOptions
					{
						PermitLimit = 60,
						Window = TimeSpan.FromMinutes(1)
					});
			});

			options.AddFixedWindowLimiter("login", opt =>
			{
				opt.PermitLimit = 5;
				opt.Window = TimeSpan.FromMinutes(1);
				opt.QueueLimit = 0;
			});

			options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
		});


		// Add services to the container.

		builder.Services.AddDbContext<AppDbContext>((sp, options) =>
		{
			var dbOptions = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
			
			options.UseMySql(
				dbOptions.Default,
				ServerVersion.AutoDetect(dbOptions.Default)
			);
		});
		

		var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>()
			?? throw new InvalidOperationException("Jwt config missing");

		builder.Services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ClockSkew = TimeSpan.Zero,

				ValidIssuer = jwtOptions.Issuer,
				ValidAudience = jwtOptions.Audience,

				IssuerSigningKey = new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(jwtOptions.Secret)
				)
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

		builder.Services.AddCors(options =>
		{
			options.AddPolicy("FrontendPolicy", policy =>
			{
				policy
					.WithOrigins("http://localhost:3000")
					.WithMethods("GET", "POST", "PUT", "DELETE")
					.WithHeaders("Authorization", "Content-Type");
			});
		});

		builder.Services.AddControllers();

		builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);


		var app = builder.Build();

		var supportedCultures = new[]
		{
			new CultureInfo("en"),
			new CultureInfo("hu")
		};

		var localizationOptions = new RequestLocalizationOptions
		{
			DefaultRequestCulture = new RequestCulture("en"),
			SupportedCultures = supportedCultures,
			SupportedUICultures = supportedCultures
		};

		localizationOptions.RequestCultureProviders = new List<IRequestCultureProvider>
		{
			new AcceptLanguageHeaderRequestCultureProvider()
		};

		app.UseRequestLocalization(localizationOptions);

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseRateLimiter();

		app.UseRouting();

		app.UseCors("AllowFrontend");

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();

		app.UseExceptionHandler(errorApp =>
		{
			errorApp.Run(async context =>
			{
				var localizer = context.RequestServices
					.GetRequiredService<IStringLocalizer<Program>>();

				context.Response.StatusCode = 500;
				context.Response.ContentType = "application/json";

				await context.Response.WriteAsJsonAsync(new
				{
					error = localizer["Error_InternalServerError"]
				});
			});
		});

		app.Use(async (context, next) =>
		{
			context.Response.Headers["X-Content-Type-Options"] =
				"nosniff";

			context.Response.Headers["X-Frame-Options"] =
				"DENY";

			context.Response.Headers["Content-Security-Policy"] =
				"default-src 'self'";

			context.Response.Headers["Referrer-Policy"] =
				"no-referrer";

			await next();
		});

		app.Run();
	}
}