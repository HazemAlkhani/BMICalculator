using Microsoft.EntityFrameworkCore;
using BMICalculatorApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Use environment variable for the connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")?
                           .Replace("{DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD")) 
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IBMIRecordRepository, BMIRecordRepository>();

// Configure CORS to allow requests from the frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BMI Calculator API V1");
    });
}
else
{
    // Add HSTS and HTTPS Redirection for production
    app.UseHsts();
    app.UseHttpsRedirection();
}

// Use CORS
app.UseCors("AllowAll");

app.UseRouting();

app.UseAuthentication(); // If you have authentication
app.UseAuthorization();

app.MapControllers();

app.Run();