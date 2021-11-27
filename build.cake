var target = Argument("target", "Pack");
var configuration = Argument("configuration", "Release");
var output = "C:\\Packages";

Task("LegacyBuild")
    .Does(() => 
    {
        DotNetCoreBuild("src/MagicGradients/MagicGradients.csproj", new DotNetCoreBuildSettings()
        {
            Configuration = configuration
        });
    });

Task("LegacyPack")
    .IsDependentOn("LegacyBuild")
    .Does(() => 
    {
        DotNetCorePack("src/MagicGradients/MagicGradients.csproj", new DotNetCorePackSettings()
        {
            Configuration = configuration,
            NoBuild = true,
            OutputDirectory = output
        });
    });

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
    .Does(() => 
    {
        var settings = new MSBuildSettings()
            .SetConfiguration(configuration)
            .WithRestore()
            //.WithTarget("Rebuild")
            .WithTarget("Pack")
            .WithProperty("PackageOutputPath", output);

        MSBuild("src/MagicGradients.Forms/MagicGradients.Forms.csproj", settings);
    });

/*
Task("PackNuGet")
    .Does(() => 
    {
        NuGetPack("src/MagicGradients.Forms/MagicGradients.Forms.csproj", new NuGetPackSettings()
        {
            OutputDirectory = output,
            ArgumentCustomization = args => args
                .Append("-Build")
                .Append("-Prop Configuration=" + configuration)
        });
    });
*/

RunTarget(target);