using MySql.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddDbContext<ShoppingCart.Models.AppDbContext>(options => options.UseMySQL());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
