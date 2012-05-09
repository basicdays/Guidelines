@echo off

"tools\NuGet" Update -self
"tools\NuGet" Push "build\packages\Guidelines.DataAccess.0.1.0.0.nupkg"
"tools\NuGet" Push "build\packages\Guidelines.Domain.0.1.0.0.nupkg"
"tools\NuGet" Push "build\packages\Guidelines.Ioc.0.1.0.0.nupkg"
"tools\NuGet" Push "build\packages\Guidelines.Mapping.0.1.0.0.nupkg"
"tools\NuGet" Push "build\packages\Guidelines.WebUI.0.1.0.0.nupkg"
