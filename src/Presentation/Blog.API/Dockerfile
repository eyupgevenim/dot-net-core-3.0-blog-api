FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src

COPY ["Blog.sln", "."]
COPY ["src/Presentation/Blog.API/Blog.API.csproj", "src/Presentation/Blog.API/"]
COPY ["src/Presentation/Blog.API/Blog.API.xml", "src/Presentation/Blog.API/"]
COPY ["src/Libraries/Blog.Data/Blog.Data.csproj", "src/Libraries/Blog.Data/"]
COPY ["src/Libraries/Blog.Core/Blog.Core.csproj", "src/Libraries/Blog.Core/"]
COPY ["src/Libraries/Blog.Services/Blog.Services.csproj", "src/Libraries/Blog.Services/"]
#COPY ["src/Tests/Blog.Services.Tests.csproj", "src/Tests/Blog.Services.Tests/"]
#COPY ["src/Tests/Blog.API.IntegrationTests.csproj", "src/Tests/Blog.API.IntegrationTests/"]
RUN dotnet restore "src/Presentation/Blog.API/Blog.API.csproj"
#RUN dotnet restore "Blog.sln"
COPY . .
WORKDIR "/src/src/Presentation/Blog.API"
RUN dotnet build "Blog.API.csproj" -c Release -o /app/build

#FROM build as servicestest
#WORKDIR "/src/src/Tests/Blog.Services.Tests"

#FROM build as integrationtest
#WORKDIR "/src/src/Tests/Blog.API.IntegrationTests"

FROM build AS publish
RUN dotnet publish "Blog.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.API.dll"]