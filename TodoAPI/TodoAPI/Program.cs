using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TodoAPI.Models;

var builder = WebApplication.CreateBuilder(args);

//Add Configuartion variable
ConfigurationManager configuration = builder.Configuration;

//Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

//Add JWT Bearer
.AddJwtBearer(options =>
 {
     options.SaveToken = true;
     options.RequireHttpsMetadata = false;
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidAudience = configuration["JWT:ValidAudience"],
         ValidIssuer = configuration["JWT:ValidIssuer"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
     };
 });

//Enable Cors 定義CORS Policy
var myCors = "appCors";

builder.Services.AddCors(options =>
{
    options.AddPolicy(myCors, policy =>
    {
        policy.WithOrigins("https://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


// Add services to the container.
//使用資料庫
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TodoContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 套用CORS策略(這邊是影響所有的controllers)
// app.UseCors(MyAllowSpecificOrigins);
// 套用但須各個controllers去指定策略
app.UseCors();

app.UseHttpsRedirection();

//Add Authentiaction
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
