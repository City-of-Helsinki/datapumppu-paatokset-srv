FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

LABEL io.openshift.expose-services="8080:http"
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENV DEBIAN_FRONTEND=noninteractive
RUN apt-get update && \
    apt-get upgrade -y && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./AhjoApiService/AhjoApiService.csproj", "./"]
RUN dotnet restore "AhjoApiService.csproj"

ENV DEBIAN_FRONTEND=noninteractive
RUN apt-get update && \
    apt-get upgrade -y && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

COPY . .
WORKDIR "/src"
RUN dotnet build "AhjoApiService/AhjoApiService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AhjoApiService/AhjoApiService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AhjoApiService.dll"]