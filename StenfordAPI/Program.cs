using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stenford.Domain.DataContext;
using Stenford.Service.Account;
using Stenford.Service.Dashboard;
using Stenford.Service.Dropdown;
using Stenford.Service.JwtToken;
using Stenford.Service.SalesPerson;
using Stenford.Service.Showroom;
using Stenford.Service.Visit;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IJwtTokenRepository, JwtTokenRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ISalesPersonRepository, SalesPersonRepository>();
builder.Services.AddScoped<IDropdownRepository, DropdownRepository>();
builder.Services.AddScoped<IShowroomRepository, ShowroomRepository>();
builder.Services.AddScoped<IVisitRepository, VisitRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];
var jwtKey = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = jwtIssuer,
			ValidAudience = jwtAudience,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
		};
	});

var app = builder.Build();
//only for dev-purpose

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
