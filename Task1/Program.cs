using Hangfire;
using Task1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add Hangfire services.
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();


builder.Services.AddScoped<HangfireService>();




var app = builder.Build();
using(var scope=app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var hangfireService = services.GetRequiredService<HangfireService>();
    var recurringJobManager = services.GetRequiredService<IRecurringJobManager>();

    recurringJobManager.AddOrUpdate("RecurringJob", () => hangfireService.RecurringJob(), Cron.Minutely);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard("/jobs");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
