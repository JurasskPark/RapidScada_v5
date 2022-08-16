@ECHO OFF
===================
ECHO TASKKILL ScadaAdmin
taskkill /im ScadaAdmin.exe /F
===================
ECHO BUILDING...
set msbuild="C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"
===================
ECHO COMPILE...
%msbuild% .\KpDbImportPlus.csproj /t:Build /p:Configuration=Release
===================
ECHO SERVICE STOP
NET STOP ScadaAgentService
NET STOP ScadaCommService
NET STOP ScadaServerService
===================
ECHO COPYING FILES...
IF EXIST ".\bin\Release\KpDbImportPlus.dll" COPY ".\bin\Release\KpDbImportPlus.dll" "C:\SCADA\ScadaAdmin\Lib\KpDbImportPlus.dll" /Y
IF EXIST ".\bin\Release\KpDbImportPlus.dll" COPY ".\bin\Release\KpDbImportPlus.dll" "C:\SCADA\ScadaComm\KP\KpDbImportPlus.dll" /Y
===================
ECHO SERVICE START
NET START ScadaAgentService
NET START ScadaCommService
NET START ScadaServerService
===================
ECHO START APP
"C:\SCADA\ScadaAdmin\ScadaAdmin.exe"



