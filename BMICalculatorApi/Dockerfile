# Use the official .NET Core SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy the csproj file and restore any dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the rest of the application code and build the application
COPY . .
RUN dotnet publish -c Release -o /app

# Use the official ASP.NET Core runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

# Expose port 80 to the outside world
EXPOSE 80

# Set the startup command
ENTRYPOINT ["dotnet", "BMICalculatorApi.dll"]
