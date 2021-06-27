FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
COPY ./bin/docker .
ENV ASPNETCORE_ENVIRONMENT docker
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT dotnet Shop.Services.Discounts.dll
