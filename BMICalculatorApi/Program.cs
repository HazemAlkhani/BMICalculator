using Microsoft.EntityFrameworkCore;
using BMICalculatorApi.Data;
using DotNetEnv;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Manually set the root directory of the solution
string solutionRootDirectory = @"C:\Users\hazem\RiderProjects\BMICalculator";

// Determine the project directory for BMICalculatorApi
string projectDirectory = Path.Combine(solutionRootDirectory, "BMICalculatorApi");

// Determine the environment and load the corresponding .env file
string envFile = builder.Environment.EnvironmentName switch
{
    "Development" => ".env.development",
    "Test" => ".env.test",
    "Production" => ".env.production",
    _ => ".env"
};

// Ensure the file path is correctly resolved relative to the solution root directory
string envFilePath = Path.Combine(solutionRootDirectory, envFile);

// Debugging output
Console.WriteLine($"Solution Root Directory: {solutionRootDirectory}");
Console.WriteLine($"Project Directory: {projectDirectory}");
Console.WriteLine($"Environment File Path: {envFilePath}");

// Load environment variables from the specified .env file
if (File.Exists(envFilePath))
{
    Env.Load(envFilePath);
    Console.WriteLine("Environment file loaded successfully.");
}
else
{
    Console.WriteLine($"The environment file '{envFilePath}' was not found.");
    throw new FileNotFoundException($"The environment file '{envFilePath}' was not found.");
}

// Load environment-specific appsettings
builder.Configuration
    .SetBasePath(projectDirectory)  // Use project directory for appsettings.json
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

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

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
