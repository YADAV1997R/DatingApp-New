using System.Text;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using API.Extensions;



var builder = WebApplication.CreateBuilder(args);
// 🔹 Add Authentication Service
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");
app.MapControllers();

app.Run();
