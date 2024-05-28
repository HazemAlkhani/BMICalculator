FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BMICalculatorApi/BMICalculatorApi.csproj", "BMICalculatorApi/"]
RUN dotnet restore "BMICalculatorApi/BMICalculatorApi.csproj"
COPY . .
WORKDIR "/src/BMICalculatorApi"
RUN dotnet build "BMICalculatorApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BMICalculatorApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BMICalculatorApi.dll"]
