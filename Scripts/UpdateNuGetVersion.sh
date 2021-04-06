#!/bin/bash

GREEN=`tput setaf 2`

Version=$1
MagicGradients="../MagicGradients/MagicGradients.csproj"
MagicGradientsToolkit="../MagicGradients.Toolkit/MagicGradients.Toolkit.csproj"

echo "${GREEN}Set new $Version version for $MagicGradients"
sed "s|<Version>.*|<Version>$Version<\/Version>|" $MagicGradients | tee $MagicGradients

#echo "Set new $Version version for $MagicGradientsToolkit"
#sed "s|<Version>.*|<Version>$Version<\/Version>|" $MagicGradientsToolkit | tee $MagicGradientsToolkit

