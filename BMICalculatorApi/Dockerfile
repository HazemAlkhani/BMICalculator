# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore the dependencies
COPY BMICalculatorApi/BMICalculatorApi.csproj ./BMICalculatorApi/
RUN dotnet restore "./BMICalculatorApi/BMICalculatorApi.csproj"

# Copy the rest of the application
COPY BMICalculatorApi/. ./BMICalculatorApi/
WORKDIR /src/BMICalculatorApi
RUN dotnet build "BMICalculatorApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BMICalculatorApi.csproj" -c Release -o /app/publish

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY BMICalculatorApi/appsettings*.json ./

# Environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "BMICalculatorApi.dll"]
