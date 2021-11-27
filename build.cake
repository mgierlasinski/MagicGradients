var target = Argument("target", "Pack");
var configuration = Argument("configuration", "Release");

var buildSettings = new MSBuildSettings()
    .SetConfiguration(configuration);
    //.WithRestore()
    //.WithTarget("Rebuild");

var packSettings = new MSBuildSettings()
    .SetConfiguration(configuration)
    .WithTarget("Pack")
    .WithProperty("PackageOutputPath", "C:\\Packages");

Task("LegacyBuild")
    .Does(() => 
    {
        MSBuild("src/MagicGradients/MagicGradients.csproj", buildSettings);
    });

Task("LegacyPack")
    .IsDependentOn("LegacyBuild")
    .Does(() => 
    {
        MSBuild("src/MagicGradients/MagicGradients.csproj", packSettings);
    });

Task("Build")
    .Does(() => 
    {
        MSBuild("src/MagicGradients.Forms/MagicGradients.Forms.csproj", buildSettings);
    });

Task("Pack")
    .IsDependentOn("Build")
    .Does(() => 
    {
        MSBuild("src/MagicGradients.Forms/MagicGradients.Forms.csproj", packSettings);
    });

RunTarget(target);