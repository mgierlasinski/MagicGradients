Function Update-NuGetVersion{
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true)]
        [string]$Version,
        [Parameter(Mandatory=$true)]
        [String]$File
        )
        Replace-LineInFile $Version $File
}

Function Replace-LineInFile{
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true)]
        [String]$Version,
        [String]$File
        )
         Write-Host "Set new $Version version for $File"
        $Regex  = '(?<=<Version>)[^<]*'
        (Get-Content $File) -replace $Regex, $Version | Set-Content $File
}

Function Update-AllNuGetVersions{
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true)]
        [String]$Version
        )
        
        Update-NuGetVersion $Version '..\MagicGradients\MagicGradients.csproj';
}

Update-AllNuGetVersions