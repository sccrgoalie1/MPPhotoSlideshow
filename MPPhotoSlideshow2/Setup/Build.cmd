SET BUILD_TYPE=Release
if "%1" == "Debug" set BUILD_TYPE=Debug
REM Determine whether we are on an 32 or 64 bit machine
if "%PROCESSOR_ARCHITECTURE%"=="x86" if "%PROCESSOR_ARCHITEW6432%"=="" goto x86

set ProgramFilesPath=%ProgramFiles(x86)%

goto startInstall

:x86

set ProgramFilesPath=%ProgramFiles%

:startInstall

pushd "%~dp0"

SET WIX_BUILD_LOCATION=%ProgramFilesPath%\WiX Toolset v3.8\bin
SET INTERMEDIATE_PATH=..\obj\%BUILD_TYPE%
SET OUTPUTNAME=..\bin\%BUILD_TYPE%\MPPhotoSlideshow2.msi
SET BUNDLEOUTPUTNAME=..\bin\%BUILD_TYPE%\MPPhotoSlideshow2.exe

REM Cleanup leftover intermediate files

del /f /q "%INTERMEDIATE_PATH%\*.wixobj"
del /f /q "%OUTPUTNAME%"

REM Build the MSI for the setup package
REM This next part looks like 2 separate lines but should be typed in as a single line.

"%WIX_BUILD_LOCATION%\candle.exe" Setup.wxs -dBuildType=%BUILD_TYPE% -out "%INTERMEDIATE_PATH%\Setup.wixobj"

REM This next part looks like 4 separate lines but should be typed in as a single line.

"%WIX_BUILD_LOCATION%\light.exe" "%INTERMEDIATE_PATH%\setup.wixobj" -cultures:en-US -ext "%WIX_BUILD_LOCATION%\WixUIExtension.dll" -ext "%WIX_BUILD_LOCATION%\WixUtilExtension.dll" -ext "%WIX_BUILD_LOCATION%\WixNetFxExtension.dll" -loc setup-en-us.wxl -out "%OUTPUTNAME%"

REM "%WIX_BUILD_LOCATION%\candle.exe" Bundle.wxs -dBuildType=%BUILD_TYPE% -out "%INTERMEDIATE_PATH%\Bundle.wixobj" -ext "%WIX_BUILD_LOCATION%\WixUtilExtension.dll" -ext "%WIX_BUILD_LOCATION%\WixBalExtension.dll"

REM "%WIX_BUILD_LOCATION%\light.exe" "%INTERMEDIATE_PATH%\bundle.wixobj" -cultures:en-US -ext "%WIX_BUILD_LOCATION%\WixUtilExtension.dll" -ext "%WIX_BUILD_LOCATION%\WixUIExtension.dll" -ext "%WIX_BUILD_LOCATION%\WixNetFxExtension.dll" -ext "%WIX_BUILD_LOCATION%\WixBalExtension.dll" -out "%BUNDLEOUTPUTNAME%"
Popd