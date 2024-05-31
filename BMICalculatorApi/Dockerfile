# Use the official .NET image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY BMICalculatorApi.csproj .
RUN dotnet restore "BMICalculatorApi.csproj"

# Copy the rest of the application
COPY . .
WORKDIR /src
RUN dotnet build "BMICalculatorApi.csproj" -c Release -o /app/build

# Use the official ASP.NET Core runtime image as the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "BMICalculatorApi.dll"]
