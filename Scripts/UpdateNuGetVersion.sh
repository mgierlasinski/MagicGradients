#!/bin/bash

GREEN=`tput setaf 2`

Version=$1
MagicGradients="../src/MagicGradients/MagicGradients.csproj"
MagicGradientsToolkit="../src/MagicGradients.Toolkit/MagicGradients.Toolkit.csproj"

echo "${GREEN}Set new $Version version for $MagicGradients"
sed -i .tmp "s|<Version>.*|<Version>$Version<\/Version>|" $MagicGradients;
rm -f "$magicGradients.tmp"

echo "Set new $Version version for $MagicGradientsToolkit"
sed -i .tmp "s|<Version>.*|<Version>$Version<\/Version>|" $MagicGradientsToolkit;
rm -f "$MagicGradientsToolkit.tmp"
