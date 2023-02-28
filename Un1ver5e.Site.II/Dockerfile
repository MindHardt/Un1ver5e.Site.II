#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Un1ver5e.Site.II/Un1ver5e.Site.II.csproj", "Un1ver5e.Site.II/"]
RUN dotnet restore "Un1ver5e.Site.II/Un1ver5e.Site.II.csproj"
COPY . .
WORKDIR "/src/Un1ver5e.Site.II"
RUN dotnet build "Un1ver5e.Site.II.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Un1ver5e.Site.II.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Un1ver5e.Site.II.dll"]