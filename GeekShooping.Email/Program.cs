using GeekShooping.Email.MessageConsume;
using GeekShooping.Email.Models.Context;
using GeekShooping.Email.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration["ConnectionStrings:MySQLConnection"];

builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(
    connection,
        new MySqlServerVersion(
            new Version(8, 0, 29))));

var dbContextBuilder = new DbContextOptionsBuilder<MySQLContext>();
dbContextBuilder.UseMySql(
    connection,
    new MySqlServerVersion(new Version(8, 0, 29))
);

builder.Services.AddSingleton(new EmailRepository(dbContextBuilder.Options));
builder.Services.AddScoped<IEmailRepository,EmailRepository>();
builder.Services.AddHostedService<RabbirMQPaymentConsumer>();
var app = builder.Build();

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
