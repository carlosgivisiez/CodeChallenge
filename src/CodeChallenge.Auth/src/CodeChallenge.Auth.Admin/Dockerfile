FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CodeChallenge.Auth/src/CodeChallenge.Auth.Admin/CodeChallenge.Auth.Admin.csproj", "src/CodeChallenge.Auth.Admin/"]
COPY ["src/CodeChallenge.Auth/src/CodeChallenge.Auth.Admin.EntityFramework.Shared/CodeChallenge.Auth.Admin.EntityFramework.Shared.csproj", "src/CodeChallenge.Auth.Admin.EntityFramework.Shared/"]
COPY ["src/CodeChallenge.Auth/src/CodeChallenge.Auth.Admin.EntityFramework.SqlServer/CodeChallenge.Auth.Admin.EntityFramework.SqlServer.csproj", "src/CodeChallenge.Auth.Admin.EntityFramework.SqlServer/"]
COPY ["src/CodeChallenge.Auth/src/CodeChallenge.Auth.Shared/CodeChallenge.Auth.Shared.csproj", "src/CodeChallenge.Auth.Shared/"]
COPY ["src/CodeChallenge.Auth/src/CodeChallenge.Auth.Admin.EntityFramework.PostgreSQL/CodeChallenge.Auth.Admin.EntityFramework.PostgreSQL.csproj", "src/CodeChallenge.Auth.Admin.EntityFramework.PostgreSQL/"]
COPY ["src/CodeChallenge.Auth/src/CodeChallenge.Auth.Admin.EntityFramework.MySql/CodeChallenge.Auth.Admin.EntityFramework.MySql.csproj", "src/CodeChallenge.Auth.Admin.EntityFramework.MySql/"]
RUN dotnet restore "src/CodeChallenge.Auth.Admin/CodeChallenge.Auth.Admin.csproj"
COPY src/CodeChallenge.Auth/src src
WORKDIR "/src/src/CodeChallenge.Auth.Admin"
RUN dotnet build "CodeChallenge.Auth.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CodeChallenge.Auth.Admin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "CodeChallenge.Auth.Admin.dll"]