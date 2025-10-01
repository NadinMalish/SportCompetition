FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY WebApplication/WebApplication.csproj WebApplication/
COPY Domain.Entities/Domain.Entities.csproj Domain.Entities/
COPY Infrastructure.EntityFramework/Infrastructure.EntityFramework.csproj Infrastructure.EntityFramework/
COPY Infrastructure.Repositories.Implementations/Infrastructure.Repositories.Implementations.csproj Infrastructure.Repositories.Implementations/
COPY RedisService/RedisService.csproj RedisService/
COPY Services.Repositories.Abstractions/Services.Repositories.Abstractions.csproj Services.Repositories.Abstractions/
RUN dotnet restore WebApplication/WebApplication.csproj

COPY . .
WORKDIR /src/WebApplication
RUN dotnet publish -c Release -o /app/out --no-restore

# ---- runtime ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "WebApplication.dll"]
