FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Autotag.sln .
COPY ["ImageAIService/ImageAIService.csproj", "ImageAIService/"]

RUN dotnet restore "ImageAIService/ImageAIService.csproj"

# ⬇️ ⬇️ تعديل: نسخ جميع ملفات الحل إلى مجلد العمل ⬇️ ⬇️
COPY . .
# ⬆️ ⬆️ ⬆️ ⬆️ ⬆️ 

WORKDIR "ImageAIService"
RUN dotnet publish "ImageAIService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# -----------------------------------------------

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ImageAIService.dll"]