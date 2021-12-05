#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["FirstWebCore/FirstWebCore.csproj", "FirstWebCore/"]
RUN dotnet restore "FirstWebCore/FirstWebCore.csproj"
COPY . .
WORKDIR "/src/FirstWebCore"
RUN dotnet build "FirstWebCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FirstWebCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FirstWebCore.dll"]