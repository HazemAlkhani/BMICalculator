# Use the official .NET image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY BMICalculatorApi/BMICalculatorApi.csproj BMICalculatorApi/
RUN dotnet restore "BMICalculatorApi/BMICalculatorApi.csproj"

# Copy the rest of the application
COPY BMICalculatorApi/ BMICalculatorApi/
WORKDIR /src/BMICalculatorApi
RUN dotnet build "BMICalculatorApi.csproj" -c Release -o /app/build

# Use the official ASP.NET Core runtime image as the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Copy the environment file to the runtime image
COPY BMICalculatorApi/.env.production .

# Copy the build output to the runtime image
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "BMICalculatorApi.dll"]
