FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Tests/Blog.API.IntegrationTests/Blog.API.IntegrationTests.csproj", "src/Tests/Blog.API.IntegrationTests/"]
COPY ["src/Presentation/Blog.API/Blog.API.csproj", "src/Presentation/Blog.API/"]
COPY ["src/Libraries/Blog.Data/Blog.Data.csproj", "src/Libraries/Blog.Data/"]
COPY ["src/Libraries/Blog.Core/Blog.Core.csproj", "src/Libraries/Blog.Core/"]
COPY ["src/Libraries/Blog.Services/Blog.Services.csproj", "src/Libraries/Blog.Services/"]
RUN dotnet restore "src/Tests/Blog.API.IntegrationTests/Blog.API.IntegrationTests.csproj"
COPY . .
WORKDIR "/src/src/Tests/Blog.API.IntegrationTests"
RUN dotnet build "Blog.API.IntegrationTests.csproj" -c Release -o /app/build

FROM build as integrationtest
WORKDIR "/src/src/Tests/Blog.API.IntegrationTests"

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Blog.API.IntegrationTests.dll"]