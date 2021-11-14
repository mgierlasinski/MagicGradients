#!/usr/bin/env bash
SLN_PATH="$APPCENTER_SOURCE_DIRECTORY/MagicGradients.sln"
UWP_PATH="$APPCENTER_SOURCE_DIRECTORY/Playground/Playground.UWP/Playground.UWP.csproj"
dotnet sln $SLN_PATH remove $UWP_PATH