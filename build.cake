var target = Argument("target", "Pack");
var configuration = Argument("configuration", "Release");
var output = Argument("output", IsRunningOnWindows() ? "C:\\Packages" : "~/Packages");

Task("Clean")
    .Does(() => 
    {
        CleanDirectories("src/**/bin");
        CleanDirectories("src/**/obj");
    });

Task("MGBuild")
    //.IsDependentOn("Clean")
    .Does(() => 
    {
        DotNetCoreBuild("src/MagicGradients/MagicGradients.csproj", new DotNetCoreBuildSettings()
        {
            Configuration = configuration
        });
    });

Task("MGPack")
    .IsDependentOn("MGBuild")
    .Does(() => 
    {
        DotNetCorePack("src/MagicGradients/MagicGradients.csproj", new DotNetCorePackSettings()
        {
            Configuration = configuration,
            NoBuild = true,
            NoRestore = true,
            //IncludeSymbols = true,
            //SymbolPackageFormat = "snupkg",
            OutputDirectory = output
        });
    });

Task("Pack")
    //.IsDependentOn("Clean")
    .Does(() => 
    {
        var settings = new MSBuildSettings()
            .SetConfiguration(configuration)
            //.SetIncludeSymbols(true)
            //.SetSymbolPackageFormat("snupkg")
            .WithRestore()
            .WithTarget("Pack")
            .WithProperty("PackageOutputPath", output);

        MSBuild("src/MagicGradients.Core/MagicGradients.Core.csproj", settings);
        MSBuild("src/MagicGradients.Forms/MagicGradients.Forms.csproj", settings);
        MSBuild("src/MagicGradients.Forms.Skia/MagicGradients.Forms.Skia.csproj", settings);
    });

RunTarget(target);