FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /app

RUN apt-get update
RUN apt-get install unzip -y

COPY entrypoint.sh ./

COPY AIPilot.sln ./
COPY AIPilot.csproj ./

COPY src/ ./src/

RUN export DOTNET_SYSTEM_NET_DISABLEIPV6=1
RUN dotnet build --verbosity diagnostic AIPilot.csproj -c Release -o /app/build

CMD ["sh", "/app/entrypoint.sh"]