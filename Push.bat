@echo off
if [%1]==[] (
	echo Missing version parameter
	goto:eof
)

"tools\NuGet" Update -self
"tools\NuGet" Push "build\packages\Guidelines.DataAccess.Mongo.%1.nupkg"
"tools\NuGet" Push "build\packages\Guidelines.Core.%1.nupkg"
"tools\NuGet" Push "build\packages\Guidelines.Ioc.StructureMap.%1.nupkg"
"tools\NuGet" Push "build\packages\Guidelines.Mapping.AutoMapper.%1.nupkg"
"tools\NuGet" Push "build\packages\Guidelines.Logging.Log4Net.%1.nupkg"
"tools\NuGet" Push "build\packages\Guidelines.WebUI.%1.nupkg"
