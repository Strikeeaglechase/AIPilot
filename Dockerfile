FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /app

RUN apt-get update
RUN apt-get install unzip -y

COPY entrypoint.sh ./

COPY AIPilot.sln ./
COPY AIPilot.csproj ./

COPY src/ ./src/

RUN dotnet build AIPilot.csproj -c Release -o /app/build

CMD ["sh", "/app/entrypoint.sh"]