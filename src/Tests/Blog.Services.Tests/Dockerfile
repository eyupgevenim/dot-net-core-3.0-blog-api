FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Tests/Blog.Services.Tests/Blog.Services.Tests.csproj", "src/Tests/Blog.Services.Tests/"]
COPY ["src/Libraries/Blog.Data/Blog.Data.csproj", "src/Libraries/Blog.Data/"]
COPY ["src/Libraries/Blog.Core/Blog.Core.csproj", "src/Libraries/Blog.Core/"]
COPY ["src/Libraries/Blog.Services/Blog.Services.csproj", "src/Libraries/Blog.Services/"]
RUN dotnet restore "src/Tests/Blog.Services.Tests/Blog.Services.Tests.csproj"
COPY . .
WORKDIR "/src/src/Tests/Blog.Services.Tests"
RUN dotnet build "Blog.Services.Tests.csproj" -c Release -o /app/build

FROM build as servicestest
WORKDIR "/src/src/Tests/Blog.Services.Tests"

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Blog.Services.Tests.dll"]