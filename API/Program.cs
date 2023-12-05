using API.Errors;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);


var app = builder.Build();

//! order important 
// Configure the HTTP request pipeline.
//!exception middleware mora na vrh pre svega
app.UseMiddleware<ExceptionMiddleware>();

//! http because you had problems with https in sec 7
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
