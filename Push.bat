@echo off
if [%1]==[] (
	echo Missing version parameter
	goto:eof
)

"tools\NuGet" Update -self
"tools\NuGet" Push "build\packages\Guidelines.DataAccess.%1.nupkg"
"tools\NuGet" Push "build\packages\Guidelines.Domain.%1.nupkg"
"tools\NuGet" Push "build\packages\Guidelines.Ioc.%1.nupkg"
"tools\NuGet" Push "build\packages\Guidelines.Mapping.%1.nupkg"
"tools\NuGet" Push "build\packages\Guidelines.WebUI.%1.nupkg"
