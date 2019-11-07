using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Lennox.LibYuvSharp
{
    public enum FilterMode
    {
        /// <summary>
        /// Point sample; Fastest.
        /// </summary>
        None = 0,
        /// <summary>
        /// Filter horizontally only.
        /// </summary>
        Linear = 1,
        /// <summary>
        /// Faster than box, but lower quality scaling down.
        /// </summary>
        Bilinear = 2,
        /// <summary>
        /// Highest quality.
        /// </summary>
        Box = 3
    }

    public enum RotationMode
    {
        Rotate0 = 0,
        Rotate90 = 90,
        Rotate180 = 180,
        Rotate270 = 270,

        // Deprecated.
        None = 0,
        Clockwise = 90,
        CounterClockwise = 270,
    }

    [SuppressUnmanagedCodeSecurity]
    public static unsafe class LibYuv
    {
        private const string _path = "libyuv_internal.dll";

        // C:\code2\libyuv\src\include\libyuv\compare.h:22
        /// <summary>
        /// Compute a hash for specified memory. Seed of 5381 recommended.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern uint HashDjb2(byte* src, ulong count, uint seed);

        // C:\code2\libyuv\src\include\libyuv\compare.h:28
        /// <summary>
        /// Hamming Distance
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern ulong ComputeHammingDistance(byte* src_a,
        byte* src_b,
        int count);

        // C:\code2\libyuv\src\include\libyuv\compare.h:36
        /// <summary>
        /// Scan an opaque argb image and return fourcc based on alpha offset.
        /// Returns FOURCC_ARGB, FOURCC_BGRA, or 0 if unknown.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern uint ARGBDetect(byte* argb,
        int stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\compare.h:42
        /// <summary>
        /// Sum Square Error - used to compute Mean Square Error or PSNR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern ulong ComputeSumSquareError(byte* src_a,
        byte* src_b,
        int count);

        // C:\code2\libyuv\src\include\libyuv\compare.h:50
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern ulong ComputeSumSquareErrorPlane(byte* src_a,
        int stride_a,
        byte* src_b,
        int stride_b,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\compare.h:55
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern double SumSquareErrorToPsnr(ulong sse, ulong count);

        // C:\code2\libyuv\src\include\libyuv\compare.h:63
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern double CalcFramePsnr(byte* src_a,
        int stride_a,
        byte* src_b,
        int stride_b,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\compare.h:79
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern double I420Psnr(byte* src_y_a,
        int stride_y_a,
        byte* src_u_a,
        int stride_u_a,
        byte* src_v_a,
        int stride_v_a,
        byte* src_y_b,
        int stride_y_b,
        byte* src_u_b,
        int stride_u_b,
        byte* src_v_b,
        int stride_v_b,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\compare.h:87
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern double CalcFrameSsim(byte* src_a,
        int stride_a,
        byte* src_b,
        int stride_b,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\compare.h:103
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern double I420Ssim(byte* src_y_a,
        int stride_y_a,
        byte* src_u_a,
        int stride_u_a,
        byte* src_v_a,
        int stride_v_a,
        byte* src_y_b,
        int stride_y_b,
        byte* src_u_b,
        int stride_u_b,
        byte* src_v_b,
        int stride_v_b,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:42
        /// <summary>
        /// TODO(fbarchard): fix WebRTC source to include following libyuv headers:
        /// Convert I444 to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I444ToI420(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:57
        /// <summary>
        /// Convert I444 to NV21.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I444ToNV21(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_vu,
        int dst_stride_vu,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:74
        /// <summary>
        /// Convert I422 to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422ToI420(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:89
        /// <summary>
        /// Convert I422 to NV21.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422ToNV21(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_vu,
        int dst_stride_vu,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:107
        /// <summary>
        /// Copy I420 to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420Copy(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:126
        /// <summary>
        /// Copy I010 to I010
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I010Copy(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        ushort* dst_y,
        int dst_stride_y,
        ushort* dst_u,
        int dst_stride_u,
        ushort* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:144
        /// <summary>
        /// Convert 10 bit YUV to 8 bit
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I010ToI420(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:157
        /// <summary>
        /// Convert I400 (grey) to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I400ToI420(byte* src_y,
        int src_stride_y,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:168
        /// <summary>
        /// Convert I400 (grey) to NV21.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I400ToNV21(byte* src_y,
        int src_stride_y,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_vu,
        int dst_stride_vu,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:185
        /// <summary>
        /// Convert NV12 to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV12ToI420(byte* src_y,
        int src_stride_y,
        byte* src_uv,
        int src_stride_uv,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:200
        /// <summary>
        /// Convert NV21 to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV21ToI420(byte* src_y,
        int src_stride_y,
        byte* src_vu,
        int src_stride_vu,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:213
        /// <summary>
        /// Convert YUY2 to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int YUY2ToI420(byte* src_yuy2,
        int src_stride_yuy2,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:226
        /// <summary>
        /// Convert UYVY to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int UYVYToI420(byte* src_uyvy,
        int src_stride_uyvy,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:237
        /// <summary>
        /// Convert AYUV to NV12.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int AYUVToNV12(byte* src_ayuv,
        int src_stride_ayuv,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:248
        /// <summary>
        /// Convert AYUV to NV21.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int AYUVToNV21(byte* src_ayuv,
        int src_stride_ayuv,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_vu,
        int dst_stride_vu,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:261
        /// <summary>
        /// Convert M420 to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int M420ToI420(byte* src_m420,
        int src_stride_m420,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:279
        /// <summary>
        /// Convert Android420 to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int Android420ToI420(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        int src_pixel_stride_uv,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:292
        /// <summary>
        /// ARGB little endian (bgra in memory) to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToI420(byte* src_argb,
        int src_stride_argb,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:305
        /// <summary>
        /// BGRA little endian (argb in memory) to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int BGRAToI420(byte* src_bgra,
        int src_stride_bgra,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:318
        /// <summary>
        /// ABGR little endian (rgba in memory) to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ABGRToI420(byte* src_abgr,
        int src_stride_abgr,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:331
        /// <summary>
        /// RGBA little endian (abgr in memory) to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RGBAToI420(byte* src_rgba,
        int src_stride_rgba,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:344
        /// <summary>
        /// RGB little endian (bgr in memory) to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RGB24ToI420(byte* src_rgb24,
        int src_stride_rgb24,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:357
        /// <summary>
        /// RGB little endian (bgr in memory) to J420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RGB24ToJ420(byte* src_rgb24,
        int src_stride_rgb24,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:370
        /// <summary>
        /// RGB big endian (rgb in memory) to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RAWToI420(byte* src_raw,
        int src_stride_raw,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:383
        /// <summary>
        /// RGB16 (RGBP fourcc) little endian to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RGB565ToI420(byte* src_rgb565,
        int src_stride_rgb565,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:396
        /// <summary>
        /// RGB15 (RGBO fourcc) little endian to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGB1555ToI420(byte* src_argb1555,
        int src_stride_argb1555,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:409
        /// <summary>
        /// RGB12 (R444 fourcc) little endian to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGB4444ToI420(byte* src_argb4444,
        int src_stride_argb4444,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:418
        /// <summary>
        /// RGB little endian (bgr in memory) to J400.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RGB24ToJ400(byte* src_rgb24,
        int src_stride_rgb24,
        byte* dst_yj,
        int dst_stride_yj,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:496
        /// <summary>
        /// Convert camera sample to I420 with cropping, rotation and vertical flip.
        /// "src_size" is needed to parse MJPG.
        /// "dst_stride_y" number of bytes in a row of the dst_y plane.
        /// Normally this would be the same as dst_width, with recommended alignment
        /// to 16 bytes for better efficiency.
        /// If rotation of 90 or 270 is used, stride is affected. The caller should
        /// allocate the I420 buffer according to rotation.
        /// "dst_stride_u" number of bytes in a row of the dst_u plane.
        /// Normally this would be the same as (dst_width + 1) / 2, with
        /// recommended alignment to 16 bytes for better efficiency.
        /// If rotation of 90 or 270 is used, stride is affected.
        /// "crop_x" and "crop_y" are starting position for cropping.
        /// To center, crop_x = (src_width - dst_width) / 2
        /// crop_y = (src_height - dst_height) / 2
        /// "src_width" / "src_height" is size of src_frame in pixels.
        /// "src_height" can be negative indicating a vertically flipped image source.
        /// "crop_width" / "crop_height" is the size to crop the src to.
        /// Must be less than or equal to src_width/src_height
        /// Cropping parameters are pre-rotation.
        /// "rotation" can be 0, 90, 180 or 270.
        /// "fourcc" is a fourcc. ie 'I420', 'YUY2'
        /// Returns 0 for successful; -1 for invalid parameter. Non-zero for failure.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ConvertToI420(byte* sample,
        IntPtr sample_size,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int crop_x,
        int crop_y,
        int src_width,
        int src_height,
        int crop_width,
        int crop_height,
        RotationMode rotation,
        uint fourcc);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:37
        /// <summary>
        /// TODO(fbarchard): This set of functions should exactly match convert.h
        /// TODO(fbarchard): Add tests. Create random content of right size and convert
        /// with C vs Opt and or to I420 and compare.
        /// TODO(fbarchard): Some of these functions lack parameter setting.
        /// Alias.
        /// Copy ARGB to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBCopy(byte* src_argb,
        int src_stride_argb,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:51
        /// <summary>
        /// Convert I420 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToARGB(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:64
        /// <summary>
        /// Convert I420 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToABGR(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:77
        /// <summary>
        /// Convert J420 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int J420ToARGB(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:90
        /// <summary>
        /// Convert J420 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int J420ToABGR(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:103
        /// <summary>
        /// Convert H420 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int H420ToARGB(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:116
        /// <summary>
        /// Convert H420 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int H420ToABGR(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:129
        /// <summary>
        /// Convert U420 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int U420ToARGB(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:142
        /// <summary>
        /// Convert U420 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int U420ToABGR(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:155
        /// <summary>
        /// Convert I010 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I010ToARGB(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:181
        /// <summary>
        /// Convert I010 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I010ToABGR(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:194
        /// <summary>
        /// Convert H010 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int H010ToARGB(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:207
        /// <summary>
        /// Convert H010 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int H010ToABGR(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:220
        /// <summary>
        /// Convert U010 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int U010ToARGB(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:233
        /// <summary>
        /// Convert U010 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int U010ToABGR(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:246
        /// <summary>
        /// Convert I422 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422ToARGB(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:259
        /// <summary>
        /// Convert I444 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I444ToARGB(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:272
        /// <summary>
        /// Convert U444 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int U444ToARGB(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:285
        /// <summary>
        /// Convert J444 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int J444ToARGB(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:298
        /// <summary>
        /// Convert I444 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I444ToABGR(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:314
        /// <summary>
        /// Convert I420 with Alpha to preattenuated ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420AlphaToARGB(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* src_a,
        int src_stride_a,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height,
        int attenuate);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:330
        /// <summary>
        /// Convert I420 with Alpha to preattenuated ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420AlphaToABGR(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* src_a,
        int src_stride_a,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height,
        int attenuate);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:339
        /// <summary>
        /// Convert I400 (grey) to ARGB.  Reverse of ARGBToI400.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I400ToARGB(byte* src_y,
        int src_stride_y,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:348
        /// <summary>
        /// Convert J400 (jpeg grey) to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int J400ToARGB(byte* src_y,
        int src_stride_y,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:362
        /// <summary>
        /// Alias.
        /// Convert NV12 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV12ToARGB(byte* src_y,
        int src_stride_y,
        byte* src_uv,
        int src_stride_uv,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:373
        /// <summary>
        /// Convert NV21 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV21ToARGB(byte* src_y,
        int src_stride_y,
        byte* src_vu,
        int src_stride_vu,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:384
        /// <summary>
        /// Convert NV12 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV12ToABGR(byte* src_y,
        int src_stride_y,
        byte* src_uv,
        int src_stride_uv,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:395
        /// <summary>
        /// Convert NV21 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV21ToABGR(byte* src_y,
        int src_stride_y,
        byte* src_vu,
        int src_stride_vu,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:406
        /// <summary>
        /// Convert NV12 to RGB24.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV12ToRGB24(byte* src_y,
        int src_stride_y,
        byte* src_uv,
        int src_stride_uv,
        byte* dst_rgb24,
        int dst_stride_rgb24,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:417
        /// <summary>
        /// Convert NV21 to RGB24.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV21ToRGB24(byte* src_y,
        int src_stride_y,
        byte* src_vu,
        int src_stride_vu,
        byte* dst_rgb24,
        int dst_stride_rgb24,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:428
        /// <summary>
        /// Convert NV21 to YUV24.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV21ToYUV24(byte* src_y,
        int src_stride_y,
        byte* src_vu,
        int src_stride_vu,
        byte* dst_yuv24,
        int dst_stride_yuv24,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:439
        /// <summary>
        /// Convert NV12 to RAW.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV12ToRAW(byte* src_y,
        int src_stride_y,
        byte* src_uv,
        int src_stride_uv,
        byte* dst_raw,
        int dst_stride_raw,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:450
        /// <summary>
        /// Convert NV21 to RAW.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV21ToRAW(byte* src_y,
        int src_stride_y,
        byte* src_vu,
        int src_stride_vu,
        byte* dst_raw,
        int dst_stride_raw,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:459
        /// <summary>
        /// Convert M420 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int M420ToARGB(byte* src_m420,
        int src_stride_m420,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:468
        /// <summary>
        /// Convert YUY2 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int YUY2ToARGB(byte* src_yuy2,
        int src_stride_yuy2,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:477
        /// <summary>
        /// Convert UYVY to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int UYVYToARGB(byte* src_uyvy,
        int src_stride_uyvy,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:490
        /// <summary>
        /// Convert J422 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int J422ToARGB(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:503
        /// <summary>
        /// Convert J422 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int J422ToABGR(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:516
        /// <summary>
        /// Convert H422 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int H422ToARGB(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:529
        /// <summary>
        /// Convert U422 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int U422ToARGB(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:542
        /// <summary>
        /// Convert H422 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int H422ToABGR(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:568
        /// <summary>
        /// Convert I010 to AR30.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I010ToAR30(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_ar30,
        int dst_stride_ar30,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:581
        /// <summary>
        /// Convert H010 to AR30.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int H010ToAR30(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_ar30,
        int dst_stride_ar30,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:594
        /// <summary>
        /// Convert U010 to AR30.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int U010ToAR30(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_ar30,
        int dst_stride_ar30,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:607
        /// <summary>
        /// Convert I010 to AB30.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I010ToAB30(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_ab30,
        int dst_stride_ab30,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:620
        /// <summary>
        /// Convert H010 to AB30.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int H010ToAB30(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_ab30,
        int dst_stride_ab30,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:633
        /// <summary>
        /// Convert U010 to AB30.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int U010ToAB30(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_ab30,
        int dst_stride_ab30,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:642
        /// <summary>
        /// BGRA little endian (argb in memory) to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int BGRAToARGB(byte* src_bgra,
        int src_stride_bgra,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:651
        /// <summary>
        /// ABGR little endian (rgba in memory) to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ABGRToARGB(byte* src_abgr,
        int src_stride_abgr,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:660
        /// <summary>
        /// RGBA little endian (abgr in memory) to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RGBAToARGB(byte* src_rgba,
        int src_stride_rgba,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:672
        /// <summary>
        /// Deprecated function name.
        /// RGB little endian (bgr in memory) to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RGB24ToARGB(byte* src_rgb24,
        int src_stride_rgb24,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:681
        /// <summary>
        /// RGB big endian (rgb in memory) to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RAWToARGB(byte* src_raw,
        int src_stride_raw,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:690
        /// <summary>
        /// RGB big endian (rgb in memory) to RGBA.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RAWToRGBA(byte* src_raw,
        int src_stride_raw,
        byte* dst_rgba,
        int dst_stride_rgba,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:699
        /// <summary>
        /// RGB16 (RGBP fourcc) little endian to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RGB565ToARGB(byte* src_rgb565,
        int src_stride_rgb565,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:708
        /// <summary>
        /// RGB15 (RGBO fourcc) little endian to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGB1555ToARGB(byte* src_argb1555,
        int src_stride_argb1555,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:717
        /// <summary>
        /// RGB12 (R444 fourcc) little endian to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGB4444ToARGB(byte* src_argb4444,
        int src_stride_argb4444,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:731
        /// <summary>
        /// Aliases
        /// Convert AR30 To ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int AR30ToARGB(byte* src_ar30,
        int src_stride_ar30,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:740
        /// <summary>
        /// Convert AR30 To ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int AR30ToABGR(byte* src_ar30,
        int src_stride_ar30,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:749
        /// <summary>
        /// Convert AR30 To AB30.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int AR30ToAB30(byte* src_ar30,
        int src_stride_ar30,
        byte* dst_ab30,
        int dst_stride_ab30,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:777
        /// <summary>
        /// Convert Android420 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int Android420ToARGB(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        int src_pixel_stride_uv,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:791
        /// <summary>
        /// Convert Android420 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int Android420ToABGR(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        int src_pixel_stride_uv,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:827
        /// <summary>
        /// Convert camera sample to ARGB with cropping, rotation and vertical flip.
        /// "sample_size" is needed to parse MJPG.
        /// "dst_stride_argb" number of bytes in a row of the dst_argb plane.
        /// Normally this would be the same as dst_width, with recommended alignment
        /// to 16 bytes for better efficiency.
        /// If rotation of 90 or 270 is used, stride is affected. The caller should
        /// allocate the I420 buffer according to rotation.
        /// "dst_stride_u" number of bytes in a row of the dst_u plane.
        /// Normally this would be the same as (dst_width + 1) / 2, with
        /// recommended alignment to 16 bytes for better efficiency.
        /// If rotation of 90 or 270 is used, stride is affected.
        /// "crop_x" and "crop_y" are starting position for cropping.
        /// To center, crop_x = (src_width - dst_width) / 2
        /// crop_y = (src_height - dst_height) / 2
        /// "src_width" / "src_height" is size of src_frame in pixels.
        /// "src_height" can be negative indicating a vertically flipped image source.
        /// "crop_width" / "crop_height" is the size to crop the src to.
        /// Must be less than or equal to src_width/src_height
        /// Cropping parameters are pre-rotation.
        /// "rotation" can be 0, 90, 180 or 270.
        /// "fourcc" is a fourcc. ie 'I420', 'YUY2'
        /// Returns 0 for successful; -1 for invalid parameter. Non-zero for failure.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ConvertToARGB(byte* sample,
        IntPtr sample_size,
        byte* dst_argb,
        int dst_stride_argb,
        int crop_x,
        int crop_y,
        int src_width,
        int src_height,
        int crop_width,
        int crop_height,
        RotationMode rotation,
        uint fourcc);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:39
        /// <summary>
        /// See Also convert.h for conversions from formats to I420.
        /// Convert 8 bit YUV to 10 bit.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToI010(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        ushort* dst_y,
        int dst_stride_y,
        ushort* dst_u,
        int dst_stride_u,
        ushort* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:55
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToI422(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:71
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToI444(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:80
        /// <summary>
        /// Copy to I400. Source can be I420, I422, I444, I400, NV12 or NV21.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I400Copy(byte* src_y,
        int src_stride_y,
        byte* dst_y,
        int dst_stride_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:94
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToNV12(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:108
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToNV21(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_vu,
        int dst_stride_vu,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:120
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToYUY2(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_yuy2,
        int dst_stride_yuy2,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:132
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToUYVY(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_uyvy,
        int dst_stride_uyvy,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:156
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToBGRA(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_bgra,
        int dst_stride_bgra,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:180
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToRGBA(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_rgba,
        int dst_stride_rgba,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:192
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToRGB24(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_rgb24,
        int dst_stride_rgb24,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:204
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToRAW(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_raw,
        int dst_stride_raw,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:216
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int H420ToRGB24(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_rgb24,
        int dst_stride_rgb24,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:228
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int H420ToRAW(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_raw,
        int dst_stride_raw,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:240
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int J420ToRGB24(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_rgb24,
        int dst_stride_rgb24,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:252
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int J420ToRAW(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_raw,
        int dst_stride_raw,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:264
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToRGB565(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_rgb565,
        int dst_stride_rgb565,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:276
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int J420ToRGB565(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_rgb565,
        int dst_stride_rgb565,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:288
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int H420ToRGB565(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_rgb565,
        int dst_stride_rgb565,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:300
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422ToRGB565(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_rgb565,
        int dst_stride_rgb565,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:317
        /// <summary>
        /// Convert I420 To RGB565 with 4x4 dither matrix (16 bytes).
        /// Values in dither matrix from 0 to 7 recommended.
        /// The order of the dither matrix is first byte is upper left.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToRGB565Dither(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_rgb565,
        int dst_stride_rgb565,
        byte* dither4x4,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:329
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToARGB1555(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_argb1555,
        int dst_stride_argb1555,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:341
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToARGB4444(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_argb4444,
        int dst_stride_argb4444,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:354
        /// <summary>
        /// Convert I420 to AR30.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToAR30(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_ar30,
        int dst_stride_ar30,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:367
        /// <summary>
        /// Convert H420 to AR30.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int H420ToAR30(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_ar30,
        int dst_stride_ar30,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:383
        /// <summary>
        /// Convert I420 to specified format.
        /// "dst_sample_stride" is bytes in a row for the destination. Pass 0 if the
        /// buffer has contiguous rows. Can be negative. A multiple of 16 is optimal.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ConvertFromI420(byte* y,
        int y_stride,
        byte* u,
        int u_stride,
        byte* v,
        int v_stride,
        byte* dst_sample,
        int dst_sample_stride,
        int width,
        int height,
        uint fourcc);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:37
        /// <summary>
        /// Convert ARGB To BGRA.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToBGRA(byte* src_argb,
        int src_stride_argb,
        byte* dst_bgra,
        int dst_stride_bgra,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:46
        /// <summary>
        /// Convert ARGB To ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToABGR(byte* src_argb,
        int src_stride_argb,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:55
        /// <summary>
        /// Convert ARGB To RGBA.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToRGBA(byte* src_argb,
        int src_stride_argb,
        byte* dst_rgba,
        int dst_stride_rgba,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:68
        /// <summary>
        /// Aliases
        /// Convert ABGR To AR30.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ABGRToAR30(byte* src_abgr,
        int src_stride_abgr,
        byte* dst_ar30,
        int dst_stride_ar30,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:77
        /// <summary>
        /// Convert ARGB To AR30.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToAR30(byte* src_argb,
        int src_stride_argb,
        byte* dst_ar30,
        int dst_stride_ar30,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:86
        /// <summary>
        /// Convert ARGB To RGB24.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToRGB24(byte* src_argb,
        int src_stride_argb,
        byte* dst_rgb24,
        int dst_stride_rgb24,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:95
        /// <summary>
        /// Convert ARGB To RAW.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToRAW(byte* src_argb,
        int src_stride_argb,
        byte* dst_raw,
        int dst_stride_raw,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:104
        /// <summary>
        /// Convert ARGB To RGB565.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToRGB565(byte* src_argb,
        int src_stride_argb,
        byte* dst_rgb565,
        int dst_stride_rgb565,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:118
        /// <summary>
        /// Convert ARGB To RGB565 with 4x4 dither matrix (16 bytes).
        /// Values in dither matrix from 0 to 7 recommended.
        /// The order of the dither matrix is first byte is upper left.
        /// TODO(fbarchard): Consider pointer to 2d array for dither4x4.
        /// const uint8_t(*dither)[4][4];
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToRGB565Dither(byte* src_argb,
        int src_stride_argb,
        byte* dst_rgb565,
        int dst_stride_rgb565,
        byte* dither4x4,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:127
        /// <summary>
        /// Convert ARGB To ARGB1555.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToARGB1555(byte* src_argb,
        int src_stride_argb,
        byte* dst_argb1555,
        int dst_stride_argb1555,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:136
        /// <summary>
        /// Convert ARGB To ARGB4444.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToARGB4444(byte* src_argb,
        int src_stride_argb,
        byte* dst_argb4444,
        int dst_stride_argb4444,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:149
        /// <summary>
        /// Convert ARGB To I444.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToI444(byte* src_argb,
        int src_stride_argb,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:162
        /// <summary>
        /// Convert ARGB To I422.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToI422(byte* src_argb,
        int src_stride_argb,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:188
        /// <summary>
        /// Convert ARGB to J420. (JPeg full range I420).
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToJ420(byte* src_argb,
        int src_stride_argb,
        byte* dst_yj,
        int dst_stride_yj,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:201
        /// <summary>
        /// Convert ARGB to J422.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToJ422(byte* src_argb,
        int src_stride_argb,
        byte* dst_yj,
        int dst_stride_yj,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:210
        /// <summary>
        /// Convert ARGB to J400. (JPeg full range).
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToJ400(byte* src_argb,
        int src_stride_argb,
        byte* dst_yj,
        int dst_stride_yj,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:219
        /// <summary>
        /// Convert RGBA to J400. (JPeg full range).
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RGBAToJ400(byte* src_rgba,
        int src_stride_rgba,
        byte* dst_yj,
        int dst_stride_yj,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:228
        /// <summary>
        /// Convert ARGB to I400.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToI400(byte* src_argb,
        int src_stride_argb,
        byte* dst_y,
        int dst_stride_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:237
        /// <summary>
        /// Convert ARGB to G. (Reverse of J400toARGB, which replicates G back to ARGB)
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToG(byte* src_argb,
        int src_stride_argb,
        byte* dst_g,
        int dst_stride_g,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:248
        /// <summary>
        /// Convert ARGB To NV12.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToNV12(byte* src_argb,
        int src_stride_argb,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:259
        /// <summary>
        /// Convert ARGB To NV21.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToNV21(byte* src_argb,
        int src_stride_argb,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_vu,
        int dst_stride_vu,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:270
        /// <summary>
        /// Convert ABGR To NV12.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ABGRToNV12(byte* src_abgr,
        int src_stride_abgr,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:281
        /// <summary>
        /// Convert ABGR To NV21.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ABGRToNV21(byte* src_abgr,
        int src_stride_abgr,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_vu,
        int dst_stride_vu,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:301
        /// <summary>
        /// Convert ARGB To YUY2.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToYUY2(byte* src_argb,
        int src_stride_argb,
        byte* dst_yuy2,
        int dst_stride_yuy2,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:310
        /// <summary>
        /// Convert ARGB To UYVY.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToUYVY(byte* src_argb,
        int src_stride_argb,
        byte* dst_uyvy,
        int dst_stride_uyvy,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\cpu_id.h:55
        /// <summary>
        /// Internal flag to indicate cpuid requires initialization.
        /// These flags are only valid on ARM processors.
        /// 0x8 reserved for future ARM flag.
        /// These flags are only valid on x86 processors.
        /// These flags are only valid on MIPS processors.
        /// Optional init function. TestCpuFlag does an auto-init.
        /// Returns cpu_info flags.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int InitCpuFlags();

        // C:\code2\libyuv\src\include\libyuv\cpu_id.h:72
        /// <summary>
        /// Detect CPU has SSE2 etc.
        /// Test_flag parameter should be one of kCpuHas constants above.
        /// Returns non-zero if instruction set is detected
        /// Internal function for parsing /proc/cpuinfo.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ArmCpuCaps(char* cpuinfo_name);

        // C:\code2\libyuv\src\include\libyuv\cpu_id.h:81
        /// <summary>
        /// For testing, allow CPU flags to be disabled.
        /// ie MaskCpuFlags(~kCpuHasSSSE3) to disable SSSE3.
        /// MaskCpuFlags(-1) to enable all cpu specific optimizations.
        /// MaskCpuFlags(1) to disable all cpu specific optimizations.
        /// MaskCpuFlags(0) to reset state so next call will auto init.
        /// Returns cpu_info flags.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int MaskCpuFlags(int enable_flags);

        // C:\code2\libyuv\src\include\libyuv\cpu_id.h:112
        /// <summary>
        /// Sets the CPU flags to |cpu_flags|, bypassing the detection code. |cpu_flags|
        /// should be a valid combination of the kCpuHas constants above and include
        /// kCpuInitialized. Use this method when running in a sandboxed process where
        /// the detection code might fail (as it might access /proc/cpuinfo). In such
        /// cases the cpu_info can be obtained from a non sandboxed process by calling
        /// InitCpuFlags() and passed to the sandboxed process (via command line
        /// parameters, IPC...) which can then call this method to initialize the CPU
        /// flags.
        /// Notes:
        /// - when specifying 0 for |cpu_flags|, the auto initialization is enabled
        /// again.
        /// - enabling CPU features that are not supported by the CPU will result in
        /// undefined behavior.
        /// TODO(fbarchard): consider writing a helper function that translates from
        /// other library CPU info to libyuv CPU info and add a .md doc that explains
        /// CPU detection.
        /// Low level cpuid for X86. Returns zeros on other CPUs.
        /// eax is the info type that you want.
        /// ecx is typically the cpu number, and should normally be zero.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void CpuId(int info_eax, int info_ecx, int* cpu_info);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:49
        /// <summary>
        /// TODO(fbarchard): Remove the following headers includes.
        /// TODO(fbarchard): Move cpu macros to row.h
        /// MemorySanitizer does not support assembly code yet. http://crbug.com/344505
        /// The following are available on all x86 platforms:
        /// Copy a plane of data.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void CopyPlane(byte* src_y,
        int src_stride_y,
        byte* dst_y,
        int dst_stride_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:57
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void CopyPlane_16(ushort* src_y,
        int src_stride_y,
        ushort* dst_y,
        int dst_stride_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:66
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void Convert16To8Plane(ushort* src_y,
        int src_stride_y,
        byte* dst_y,
        int dst_stride_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:75
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void Convert8To16Plane(byte* src_y,
        int src_stride_y,
        ushort* dst_y,
        int dst_stride_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:83
        /// <summary>
        /// Set a plane of data to a 32 bit value.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void SetPlane(byte* dst_y,
        int dst_stride_y,
        int width,
        int height,
        uint value);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:94
        /// <summary>
        /// Split interleaved UV plane into separate U and V planes.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void SplitUVPlane(byte* src_uv,
        int src_stride_uv,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:105
        /// <summary>
        /// Merge separate U and V planes into one interleaved UV plane.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void MergeUVPlane(byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:114
        /// <summary>
        /// Swap U and V channels in interleaved UV plane.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void SwapUVPlane(byte* src_uv,
        int src_stride_uv,
        byte* dst_vu,
        int dst_stride_vu,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:127
        /// <summary>
        /// Split interleaved RGB plane into separate R, G and B planes.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void SplitRGBPlane(byte* src_rgb,
        int src_stride_rgb,
        byte* dst_r,
        int dst_stride_r,
        byte* dst_g,
        int dst_stride_g,
        byte* dst_b,
        int dst_stride_b,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:140
        /// <summary>
        /// Merge separate R, G and B planes into one interleaved RGB plane.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void MergeRGBPlane(byte* src_r,
        int src_stride_r,
        byte* src_g,
        int src_stride_g,
        byte* src_b,
        int src_stride_b,
        byte* dst_rgb,
        int dst_stride_rgb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:149
        /// <summary>
        /// Copy I400.  Supports inverting.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I400ToI400(byte* src_y,
        int src_stride_y,
        byte* dst_y,
        int dst_stride_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:169
        /// <summary>
        /// Copy I422 to I422.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422Copy(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:187
        /// <summary>
        /// Copy I444 to I444.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I444Copy(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:200
        /// <summary>
        /// Convert YUY2 to I422.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int YUY2ToI422(byte* src_yuy2,
        int src_stride_yuy2,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:213
        /// <summary>
        /// Convert UYVY to I422.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int UYVYToI422(byte* src_uyvy,
        int src_stride_uyvy,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:223
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int YUY2ToNV12(byte* src_yuy2,
        int src_stride_yuy2,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:233
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int UYVYToNV12(byte* src_uyvy,
        int src_stride_uyvy,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:246
        /// <summary>
        /// Convert NV21 to NV12.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV21ToNV12(byte* src_y,
        int src_stride_y,
        byte* src_vu,
        int src_stride_vu,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:254
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int YUY2ToY(byte* src_yuy2,
        int src_stride_yuy2,
        byte* dst_y,
        int dst_stride_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:267
        /// <summary>
        /// Convert I420 to I400. (calls CopyPlane ignoring u/v).
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToI400(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_y,
        int dst_stride_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:288
        /// <summary>
        /// Alias
        /// I420 mirror.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420Mirror(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:301
        /// <summary>
        /// Alias
        /// I400 mirror.  A single plane is mirrored horizontally.
        /// Pass negative height to achieve 180 degree rotation.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I400Mirror(byte* src_y,
        int src_stride_y,
        byte* dst_y,
        int dst_stride_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:313
        /// <summary>
        /// Alias
        /// ARGB mirror.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBMirror(byte* src_argb,
        int src_stride_argb,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:324
        /// <summary>
        /// Convert NV12 to RGB565.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV12ToRGB565(byte* src_y,
        int src_stride_y,
        byte* src_uv,
        int src_stride_uv,
        byte* dst_rgb565,
        int dst_stride_rgb565,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:338
        /// <summary>
        /// I422ToARGB is in convert_argb.h
        /// Convert I422 to BGRA.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422ToBGRA(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_bgra,
        int dst_stride_bgra,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:351
        /// <summary>
        /// Convert I422 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422ToABGR(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:364
        /// <summary>
        /// Convert I422 to RGBA.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422ToRGBA(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_rgba,
        int dst_stride_rgba,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:375
        /// <summary>
        /// Alias
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RAWToRGB24(byte* src_raw,
        int src_stride_raw,
        byte* dst_rgb24,
        int dst_stride_rgb24,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:391
        /// <summary>
        /// Draw a rectangle into I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420Rect(byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int x,
        int y,
        int width,
        int height,
        int value_y,
        int value_u,
        int value_v);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:401
        /// <summary>
        /// Draw a rectangle into ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBRect(byte* dst_argb,
        int dst_stride_argb,
        int dst_x,
        int dst_y,
        int width,
        int height,
        uint value);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:410
        /// <summary>
        /// Convert ARGB to gray scale ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBGrayTo(byte* src_argb,
        int src_stride_argb,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:419
        /// <summary>
        /// Make a rectangle of ARGB gray scale.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBGray(byte* dst_argb,
        int dst_stride_argb,
        int dst_x,
        int dst_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:428
        /// <summary>
        /// Make a rectangle of ARGB Sepia tone.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBSepia(byte* dst_argb,
        int dst_stride_argb,
        int dst_x,
        int dst_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:443
        /// <summary>
        /// Apply a matrix rotation to each ARGB pixel.
        /// matrix_argb is 4 signed ARGB values. -128 to 127 representing -2 to 2.
        /// The first 4 coefficients apply to B, G, R, A and produce B of the output.
        /// The next 4 coefficients apply to B, G, R, A and produce G of the output.
        /// The next 4 coefficients apply to B, G, R, A and produce R of the output.
        /// The last 4 coefficients apply to B, G, R, A and produce A of the output.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBColorMatrix(byte* src_argb,
        int src_stride_argb,
        byte* dst_argb,
        int dst_stride_argb,
        byte* matrix_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:458
        /// <summary>
        /// Deprecated. Use ARGBColorMatrix instead.
        /// Apply a matrix rotation to each ARGB pixel.
        /// matrix_argb is 3 signed ARGB values. -128 to 127 representing -1 to 1.
        /// The first 4 coefficients apply to B, G, R, A and produce B of the output.
        /// The next 4 coefficients apply to B, G, R, A and produce G of the output.
        /// The last 4 coefficients apply to B, G, R, A and produce R of the output.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RGBColorMatrix(byte* dst_argb,
        int dst_stride_argb,
        byte* matrix_rgb,
        int dst_x,
        int dst_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:469
        /// <summary>
        /// Apply a color table each ARGB pixel.
        /// Table contains 256 ARGB values.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBColorTable(byte* dst_argb,
        int dst_stride_argb,
        byte* table_argb,
        int dst_x,
        int dst_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:480
        /// <summary>
        /// Apply a color table each ARGB pixel but preserve destination alpha.
        /// Table contains 256 ARGB values.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RGBColorTable(byte* dst_argb,
        int dst_stride_argb,
        byte* table_argb,
        int dst_x,
        int dst_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:492
        /// <summary>
        /// Apply a luma/color table each ARGB pixel but preserve destination alpha.
        /// Table contains 32768 values indexed by [Y][C] where 7 it 7 bit luma from
        /// RGB (YJ style) and C is an 8 bit color component (R, G or B).
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBLumaColorTable(byte* src_argb,
        int src_stride_argb,
        byte* dst_argb,
        int dst_stride_argb,
        byte* luma,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:509
        /// <summary>
        /// Apply a 3 term polynomial to ARGB values.
        /// poly points to a 4x4 matrix.  The first row is constants.  The 2nd row is
        /// coefficients for b, g, r and a.  The 3rd row is coefficients for b squared,
        /// g squared, r squared and a squared.  The 4rd row is coefficients for b to
        /// the 3, g to the 3, r to the 3 and a to the 3.  The values are summed and
        /// result clamped to 0 to 255.
        /// A polynomial approximation can be dirived using software such as 'R'.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBPolynomial(byte* src_argb,
        int src_stride_argb,
        byte* dst_argb,
        int dst_stride_argb,
        float* poly,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:520
        /// <summary>
        /// Convert plane of 16 bit shorts to half floats.
        /// Source values are multiplied by scale before storing as half float.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int HalfFloatPlane(ushort* src_y,
        int src_stride_y,
        ushort* dst_y,
        int dst_stride_y,
        float scale,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:524
        /// <summary>
        /// Convert a buffer of bytes to floats, scale the values and store as floats.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ByteToFloat(byte* src_y, float* dst_y, float scale, int width);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:539
        /// <summary>
        /// Quantize a rectangle of ARGB. Alpha unaffected.
        /// scale is a 16 bit fractional fixed point scaler between 0 and 65535.
        /// interval_size should be a value between 1 and 255.
        /// interval_offset should be a value between 0 and 255.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBQuantize(byte* dst_argb,
        int dst_stride_argb,
        int scale,
        int interval_size,
        int interval_offset,
        int dst_x,
        int dst_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:557
        /// <summary>
        /// Copy Alpha channel of ARGB to alpha of ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBCopyAlpha(byte* src_argb,
        int src_stride_argb,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:566
        /// <summary>
        /// Extract the alpha channel from ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBExtractAlpha(byte* src_argb,
        int src_stride_argb,
        byte* dst_a,
        int dst_stride_a,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:575
        /// <summary>
        /// Copy Y channel to Alpha of ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBCopyYToAlpha(byte* src_y,
        int src_stride_y,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:584
        /// <summary>
        /// Get function to Alpha Blend ARGB pixels and store to destination.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void* GetARGBBlend();

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:597
        /// <summary>
        /// Alpha Blend ARGB images and store to destination.
        /// Source is pre-multiplied by alpha using ARGBAttenuate.
        /// Alpha of destination is set to 255.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBBlend(byte* src_argb0,
        int src_stride_argb0,
        byte* src_argb1,
        int src_stride_argb1,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:611
        /// <summary>
        /// Alpha Blend plane and store to destination.
        /// Source is not pre-multiplied by alpha.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int BlendPlane(byte* src_y0,
        int src_stride_y0,
        byte* src_y1,
        int src_stride_y1,
        byte* alpha,
        int alpha_stride,
        byte* dst_y,
        int dst_stride_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:638
        /// <summary>
        /// Alpha Blend YUV images and store to destination.
        /// Source is not pre-multiplied by alpha.
        /// Alpha is full width x height and subsampled to half size to apply to UV.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420Blend(byte* src_y0,
        int src_stride_y0,
        byte* src_u0,
        int src_stride_u0,
        byte* src_v0,
        int src_stride_v0,
        byte* src_y1,
        int src_stride_y1,
        byte* src_u1,
        int src_stride_u1,
        byte* src_v1,
        int src_stride_v1,
        byte* alpha,
        int alpha_stride,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:649
        /// <summary>
        /// Multiply ARGB image by ARGB image. Shifted down by 8. Saturates to 255.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBMultiply(byte* src_argb0,
        int src_stride_argb0,
        byte* src_argb1,
        int src_stride_argb1,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:660
        /// <summary>
        /// Add ARGB image with ARGB image. Saturates to 255.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBAdd(byte* src_argb0,
        int src_stride_argb0,
        byte* src_argb1,
        int src_stride_argb1,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:671
        /// <summary>
        /// Subtract ARGB image (argb1) from ARGB image (argb0). Saturates to 0.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBSubtract(byte* src_argb0,
        int src_stride_argb0,
        byte* src_argb1,
        int src_stride_argb1,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:684
        /// <summary>
        /// Convert I422 to YUY2.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422ToYUY2(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_yuy2,
        int dst_stride_yuy2,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:697
        /// <summary>
        /// Convert I422 to UYVY.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422ToUYVY(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_uyvy,
        int dst_stride_uyvy,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:706
        /// <summary>
        /// Convert unattentuated ARGB to preattenuated ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBAttenuate(byte* src_argb,
        int src_stride_argb,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:715
        /// <summary>
        /// Convert preattentuated ARGB to unattenuated ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBUnattenuate(byte* src_argb,
        int src_stride_argb,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:726
        /// <summary>
        /// Internal function - do not call directly.
        /// Computes table of cumulative sum for image where the value is the sum
        /// of all values above and to the left of the entry. Used by ARGBBlur.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBComputeCumulativeSum(byte* src_argb,
        int src_stride_argb,
        int* dst_cumsum,
        int dst_stride32_cumsum,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:743
        /// <summary>
        /// Blur ARGB image.
        /// dst_cumsum table of width * (height + 1) * 16 bytes aligned to
        /// 16 byte boundary.
        /// dst_stride32_cumsum is number of ints in a row (width * 4).
        /// radius is number of pixels around the center.  e.g. 1 = 3x3. 2=5x5.
        /// Blur is optimized for radius of 5 (11x11) or less.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBBlur(byte* src_argb,
        int src_stride_argb,
        byte* dst_argb,
        int dst_stride_argb,
        int* dst_cumsum,
        int dst_stride32_cumsum,
        int width,
        int height,
        int radius);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:753
        /// <summary>
        /// Multiply ARGB image by ARGB value.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBShade(byte* src_argb,
        int src_stride_argb,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height,
        uint value);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:768
        /// <summary>
        /// Interpolate between two images using specified amount of interpolation
        /// (0 to 255) and store to destination.
        /// 'interpolation' is specified as 8 bit fraction where 0 means 100% src0
        /// and 255 means 1% src0 and 99% src1.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int InterpolatePlane(byte* src0,
        int src_stride0,
        byte* src1,
        int src_stride1,
        byte* dst,
        int dst_stride,
        int width,
        int height,
        int interpolation);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:781
        /// <summary>
        /// Interpolate between two ARGB images using specified amount of interpolation
        /// Internally calls InterpolatePlane with width * 4 (bpp).
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBInterpolate(byte* src_argb0,
        int src_stride_argb0,
        byte* src_argb1,
        int src_stride_argb1,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height,
        int interpolation);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:807
        /// <summary>
        /// Interpolate between two YUV images using specified amount of interpolation
        /// Internally calls InterpolatePlane on each plane where the U and V planes
        /// are half width and half height.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420Interpolate(byte* src0_y,
        int src0_stride_y,
        byte* src0_u,
        int src0_stride_u,
        byte* src0_v,
        int src0_stride_v,
        byte* src1_y,
        int src1_stride_y,
        byte* src1_u,
        int src1_stride_u,
        byte* src1_v,
        int src1_stride_v,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height,
        int interpolation);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:816
        /// <summary>
        /// Row function for copying pixels from a source with a slope to a row
        /// of destination. Useful for scaling, rotation, mirror, texture mapping.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void ARGBAffineRow_C(byte* src_argb,
        int src_argb_stride,
        byte* dst_argb,
        float* uv_dudv,
        int width);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:823
        /// <summary>
        /// TODO(fbarchard): Move ARGBAffineRow_SSE2 to row.h
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void ARGBAffineRow_SSE2(byte* src_argb,
        int src_argb_stride,
        byte* dst_argb,
        float* uv_dudv,
        int width);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:834
        /// <summary>
        /// Shuffle ARGB channel order.  e.g. BGRA to ARGB.
        /// shuffler is 16 bytes and must be aligned.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBShuffle(byte* src_bgra,
        int src_stride_bgra,
        byte* dst_argb,
        int dst_stride_argb,
        byte* shuffler,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:843
        /// <summary>
        /// Sobel ARGB effect with planar output.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBSobelToPlane(byte* src_argb,
        int src_stride_argb,
        byte* dst_y,
        int dst_stride_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:852
        /// <summary>
        /// Sobel ARGB effect.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBSobel(byte* src_argb,
        int src_stride_argb,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:861
        /// <summary>
        /// Sobel ARGB effect w/ Sobel X, Sobel, Sobel Y in ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBSobelXY(byte* src_argb,
        int src_stride_argb,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\rotate.h:49
        /// <summary>
        /// Supported rotation.
        /// Deprecated.
        /// Rotate I420 frame.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420Rotate(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height,
        RotationMode mode);

        // C:\code2\libyuv\src\include\libyuv\rotate.h:67
        /// <summary>
        /// Rotate I444 frame.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I444Rotate(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height,
        RotationMode mode);

        // C:\code2\libyuv\src\include\libyuv\rotate.h:83
        /// <summary>
        /// Rotate NV12 input and store in I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV12ToI420Rotate(byte* src_y,
        int src_stride_y,
        byte* src_uv,
        int src_stride_uv,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height,
        RotationMode mode);

        // C:\code2\libyuv\src\include\libyuv\rotate.h:93
        /// <summary>
        /// Rotate a plane by 0, 90, 180, or 270.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RotatePlane(byte* src,
        int src_stride,
        byte* dst,
        int dst_stride,
        int width,
        int height,
        RotationMode mode);

        // C:\code2\libyuv\src\include\libyuv\rotate.h:102
        /// <summary>
        /// Rotate planes by 90, 180, 270. Deprecated.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void RotatePlane90(byte* src,
        int src_stride,
        byte* dst,
        int dst_stride,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\rotate.h:110
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void RotatePlane180(byte* src,
        int src_stride,
        byte* dst,
        int dst_stride,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\rotate.h:118
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void RotatePlane270(byte* src,
        int src_stride,
        byte* dst,
        int dst_stride,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\rotate.h:128
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void RotateUV90(byte* src,
        int src_stride,
        byte* dst_a,
        int dst_stride_a,
        byte* dst_b,
        int dst_stride_b,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\rotate.h:142
        /// <summary>
        /// Rotations for when U and V are interleaved.
        /// These functions take one input pointer and
        /// split the data into two buffers while
        /// rotating them. Deprecated.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void RotateUV180(byte* src,
        int src_stride,
        byte* dst_a,
        int dst_stride_a,
        byte* dst_b,
        int dst_stride_b,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\rotate.h:152
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void RotateUV270(byte* src,
        int src_stride,
        byte* dst_a,
        int dst_stride_a,
        byte* dst_b,
        int dst_stride_b,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\rotate.h:164
        /// <summary>
        /// The 90 and 270 functions are based on transposes.
        /// Doing a transpose with reversing the read/write
        /// order will result in a rotation by +- 90 degrees.
        /// Deprecated.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void TransposePlane(byte* src,
        int src_stride,
        byte* dst,
        int dst_stride,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\rotate.h:174
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void TransposeUV(byte* src,
        int src_stride,
        byte* dst_a,
        int dst_stride_a,
        byte* dst_b,
        int dst_stride_b,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\rotate_argb.h:29
        /// <summary>
        /// Rotate ARGB frame
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBRotate(byte* src_argb,
        int src_stride_argb,
        byte* dst_argb,
        int dst_stride_argb,
        int src_width,
        int src_height,
        RotationMode mode);

        // C:\code2\libyuv\src\include\libyuv\scale.h:38
        /// <summary>
        /// Supported filtering.
        /// Scale a YUV plane.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void ScalePlane(byte* src,
        int src_stride,
        int src_width,
        int src_height,
        byte* dst,
        int dst_stride,
        int dst_width,
        int dst_height,
        FilterMode filtering);

        // C:\code2\libyuv\src\include\libyuv\scale.h:49
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void ScalePlane_16(ushort* src,
        int src_stride,
        int src_width,
        int src_height,
        ushort* dst,
        int dst_stride,
        int dst_width,
        int dst_height,
        FilterMode filtering);

        // C:\code2\libyuv\src\include\libyuv\scale.h:78
        /// <summary>
        /// Scales a YUV 4:2:0 image from the src width and height to the
        /// dst width and height.
        /// If filtering is kFilterNone, a simple nearest-neighbor algorithm is
        /// used. This produces basic (blocky) quality at the fastest speed.
        /// If filtering is kFilterBilinear, interpolation is used to produce a better
        /// quality image, at the expense of speed.
        /// If filtering is kFilterBox, averaging is used to produce ever better
        /// quality image, at further expense of speed.
        /// Returns 0 if successful.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420Scale(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        int src_width,
        int src_height,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int dst_width,
        int dst_height,
        FilterMode filtering);

        // C:\code2\libyuv\src\include\libyuv\scale.h:97
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420Scale_16(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        int src_width,
        int src_height,
        ushort* dst_y,
        int dst_stride_y,
        ushort* dst_u,
        int dst_stride_u,
        ushort* dst_v,
        int dst_stride_v,
        int dst_width,
        int dst_height,
        FilterMode filtering);

        // C:\code2\libyuv\src\include\libyuv\scale.h:126
        /// <summary>
        /// Scales a YUV 4:4:4 image from the src width and height to the
        /// dst width and height.
        /// If filtering is kFilterNone, a simple nearest-neighbor algorithm is
        /// used. This produces basic (blocky) quality at the fastest speed.
        /// If filtering is kFilterBilinear, interpolation is used to produce a better
        /// quality image, at the expense of speed.
        /// If filtering is kFilterBox, averaging is used to produce ever better
        /// quality image, at further expense of speed.
        /// Returns 0 if successful.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I444Scale(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        int src_width,
        int src_height,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int dst_width,
        int dst_height,
        FilterMode filtering);

        // C:\code2\libyuv\src\include\libyuv\scale.h:145
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I444Scale_16(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        int src_width,
        int src_height,
        ushort* dst_y,
        int dst_stride_y,
        ushort* dst_u,
        int dst_stride_u,
        ushort* dst_v,
        int dst_stride_v,
        int dst_width,
        int dst_height,
        FilterMode filtering);

        // C:\code2\libyuv\src\include\libyuv\scale.h:166
        /// <summary>
        /// Legacy API.  Deprecated.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int Scale(byte* src_y,
        byte* src_u,
        byte* src_v,
        int src_stride_y,
        int src_stride_u,
        int src_stride_v,
        int src_width,
        int src_height,
        byte* dst_y,
        byte* dst_u,
        byte* dst_v,
        int dst_stride_y,
        int dst_stride_u,
        int dst_stride_v,
        int dst_width,
        int dst_height,
        int interpolate);

        // C:\code2\libyuv\src\include\libyuv\scale.h:170
        /// <summary>
        /// For testing, allow disabling of specialized scalers.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void SetUseReferenceImpl(int use);

        // C:\code2\libyuv\src\include\libyuv\scale_argb.h:30
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBScale(byte* src_argb,
        int src_stride_argb,
        int src_width,
        int src_height,
        byte* dst_argb,
        int dst_stride_argb,
        int dst_width,
        int dst_height,
        FilterMode filtering);

        // C:\code2\libyuv\src\include\libyuv\scale_argb.h:46
        /// <summary>
        /// Clipped scale takes destination rectangle coordinates for clip values.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBScaleClip(byte* src_argb,
        int src_stride_argb,
        int src_width,
        int src_height,
        byte* dst_argb,
        int dst_stride_argb,
        int dst_width,
        int dst_height,
        int clip_x,
        int clip_y,
        int clip_width,
        int clip_height,
        FilterMode filtering);

        // C:\code2\libyuv\src\include\libyuv\scale_argb.h:68
        /// <summary>
        /// Scale with YUV conversion to ARGB and clipping.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int YUVToARGBScaleClip(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        uint src_fourcc,
        int src_width,
        int src_height,
        byte* dst_argb,
        int dst_stride_argb,
        uint dst_fourcc,
        int dst_width,
        int dst_height,
        int clip_x,
        int clip_y,
        int clip_width,
        int clip_height,
        FilterMode filtering);
    }
}