@echo off
if exist "build\packages" (
	rmdir /S /Q "build\packages"
)
mkdir "build\packages"

"tools\NuGet" update -self
"tools\NuGet" pack -symbols -outputDirectory "build\packages" "src\DataAccess\DataAccess.csproj"
"tools\NuGet" pack -symbols -outputDirectory "build\packages" "src\Domain\Domain.csproj"
"tools\NuGet" pack -symbols -outputDirectory "build\packages" "src\Ioc\Ioc.csproj"
"tools\NuGet" pack -symbols -outputDirectory "build\packages" "src\Mapping\Mapping.csproj"
"tools\NuGet" pack -symbols -outputDirectory "build\packages" "src\WebUI\WebUI.csproj"
