
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["APIGatewayService/APIGatewayService.csproj", "APIGatewayService/"]
COPY Autotag.sln .
RUN dotnet restore "APIGatewayService/APIGatewayService.csproj"

COPY . .
WORKDIR "/src/APIGatewayService"
RUN dotnet publish "APIGatewayService.csproj" -c Release -o /app/build

# -----------------------------------------------


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080 
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "APIGatewayService.dll"]