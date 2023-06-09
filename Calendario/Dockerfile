#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
ENV ASPNETCORE_URLS=http://+:5000;http://+:5001

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Calendario/Calendario.csproj", "Calendario/"]
COPY ["Calendario/token/Google.Apis.Auth.OAuth2.Responses.TokenResponse-user","Calendario/"]
RUN dotnet restore "Calendario/Calendario.csproj"
COPY . .
WORKDIR "/src/Calendario"
RUN dotnet build "Calendario.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Calendario.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Calendario.dll"]