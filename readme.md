# About

LibYuvSharp is a library for .net framework and dotnet core that provides a
basic calling interface into [libyuv](https://chromium.googlesource.com/libyuv/libyuv/)

libyuv is a highly efficient SIMD accelerated library for doing color space
conversions on the CPU.

The precompiled Window's dll is included with the nuget package/repo. The
source used was from commit `53e014c99d6f59647c57b70b3fa65ad3dd59ce08` (2019-11-07).

# How to use

* [Add the nuget package.](https://www.nuget.org/packages/Lennox.LibYuvSharp)
* [Reference the sample code.](LibYuvSharp.Test/LibYuvTests.cs)

# Welcome contributions

Including the libraries for linux and mac os would be great if anyone would
like to contribute them.

# How to compile LibYuv as a Window's dll

Begin with the [official instructions](https://github.com/frankpapenmeier/libyuv/blob/master/docs/getting_started.md)

Fix [a bug in `src/BUILD.gn`](https://bugs.chromium.org/p/libyuv/issues/detail?id=849) if it's not yet fixed:

change:
```
  if (!is_ios) {
    defines += [ "HAVE_JPEG" ]
```

to:
```
  if (!is_ios && !libyuv_disable_jpeg) {
    defines += [ "HAVE_JPEG" ]
```

Change:
`static_library("libyuv_internal") {`
to
`shared_library("libyuv_internal") {`

Update the `defines` declaration nested under `shared_library("libyuv_internal")` to include LIBYUV_BUILDING_SHARED_LIBRARY:
`defines = [ "LIBYUV_BUILDING_SHARED_LIBRARY" ]`

Fix [a bug in `convert_from.h`](https://bugs.chromium.org/p/libyuv/issues/detail?id=850) if not yet fixed:

Inside `src/` run: `call gn gen out\Release "--args=is_debug=false libyuv_disable_jpeg=true target_cpu=\"x64\""`