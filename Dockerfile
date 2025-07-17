FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Backend_PhongChongMaTuy/Backend_PhongChongMaTuy.csproj", "Backend_PhongChongMaTuy/"]
COPY ["BusinessObjects_PhongChongMaTuy/BusinessObjects_PhongChongMaTuy.csproj", "BusinessObjects_PhongChongMaTuy/"]
RUN dotnet restore "Backend_PhongChongMaTuy/Backend_PhongChongMaTuy.csproj"

COPY . .
WORKDIR "/src/Backend_PhongChongMaTuy"
RUN dotnet publish "Backend_PhongChongMaTuy.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Backend_PhongChongMaTuy.dll"]
