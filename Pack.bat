@echo off
if not exist build (
	mkdir "build"
)
if not exist build\packages (
	mkdir "build\packages"
)

"tools\NuGet" Update -self
"tools\NuGet" Pack -OutputDirectory "build\packages" "src\DataAccess\DataAccess.csproj"
"tools\NuGet" Pack -OutputDirectory "build\packages" "src\Domain\Domain.csproj"
"tools\NuGet" Pack -OutputDirectory "build\packages" "src\Ioc\Ioc.csproj"
"tools\NuGet" Pack -OutputDirectory "build\packages" "src\Mapping\Mapping.csproj"
"tools\NuGet" Pack -OutputDirectory "build\packages" "src\WebUI\WebUI.csproj"
