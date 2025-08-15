#!/bin/bash

pkill SpeedApply
rm -rf /app/build/*
mkdir -p /app/build
dotnet build "/app/SpeedApply.csproj" -c Release -o /app/build
mkdir -p /app/publish
rm -rf /app/publish/*
dotnet publish "/app/SpeedApply.csproj" -c Release -o /app/publish /p:UseAppHost=true