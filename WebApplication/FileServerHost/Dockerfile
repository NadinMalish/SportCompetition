FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY FileServerHost/FileServerHost.csproj FileServerHost/
RUN dotnet restore FileServerHost/FileServerHost.csproj

COPY . .
WORKDIR /src/FileServerHost
RUN dotnet publish -c Release -o /app/out --no-restore

# ---- runtime ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 8081
ENV ASPNETCORE_URLS=http://+:8081
ENTRYPOINT ["dotnet", "FileServerHost.dll"]
