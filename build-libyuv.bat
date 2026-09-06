@echo off
setlocal
cd /d "%~dp0"
rem Use an x64/ARM64 developer prompt, or set CC and CXX to LLVM-MinGW compilers.
rem CMake, Ninja, and (for x64) NASM must be on PATH.
set "RID=%~1"
if "%RID%"=="" set RID=win-x64
cmake -DRID=%RID% -P native/build.cmake
if errorlevel 1 exit /b 1
if not "%RID%"=="win-x64" exit /b 0
set "LIBYUV_SOURCE=%CD%\artifacts\libyuv"
dotnet build LibYuvSharp.Test\LibYuvSharp.Tests.csproj -c Release -p:Platform=x64
if errorlevel 1 exit /b 1
dotnet vstest LibYuvSharp.Test\bin\x64\Release\net10.0\LibYuvSharp.Tests.dll /Tests:Lennox.LibYuvSharp.Tests.CodeGeneration.GenerateClassDefinition
exit /b %ERRORLEVEL%
