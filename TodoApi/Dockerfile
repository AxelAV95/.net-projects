# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "TodoApi.csproj"
RUN dotnet build "TodoApi.csproj" -c Release -o /app/build

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "TodoApi.csproj" -c Release -o /app/publish

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5185
ENV ASPNETCORE_URLS="http://+:5185" # Solo HTTP
ENTRYPOINT ["dotnet", "TodoApi.dll"]
