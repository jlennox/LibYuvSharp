# About

LibYuvSharp provides a calling interface to Google's
[libyuv](https://chromium.googlesource.com/libyuv/libyuv/) for SIMD accelerated
color space conversions in dotnet.

The native libraries are built from **libyuv
1971**, commit `af1aaca84027a69ab25f251ade1bf1714b180d89` (2026-09-04).
Upstream has no release tags; this is the latest main revision checked on that
date. JPEG/MJPEG decoding is included through libjpeg-turbo 3.2.0.

# How to use

* [Add the NuGet package.](https://www.nuget.org/packages/Lennox.LibYuvSharp)
* [Reference the sample code and tests.](LibYuvSharp.Test/LibYuvTests.cs)

# Native platforms

The native build workflow produces one NuGet package with these runtimes:

| Platform | Architectures | Build baseline |
| --- | --- | --- |
| Windows | x64, ARM64 | UCRT |
| Linux | x64, ARM64 | Ubuntu 22.04 (glibc) |
| macOS | x64, ARM64 | macOS 11 |
| Android | x64, ARM64 | API 23, 16 KB page alignment |

JPEG is included on every platform. Desktop .NET selects the native library
through NuGet runtime assets; Android builds include it in the APK.

Run **Build native libraries** from GitHub Actions to rebuild all eight runtimes
and download the `nuget-multiplatform` artifact. It tests the resulting package
on each desktop architecture and an Android x64 emulator. Both Android ABIs
are checked in the APK; Android ARM64 still needs a device test.
The regular NuGet workflow packages the binaries already in the checkout.

To build a single runtime, install Git, CMake, Ninja and the target compiler,
plus NASM for x64 JPEG assembly. For example, on Linux:

```sh
cmake -DRID=linux-x64 -P native/build.cmake
```

For ARM64 from x64 Linux, install `gcc-aarch64-linux-gnu` and
`g++-aarch64-linux-gnu`, then pass
`-DRID=linux-arm64 -DTOOLCHAIN_FILE="$PWD/native/toolchains/linux-arm64.cmake"`.
Build Linux release binaries on Ubuntu 22.04 to keep the glibc baseline.
For Android, pass `-DRID=android-arm64` (or `android-x64`) and
`-DANDROID_NDK=/path/to/android-ndk-r28c`. For macOS, run on a Mac with
`-DRID=osx-arm64` or `-DRID=osx-x64`.

Builds go in `artifacts/native/<rid>` and copy the native library to
`LibYuvSharp/lib/runtimes/<rid>/native`. Use a fresh build directory when
changing compilers. Set `-DLIBYUV_BUILD_ROOT=/path/to/builds` if needed.

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
For Windows ARM64, select an ARM64 compiler (for LLVM-MinGW, use the
`aarch64-w64-mingw32-` prefix) and run `build-libyuv.bat win-arm64`.
Only the Windows x64 build regenerates the bindings.

To regenerate only the bindings after building the DLL:

```bat
set LIBYUV_SOURCE=%CD%\artifacts\libyuv
dotnet build LibYuvSharp.Test\LibYuvSharp.Tests.csproj -c Release -p:Platform=x64
dotnet vstest LibYuvSharp.Test\bin\x64\Release\net10.0\LibYuvSharp.Tests.dll /Tests:Lennox.LibYuvSharp.Tests.CodeGeneration.GenerateClassDefinition
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
