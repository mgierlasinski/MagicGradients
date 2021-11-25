var target = Argument("target", "Pack");
var configuration = Argument("configuration", "Release");

Task("Build")
    .Does(() => 
    {
        var settings = new MSBuildSettings()
            .SetConfiguration(configuration)
            .WithRestore()
            .WithTarget("Rebuild");

        MSBuild("src/MagicGradients.Forms/MagicGradients.Forms.csproj", settings);
    });


Task("Pack")
    .IsDependentOn("Build")
    .Does(() => 
    {
        var settings = new MSBuildSettings()
            .SetConfiguration(configuration)
            .WithTarget("Pack")
            .WithProperty("PackageOutputPath", "C:\\Packages");

        MSBuild("src/MagicGradients.Forms/MagicGradients.Forms.csproj", settings);
    });

RunTarget(target);