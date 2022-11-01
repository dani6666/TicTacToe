#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY . .
RUN dotnet publish TicTacToe.sln -c Release -o app/publish
EXPOSE 8000

ENTRYPOINT ["dotnet", "app/publish/TicTacToe.dll"]