# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


COPY src/AgroSolutions.Identity.API/AgroSolutions.Identity.csproj AgroSolutions.Identity.API/
COPY src/AgroSolutions.Application/AgroSolutions.Application.csproj AgroSolutions.Application/
COPY src/AgroSolutions.Identity.Domain/AgroSolutions.Identity.Domain.csproj AgroSolutions.Identity.Domain/
COPY src/AgroSolutions.Identity.Data/AgroSolutions.Identity.Data.csproj AgroSolutions.Properties.Data/


RUN dotnet restore AgroSolutions.Identity.API/AgroSolutions.Identity.csproj

COPY src .
WORKDIR /src/AgroSolutions.Identity.API
RUN dotnet publish "AgroSolutions.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "AgroSolutions.Identity.dll"]

