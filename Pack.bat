@echo off
if exist "build\packages" (
	rmdir /S /Q "build\packages"
)
mkdir "build\packages"

"tools\NuGet" update -self
"tools\NuGet" pack -symbols -outputDirectory "build\packages" "src\DataAccess.EntityFramework\DataAccess.EntityFramework.csproj"
"tools\NuGet" pack -symbols -outputDirectory "build\packages" "src\DataAccess.Mongo\DataAccess.Mongo.csproj"
"tools\NuGet" pack -symbols -outputDirectory "build\packages" "src\Core\Core.csproj"
"tools\NuGet" pack -symbols -outputDirectory "build\packages" "src\Ioc.StructureMap\Ioc.StructureMap.csproj"
"tools\NuGet" pack -symbols -outputDirectory "build\packages" "src\Mapping.AutoMapper\Mapping.AutoMapper.csproj"
"tools\NuGet" pack -symbols -outputDirectory "build\packages" "src\Logging.Log4Net\Logging.Log4Net.csproj"
"tools\NuGet" pack -symbols -outputDirectory "build\packages" "src\WebUI\WebUI.csproj"
