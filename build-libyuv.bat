@echo off
setlocal
cd /d "%~dp0"
rem Run in an x64 C++ developer command prompt with CMake, Ninja, and NASM on PATH.
rem For LLVM-MinGW, set CC=x86_64-w64-mingw32-clang and CXX=x86_64-w64-mingw32-clang++.
set LIBYUV_REVISION=af1aaca84027a69ab25f251ade1bf1714b180d89
if not exist artifacts\libyuv\.git (
  git clone --no-checkout https://chromium.googlesource.com/libyuv/libyuv artifacts\libyuv
  if errorlevel 1 exit /b 1
)
git -C artifacts\libyuv fetch --depth 1 origin %LIBYUV_REVISION%
if errorlevel 1 exit /b 1
git -C artifacts\libyuv checkout --detach %LIBYUV_REVISION%
if errorlevel 1 exit /b 1
cmake -P native\download-jpeg.cmake
if errorlevel 1 exit /b 1
cmake -S artifacts\libjpeg-turbo-3.2.0 -B artifacts\jpeg-release -G Ninja -DCMAKE_BUILD_TYPE=Release -DENABLE_SHARED=OFF -DENABLE_STATIC=ON -DWITH_TURBOJPEG=OFF -DWITH_TOOLS=OFF -DREQUIRE_SIMD=ON "-DCMAKE_INSTALL_PREFIX=%CD%/artifacts/jpeg-install"
if errorlevel 1 exit /b 1
cmake --build artifacts\jpeg-release --target jpeg-static --parallel
if errorlevel 1 exit /b 1
cmake --install artifacts\jpeg-release
if errorlevel 1 exit /b 1
cmake -S native -B artifacts\native-release -G Ninja -DCMAKE_BUILD_TYPE=Release "-DLIBYUV_SOURCE_DIR=%CD%/artifacts/libyuv" "-DJPEG_ROOT=%CD%/artifacts/jpeg-install"
if errorlevel 1 exit /b 1
cmake --build artifacts\native-release --target yuv_shared --parallel
if errorlevel 1 exit /b 1
copy /y artifacts\native-release\libyuv\libyuv_internal.dll LibYuvSharp\lib\runtimes\win-x64\native\
if errorlevel 1 exit /b 1
for %%f in (LICENSE PATENTS AUTHORS) do copy /y artifacts\libyuv\%%f native\%%f >nul
if not exist native\libjpeg-turbo mkdir native\libjpeg-turbo
for %%f in (LICENSE.md README.ijg) do copy /y artifacts\libjpeg-turbo-3.2.0\%%f native\libjpeg-turbo\%%f >nul
set "LIBYUV_SOURCE=%CD%\artifacts\libyuv"
dotnet build LibYuvSharp.Test\LibYuvSharp.Tests.csproj -c Release -p:Platform=x64
if errorlevel 1 exit /b 1
dotnet vstest LibYuvSharp.Test\bin\x64\Release\netcoreapp3.0\LibYuvSharp.Tests.dll /Tests:Lennox.LibYuvSharp.Tests.CodeGeneration.GenerateClassDefinition
exit /b %ERRORLEVEL%
