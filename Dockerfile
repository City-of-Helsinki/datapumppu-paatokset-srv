FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

LABEL io.openshift.expose-services="8080:http"
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080

COPY AhjoApiService/AhjoApiService.csproj .
RUN dotnet restore "AhjoApiService.csproj"

COPY . .
RUN dotnet publish "AhjoApiService/AhjoApiService.csproj" -c Release -o /publish
    
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "AhjoApiService.dll"]