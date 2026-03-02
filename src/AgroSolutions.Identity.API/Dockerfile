FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copia csproj (API + dependências) para AS MESMAS pastas
COPY AgroSolutions.Identity.API/*.csproj AgroSolutions.Identity.API/
COPY AgroSolutions.Application/*.csproj AgroSolutions.Application/
COPY AgroSolutions.Identity.Data/*.csproj AgroSolutions.Identity.Data/
COPY AgroSolutions.Identity.Domain/*.csproj AgroSolutions.Identity.Domain/

RUN dotnet restore AgroSolutions.Identity.API/AgroSolutions.Identity.csproj

# copia o resto do repo
COPY . .

RUN dotnet publish AgroSolutions.Identity.API/AgroSolutions.Identity.csproj \
    -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AgroSolutions.Identity.dll"]