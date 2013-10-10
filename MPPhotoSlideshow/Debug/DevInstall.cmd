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
REM copy file
xcopy.exe "..\bin\debug\MPPhotoSlideshow.dll" "C:\Program Files (x86)\Team MediaPortal\MediaPortal\plugins\Windows\" /y
xcopy.exe "..\..\MPPhotoSlideshowCommon\bin\Debug\MPPhotoSlideshowCommon.dll" "C:\Program Files (x86)\Team MediaPortal\MediaPortal\" /y
xcopy.exe "..\MPPhotoSlideshow.xml" "C:\ProgramData\Team MediaPortal\MediaPortal\skin\Titan\" /y