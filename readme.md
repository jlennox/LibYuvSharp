# About

LibYuvSharp provides a calling interface to Google's
[libyuv](https://chromium.googlesource.com/libyuv/libyuv/) for SIMD accelerated
color space conversions in dotnet.

The NuGet package and repository include a Windows x64 DLL built from **libyuv
1971**, commit `af1aaca84027a69ab25f251ade1bf1714b180d89` (2026-09-04).
Upstream has no release tags; this is the latest main revision checked on that
date. JPEG/MJPEG decoding is included through libjpeg-turbo 3.2.0.

# How to use

* [Add the NuGet package.](https://www.nuget.org/packages/Lennox.LibYuvSharp)
* [Reference the sample code and tests.](LibYuvSharp.Test/LibYuvTests.cs)

# Building the Windows DLL and bindings

Install Git, CMake 3.16 or newer, Ninja, NASM, a current x64 C++ compiler, and a .NET SDK
that can build the existing project targets. Use an x64 developer command prompt,
then run:

```bat
build-libyuv.bat
```

Alternatively, put an LLVM-MinGW toolchain's `bin` directory on PATH and run:

```bat
set CC=x86_64-w64-mingw32-clang
set CXX=x86_64-w64-mingw32-clang++
build-libyuv.bat
```

The bundled DLL was built with LLVM-MinGW 20260826 (Clang 23.1.0), in Release
mode with libjpeg-turbo and compiler runtime libraries linked statically. It
requires only Windows system/UCRT DLLs. The script fetches the pinned upstream
commit into the ignored `artifacts` directory, builds `libyuv_internal.dll`,
copies upstream license files, and runs the existing C# NUnit code generator.
Use fresh `artifacts/native-release` and `artifacts/jpeg-release` directories when changing compilers.

To regenerate only the bindings after building the DLL:

```bat
set LIBYUV_SOURCE=%CD%\artifacts\libyuv
dotnet build LibYuvSharp.Test\LibYuvSharp.Tests.csproj -c Release -p:Platform=x64
dotnet vstest LibYuvSharp.Test\bin\x64\Release\netcoreapp3.0\LibYuvSharp.Tests.dll /Tests:Lennox.LibYuvSharp.Tests.CodeGeneration.GenerateClassDefinition
set LIBYUV_SOURCE=
```

The generator updates `LibYuvSharp/LibYuv.cs` using public header declarations
and actual DLL exports.

```bat
dotnet test LibYuvSharp.Test\LibYuvSharp.Tests.csproj -c Release -p:Platform=x64
dotnet pack LibYuvSharp\LibYuvSharp.csproj -c Release -p:Platform=x64
```

# Welcome contributions

Native libraries for Linux and macOS are welcome.

# Third-party notices

This software is based in part on the work of the Independent JPEG Group.
