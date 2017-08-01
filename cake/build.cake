var configuration = Argument("configuration", "Debug");

Task("BuildCommon")
.Description("Build common solution")
.Does(() =>
{
    NuGetRestore("./sources/Common/Common.sln");
    DotNetBuild("./sources/Common/Common.sln", x => x
        .SetConfiguration(configuration)
        .SetVerbosity(Verbosity.Minimal)
        .WithTarget("build")
        .WithProperty("TreatWarningsAsErrors", "false")
    );
	
	var sourceFolder = "./sources/Common/Assemblies/*.*";
	var targetFolder = "./assemblies";
	CopyFiles(sourceFolder, targetFolder);
});

Task("BuildServer")
.Description("Build server solution")
.IsDependentOn("BuildCommon")
.Does(() =>
{
    NuGetRestore("./sources/Server/Server.sln");
    DotNetBuild("./sources/Server/Server.sln", x => x
        .SetConfiguration(configuration)
        .SetVerbosity(Verbosity.Minimal)
        .WithTarget("build")
        .WithProperty("TreatWarningsAsErrors", "false")
    );
	
	var sourceFolder = "./sources/Server/Assemblies/*.*";
	var targetFolder = "./assemblies";
	CopyFiles(sourceFolder, targetFolder);
});

Task("BuildClient")
.Description("Build client solution")
.IsDependentOn("BuildServer")
.Does(() =>
{
    NuGetRestore("./sources/Client/Client.sln");
    DotNetBuild("./sources/Client/Client.sln", x => x
        .SetConfiguration(configuration)
        .SetVerbosity(Verbosity.Minimal)
        .WithTarget("build")
        .WithProperty("TreatWarningsAsErrors", "false")
    );
});

RunTarget("BuildClient");