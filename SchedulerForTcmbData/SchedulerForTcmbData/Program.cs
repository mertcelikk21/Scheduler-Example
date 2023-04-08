
using Coravel;
using Microsoft.Extensions.Configuration;
using SchedulerForTcmbData.Abstract;
using SchedulerForTcmbData.Models;
using SchedulerForTcmbData.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScheduler();
var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddTransient<CustomScheduler>();
builder.Services.AddScoped<ITcmbRequestService,TcmbRequestService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<CustomScheduler>().DailyAt(15,32).Zoned(TimeZoneInfo.Local);
});

app.Run();
