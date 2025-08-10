#!/bin/sh
unzip /app/clients/allied.zip -d /app/clients/allied
unzip /app/clients/enemy.zip -d /app/clients/enemy

mkdir /sim/
cd /sim/
dotnet /app/build/AIPilot.dll /app/simConfig.json