FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /app

RUN apt-get update
RUN apt-get install unzip -y

COPY entrypoint.sh ./

COPY AIPilot.sln ./
COPY AIPilot.csproj ./

COPY src/ ./src/

RUN export DOTNET_SYSTEM_NET_DISABLEIPV6=1

# RUN echo "MaxProtocol = TLSv1.2" >> /etc/ssl/openssl.cnf
RUN wget https://hs.vtolvr.live/api/v1/public/users/0 -O testHs.json -T 15
RUN cat testHs.json

RUN wget https://api.nuget.org/v3/index.json -O nuget.json -T 15
RUN cat nuget.json


RUN dotnet build AIPilot.csproj -c Release -o /app/build

CMD ["sh", "/app/entrypoint.sh"]