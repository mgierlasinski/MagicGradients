#!/bin/bash

GREEN=`tput setaf 2`

NuGetPath="../src/MagicGradients/bin/**/Release/*.nupkg"
PackagesFolder="~/Packages"

echo "${GREEN}Remove all nupkg's"
rm $NuGetPath

echo "Build NuGet & Copy for $1" 
nuget pack $1 -Build -Properties Configuration=Release -OutputDirectory ~/Packages

echo "Completed."
