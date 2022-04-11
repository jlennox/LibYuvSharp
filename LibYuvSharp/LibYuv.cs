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
    /// <summary>
    /// This struct is for Intel color conversion.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct YuvConstants
    {
        public fixed byte kUVToB[32];
        public fixed byte kUVToG[32];
        public fixed byte kUVToR[32];
        public fixed short kYToRgb[16];
        public fixed short kYBiasToRgb[16];
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
        /// Convert I444 to NV12.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I444ToNV12(byte* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:72
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:89
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:106
        /// <summary>
        /// Convert I422 to I444.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422ToI444(byte* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:123
        /// <summary>
        /// Convert I422 to I210.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422ToI210(byte* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:136
        /// <summary>
        /// Convert MM21 to NV12.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int MM21ToNV12(byte* src_y,
        int src_stride_y,
        byte* src_uv,
        int src_stride_uv,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:151
        /// <summary>
        /// Convert MM21 to I420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int MM21ToI420(byte* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:166
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:184
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:201
        /// <summary>
        /// Convert I420 to I444.
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:220
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:238
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:255
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I210ToI422(ushort* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:272
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I410ToI444(ushort* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:289
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I012ToI420(ushort* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:306
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I212ToI422(ushort* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:323
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I412ToI444(ushort* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:342
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I410ToI010(ushort* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:361
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I210ToI010(ushort* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:378
        /// <summary>
        /// Convert I010 to I410
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I010ToI410(ushort* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:398
        /// <summary>
        /// Convert I012 to I412
        /// Convert I210 to I410
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I210ToI410(ushort* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:416
        /// <summary>
        /// Convert I212 to I412
        /// Convert I010 to P010
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I010ToP010(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        ushort* dst_y,
        int dst_stride_y,
        ushort* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:431
        /// <summary>
        /// Convert I210 to P210
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I210ToP210(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        ushort* dst_y,
        int dst_stride_y,
        ushort* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:446
        /// <summary>
        /// Convert I012 to P012
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I012ToP012(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        ushort* dst_y,
        int dst_stride_y,
        ushort* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:461
        /// <summary>
        /// Convert I212 to P212
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I212ToP212(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        ushort* dst_y,
        int dst_stride_y,
        ushort* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:474
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:485
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:502
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:517
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:530
        /// <summary>
        /// Convert NV12 to NV24.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV12ToNV24(byte* src_y,
        int src_stride_y,
        byte* src_uv,
        int src_stride_uv,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:543
        /// <summary>
        /// Convert NV16 to NV24.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV16ToNV24(byte* src_y,
        int src_stride_y,
        byte* src_uv,
        int src_stride_uv,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:556
        /// <summary>
        /// Convert P010 to P410.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int P010ToP410(ushort* src_y,
        int src_stride_y,
        ushort* src_uv,
        int src_stride_uv,
        ushort* dst_y,
        int dst_stride_y,
        ushort* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:575
        /// <summary>
        /// Convert P012 to P412.
        /// Convert P016 to P416.
        /// Convert P210 to P410.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int P210ToP410(ushort* src_y,
        int src_stride_y,
        ushort* src_uv,
        int src_stride_uv,
        ushort* dst_y,
        int dst_stride_y,
        ushort* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:594
        /// <summary>
        /// Convert P212 to P412.
        /// Convert P216 to P416.
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:607
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:618
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:629
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:647
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:660
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:673
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:686
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:699
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:712
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:725
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:738
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:751
        /// <summary>
        /// RGB big endian (rgb in memory) to J420.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RAWToJ420(byte* src_raw,
        int src_stride_raw,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:764
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:777
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:790
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:799
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

        // C:\code2\libyuv\src\include\libyuv\convert.h:808
        /// <summary>
        /// RGB big endian (rgb in memory) to J400.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RAWToJ400(byte* src_raw,
        int src_stride_raw,
        byte* dst_yj,
        int dst_stride_yj,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:824
        /// <summary>
        /// src_width/height provided by capture.
        /// dst_width/height for clipping determine final size.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int MJPGToI420(byte* sample,
        IntPtr sample_size,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int src_width,
        int src_height,
        int dst_width,
        int dst_height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:837
        /// <summary>
        /// JPEG to NV21
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int MJPGToNV21(byte* sample,
        IntPtr sample_size,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_vu,
        int dst_stride_vu,
        int src_width,
        int src_height,
        int dst_width,
        int dst_height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:850
        /// <summary>
        /// JPEG to NV12
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int MJPGToNV12(byte* sample,
        IntPtr sample_size,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_uv,
        int dst_stride_uv,
        int src_width,
        int src_height,
        int dst_width,
        int dst_height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:857
        /// <summary>
        /// Query size of MJPG in pixels.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int MJPGSize(byte* sample,
        IntPtr sample_size,
        int* width,
        int* height);

        // C:\code2\libyuv\src\include\libyuv\convert.h:897
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:92
        /// <summary>
        /// Conversion matrix for YUV to RGB
        /// Conversion matrix for YVU to BGR
        /// Macros for end swapped destination Matrix conversions.
        /// Swap UV and pass mirrored kYvuJPEGConstants matrix.
        /// TODO(fbarchard): Add macro for each Matrix function.
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:105
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:118
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:131
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:144
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:157
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:170
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:183
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:196
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:209
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:222
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:235
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:248
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:261
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:274
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:287
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:300
        /// <summary>
        /// Convert U422 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int U422ToABGR(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:313
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:326
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:339
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:352
        /// <summary>
        /// Convert J444 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int J444ToABGR(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:365
        /// <summary>
        /// Convert H444 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int H444ToARGB(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:378
        /// <summary>
        /// Convert H444 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int H444ToABGR(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:391
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:404
        /// <summary>
        /// Convert U444 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int U444ToABGR(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:417
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:430
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:443
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:456
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:469
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:482
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:495
        /// <summary>
        /// Convert I210 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I210ToARGB(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:508
        /// <summary>
        /// Convert I210 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I210ToABGR(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:521
        /// <summary>
        /// Convert H210 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int H210ToARGB(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:534
        /// <summary>
        /// Convert H210 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int H210ToABGR(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:547
        /// <summary>
        /// Convert U210 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int U210ToARGB(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:560
        /// <summary>
        /// Convert U210 to ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int U210ToABGR(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_abgr,
        int dst_stride_abgr,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:576
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:592
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:608
        /// <summary>
        /// Convert I422 with Alpha to preattenuated ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422AlphaToARGB(byte* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:624
        /// <summary>
        /// Convert I422 with Alpha to preattenuated ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422AlphaToABGR(byte* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:640
        /// <summary>
        /// Convert I444 with Alpha to preattenuated ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I444AlphaToARGB(byte* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:656
        /// <summary>
        /// Convert I444 with Alpha to preattenuated ABGR.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I444AlphaToABGR(byte* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:665
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:674
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:688
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:699
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:710
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:721
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:732
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:743
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:754
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:765
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:776
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:785
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:794
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:807
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:820
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:833
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:846
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:859
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:872
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:885
        /// <summary>
        /// Convert I210 to AR30.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I210ToAR30(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_ar30,
        int dst_stride_ar30,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:898
        /// <summary>
        /// Convert I210 to AB30.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I210ToAB30(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_ab30,
        int dst_stride_ab30,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:911
        /// <summary>
        /// Convert H210 to AR30.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int H210ToAR30(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_ar30,
        int dst_stride_ar30,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:924
        /// <summary>
        /// Convert H210 to AB30.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int H210ToAB30(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_ab30,
        int dst_stride_ab30,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:937
        /// <summary>
        /// Convert U210 to AR30.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int U210ToAR30(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_ar30,
        int dst_stride_ar30,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:950
        /// <summary>
        /// Convert U210 to AB30.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int U210ToAB30(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_ab30,
        int dst_stride_ab30,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:959
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:968
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:977
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:989
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:998
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1007
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1016
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1025
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1034
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1048
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1057
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1066
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1075
        /// <summary>
        /// Convert AR64 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int AR64ToARGB(ushort* src_ar64,
        int src_stride_ar64,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1087
        /// <summary>
        /// Convert AB64 to ABGR.
        /// Convert AB64 to ARGB.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int AB64ToARGB(ushort* src_ab64,
        int src_stride_ab64,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1099
        /// <summary>
        /// Convert AR64 to ABGR.
        /// Convert AR64 To AB64.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int AR64ToAB64(ushort* src_ar64,
        int src_stride_ar64,
        ushort* dst_ab64,
        int dst_stride_ab64,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1114
        /// <summary>
        /// Convert AB64 To AR64.
        /// src_width/height provided by capture
        /// dst_width/height for clipping determine final size.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int MJPGToARGB(byte* sample,
        IntPtr sample_size,
        byte* dst_argb,
        int dst_stride_argb,
        int src_width,
        int src_height,
        int dst_width,
        int dst_height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1128
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1142
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1153
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1166
        /// <summary>
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1192
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1216
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1240
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1252
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1264
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1276
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1288
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1300
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1312
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1324
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1336
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1348
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1360
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1377
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1389
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1401
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1414
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1427
        /// <summary>
        /// Convert I420 to AB30.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToAB30(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_ab30,
        int dst_stride_ab30,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1440
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

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1453
        /// <summary>
        /// Convert H420 to AB30.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int H420ToAB30(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_ab30,
        int dst_stride_ab30,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1467
        /// <summary>
        /// Convert I420 to ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToARGBMatrix(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1481
        /// <summary>
        /// Convert I422 to ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422ToARGBMatrix(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1495
        /// <summary>
        /// Convert I444 to ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I444ToARGBMatrix(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1509
        /// <summary>
        /// Convert 10 bit 420 YUV to ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I010ToAR30Matrix(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_ar30,
        int dst_stride_ar30,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1523
        /// <summary>
        /// Convert 10 bit 420 YUV to ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I210ToAR30Matrix(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_ar30,
        int dst_stride_ar30,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1537
        /// <summary>
        /// Convert 10 bit 444 YUV to ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I410ToAR30Matrix(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_ar30,
        int dst_stride_ar30,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1551
        /// <summary>
        /// Convert 10 bit YUV to ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I010ToARGBMatrix(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1565
        /// <summary>
        /// multiply 12 bit yuv into high bits to allow any number of bits.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I012ToAR30Matrix(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_ar30,
        int dst_stride_ar30,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1579
        /// <summary>
        /// Convert 12 bit YUV to ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I012ToARGBMatrix(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1593
        /// <summary>
        /// Convert 10 bit 422 YUV to ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I210ToARGBMatrix(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1607
        /// <summary>
        /// Convert 10 bit 444 YUV to ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I410ToARGBMatrix(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1619
        /// <summary>
        /// Convert P010 to ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int P010ToARGBMatrix(ushort* src_y,
        int src_stride_y,
        ushort* src_uv,
        int src_stride_uv,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1631
        /// <summary>
        /// Convert P210 to ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int P210ToARGBMatrix(ushort* src_y,
        int src_stride_y,
        ushort* src_uv,
        int src_stride_uv,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1643
        /// <summary>
        /// Convert P010 to AR30 with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int P010ToAR30Matrix(ushort* src_y,
        int src_stride_y,
        ushort* src_uv,
        int src_stride_uv,
        byte* dst_ar30,
        int dst_stride_ar30,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1655
        /// <summary>
        /// Convert P210 to AR30 with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int P210ToAR30Matrix(ushort* src_y,
        int src_stride_y,
        ushort* src_uv,
        int src_stride_uv,
        byte* dst_ar30,
        int dst_stride_ar30,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1691
        /// <summary>
        /// P012 and P010 use most significant bits so the conversion is the same.
        /// Convert P012 to ARGB with matrix.
        /// Convert P012 to AR30 with matrix.
        /// Convert P212 to ARGB with matrix.
        /// Convert P212 to AR30 with matrix.
        /// Convert P016 to ARGB with matrix.
        /// Convert P016 to AR30 with matrix.
        /// Convert P216 to ARGB with matrix.
        /// Convert P216 to AR30 with matrix.
        /// Convert I420 with Alpha to preattenuated ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420AlphaToARGBMatrix(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* src_a,
        int src_stride_a,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height,
        int attenuate);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1708
        /// <summary>
        /// Convert I422 with Alpha to preattenuated ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422AlphaToARGBMatrix(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* src_a,
        int src_stride_a,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height,
        int attenuate);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1725
        /// <summary>
        /// Convert I444 with Alpha to preattenuated ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I444AlphaToARGBMatrix(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* src_a,
        int src_stride_a,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height,
        int attenuate);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1742
        /// <summary>
        /// Convert I010 with Alpha to preattenuated ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I010AlphaToARGBMatrix(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        ushort* src_a,
        int src_stride_a,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height,
        int attenuate);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1759
        /// <summary>
        /// Convert I210 with Alpha to preattenuated ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I210AlphaToARGBMatrix(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        ushort* src_a,
        int src_stride_a,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height,
        int attenuate);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1776
        /// <summary>
        /// Convert I410 with Alpha to preattenuated ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I410AlphaToARGBMatrix(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        ushort* src_a,
        int src_stride_a,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height,
        int attenuate);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1788
        /// <summary>
        /// Convert NV12 to ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV12ToARGBMatrix(byte* src_y,
        int src_stride_y,
        byte* src_uv,
        int src_stride_uv,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1800
        /// <summary>
        /// Convert NV21 to ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV21ToARGBMatrix(byte* src_y,
        int src_stride_y,
        byte* src_vu,
        int src_stride_vu,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1812
        /// <summary>
        /// Convert NV12 to RGB565 with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV12ToRGB565Matrix(byte* src_y,
        int src_stride_y,
        byte* src_uv,
        int src_stride_uv,
        byte* dst_rgb565,
        int dst_stride_rgb565,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1824
        /// <summary>
        /// Convert NV12 to RGB24 with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV12ToRGB24Matrix(byte* src_y,
        int src_stride_y,
        byte* src_uv,
        int src_stride_uv,
        byte* dst_rgb24,
        int dst_stride_rgb24,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1836
        /// <summary>
        /// Convert NV21 to RGB24 with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV21ToRGB24Matrix(byte* src_y,
        int src_stride_y,
        byte* src_vu,
        int src_stride_vu,
        byte* dst_rgb24,
        int dst_stride_rgb24,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1851
        /// <summary>
        /// Convert Android420 to ARGB with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int Android420ToARGBMatrix(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        int src_pixel_stride_uv,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1865
        /// <summary>
        /// Convert I422 to RGBA with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422ToRGBAMatrix(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_rgba,
        int dst_stride_rgba,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1879
        /// <summary>
        /// Convert I420 to RGBA with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToRGBAMatrix(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_rgba,
        int dst_stride_rgba,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1893
        /// <summary>
        /// Convert I420 to RGB24 with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToRGB24Matrix(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_rgb24,
        int dst_stride_rgb24,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1907
        /// <summary>
        /// Convert I420 to RGB565 with specified color matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToRGB565Matrix(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_rgb565,
        int dst_stride_rgb565,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1921
        /// <summary>
        /// Convert I420 to AR30 with matrix.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToAR30Matrix(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_ar30,
        int dst_stride_ar30,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1931
        /// <summary>
        /// Convert I400 (grey) to ARGB.  Reverse of ARGBToI400.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I400ToARGBMatrix(byte* src_y,
        int src_stride_y,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1946
        /// <summary>
        /// Convert I420 to ARGB with matrix and UV filter mode.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToARGBMatrixFilter(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height,
        FilterMode filter);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1961
        /// <summary>
        /// Convert I422 to ARGB with matrix and UV filter mode.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422ToARGBMatrixFilter(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height,
        FilterMode filter);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1976
        /// <summary>
        /// Convert I010 to AR30 with matrix and UV filter mode.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I010ToAR30MatrixFilter(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_ar30,
        int dst_stride_ar30,
        YuvConstants* yuvconstants,
        int width,
        int height,
        FilterMode filter);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:1991
        /// <summary>
        /// Convert I210 to AR30 with matrix and UV filter mode.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I210ToAR30MatrixFilter(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_ar30,
        int dst_stride_ar30,
        YuvConstants* yuvconstants,
        int width,
        int height,
        FilterMode filter);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:2006
        /// <summary>
        /// Convert I010 to ARGB with matrix and UV filter mode.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I010ToARGBMatrixFilter(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height,
        FilterMode filter);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:2021
        /// <summary>
        /// Convert I210 to ARGB with matrix and UV filter mode.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I210ToARGBMatrixFilter(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height,
        FilterMode filter);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:2039
        /// <summary>
        /// Convert I420 with Alpha to attenuated ARGB with matrix and UV filter mode.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420AlphaToARGBMatrixFilter(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* src_a,
        int src_stride_a,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height,
        int attenuate,
        FilterMode filter);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:2057
        /// <summary>
        /// Convert I422 with Alpha to attenuated ARGB with matrix and UV filter mode.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422AlphaToARGBMatrixFilter(byte* src_y,
        int src_stride_y,
        byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* src_a,
        int src_stride_a,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height,
        int attenuate,
        FilterMode filter);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:2075
        /// <summary>
        /// Convert I010 with Alpha to attenuated ARGB with matrix and UV filter mode.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I010AlphaToARGBMatrixFilter(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        ushort* src_a,
        int src_stride_a,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height,
        int attenuate,
        FilterMode filter);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:2093
        /// <summary>
        /// Convert I210 with Alpha to attenuated ARGB with matrix and UV filter mode.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I210AlphaToARGBMatrixFilter(ushort* src_y,
        int src_stride_y,
        ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        ushort* src_a,
        int src_stride_a,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height,
        int attenuate,
        FilterMode filter);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:2106
        /// <summary>
        /// Convert P010 to ARGB with matrix and UV filter mode.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int P010ToARGBMatrixFilter(ushort* src_y,
        int src_stride_y,
        ushort* src_uv,
        int src_stride_uv,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height,
        FilterMode filter);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:2119
        /// <summary>
        /// Convert P210 to ARGB with matrix and UV filter mode.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int P210ToARGBMatrixFilter(ushort* src_y,
        int src_stride_y,
        ushort* src_uv,
        int src_stride_uv,
        byte* dst_argb,
        int dst_stride_argb,
        YuvConstants* yuvconstants,
        int width,
        int height,
        FilterMode filter);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:2132
        /// <summary>
        /// Convert P010 to AR30 with matrix and UV filter mode.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int P010ToAR30MatrixFilter(ushort* src_y,
        int src_stride_y,
        ushort* src_uv,
        int src_stride_uv,
        byte* dst_ar30,
        int dst_stride_ar30,
        YuvConstants* yuvconstants,
        int width,
        int height,
        FilterMode filter);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:2145
        /// <summary>
        /// Convert P210 to AR30 with matrix and UV filter mode.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int P210ToAR30MatrixFilter(ushort* src_y,
        int src_stride_y,
        ushort* src_uv,
        int src_stride_uv,
        byte* dst_ar30,
        int dst_stride_ar30,
        YuvConstants* yuvconstants,
        int width,
        int height,
        FilterMode filter);

        // C:\code2\libyuv\src\include\libyuv\convert_argb.h:2181
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

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:57
        /// <summary>
        /// Convert 8 bit YUV to 12 bit.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420ToI012(byte* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:73
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

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:98
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

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:112
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

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:126
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

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:138
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

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:150
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

        // C:\code2\libyuv\src\include\libyuv\convert_from.h:195
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

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:90
        /// <summary>
        /// Aliases
        /// Convert ARGB To RGB24.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToRGB24(byte* src_argb,
        int src_stride_argb,
        byte* dst_rgb24,
        int dst_stride_rgb24,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:99
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

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:108
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

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:122
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

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:131
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

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:140
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

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:153
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
        /// Convert ARGB to AR64.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToAR64(byte* src_argb,
        int src_stride_argb,
        ushort* dst_ar64,
        int dst_stride_ar64,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:174
        /// <summary>
        /// Convert ABGR to AB64.
        /// Convert ARGB to AB64.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBToAB64(byte* src_argb,
        int src_stride_argb,
        ushort* dst_ab64,
        int dst_stride_ab64,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:190
        /// <summary>
        /// Convert ABGR to AR64.
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

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:216
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

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:229
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

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:238
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

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:247
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

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:256
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

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:265
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

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:276
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

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:287
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

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:298
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

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:309
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

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:318
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

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:327
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

        // C:\code2\libyuv\src\include\libyuv\convert_from_argb.h:338
        /// <summary>
        /// RAW to JNV21 full range NV21
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RAWToJNV21(byte* src_raw,
        int src_stride_raw,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_vu,
        int dst_stride_vu,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\cpu_id.h:60
        /// <summary>
        /// Internal flag to indicate cpuid requires initialization.
        /// These flags are only valid on ARM processors.
        /// 0x8 reserved for future ARM flag.
        /// These flags are only valid on x86 processors.
        /// These flags are only valid on MIPS processors.
        /// These flags are only valid on LOONGARCH processors.
        /// Optional init function. TestCpuFlag does an auto-init.
        /// Returns cpu_info flags.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int InitCpuFlags();

        // C:\code2\libyuv\src\include\libyuv\cpu_id.h:77
        /// <summary>
        /// Detect CPU has SSE2 etc.
        /// Test_flag parameter should be one of kCpuHas constants above.
        /// Returns non-zero if inion set is detected
        /// Internal function for parsing /proc/cpuinfo.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ArmCpuCaps(char* cpuinfo_name);

        // C:\code2\libyuv\src\include\libyuv\cpu_id.h:79
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int MipsCpuCaps(char* cpuinfo_name);

        // C:\code2\libyuv\src\include\libyuv\cpu_id.h:88
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

        // C:\code2\libyuv\src\include\libyuv\cpu_id.h:119
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:93
        /// <summary>
        /// Convert a plane of tiles of 16 x H to linear.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void DetilePlane(byte* src_y,
        int src_stride_y,
        byte* dst_y,
        int dst_stride_y,
        int width,
        int height,
        int tile_height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:105
        /// <summary>
        /// Convert a UV plane of tiles of 16 x H into linear U and V planes.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void DetileSplitUVPlane(byte* src_uv,
        int src_stride_uv,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height,
        int tile_height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:116
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:127
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:139
        /// <summary>
        /// Split interleaved msb UV plane into separate lsb U and V planes.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void SplitUVPlane_16(ushort* src_uv,
        int src_stride_uv,
        ushort* dst_u,
        int dst_stride_u,
        ushort* dst_v,
        int dst_stride_v,
        int width,
        int height,
        int depth);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:151
        /// <summary>
        /// Merge separate lsb U and V planes into one interleaved msb UV plane.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void MergeUVPlane_16(ushort* src_u,
        int src_stride_u,
        ushort* src_v,
        int src_stride_v,
        ushort* dst_uv,
        int dst_stride_uv,
        int width,
        int height,
        int depth);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:161
        /// <summary>
        /// Convert lsb plane to msb plane
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void ConvertToMSBPlane_16(ushort* src_y,
        int src_stride_y,
        ushort* dst_y,
        int dst_stride_y,
        int width,
        int height,
        int depth);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:171
        /// <summary>
        /// Convert msb plane to lsb plane
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void ConvertToLSBPlane_16(ushort* src_y,
        int src_stride_y,
        ushort* dst_y,
        int dst_stride_y,
        int width,
        int height,
        int depth);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:184
        /// <summary>
        /// Scale U and V to half width and height and merge into interleaved UV plane.
        /// width and height are source size, allowing odd sizes.
        /// Use for converting I444 or I422 to NV12.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void HalfMergeUVPlane(byte* src_u,
        int src_stride_u,
        byte* src_v,
        int src_stride_v,
        byte* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:193
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:206
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:219
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:235
        /// <summary>
        /// Split interleaved ARGB plane into separate R, G, B and A planes.
        /// dst_a can be NULL to discard alpha plane.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void SplitARGBPlane(byte* src_argb,
        int src_stride_argb,
        byte* dst_r,
        int dst_stride_r,
        byte* dst_g,
        int dst_stride_g,
        byte* dst_b,
        int dst_stride_b,
        byte* dst_a,
        int dst_stride_a,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:251
        /// <summary>
        /// Merge separate R, G, B and A planes into one interleaved ARGB plane.
        /// src_a can be NULL to fill opaque value to alpha.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void MergeARGBPlane(byte* src_r,
        int src_stride_r,
        byte* src_g,
        int src_stride_g,
        byte* src_b,
        int src_stride_b,
        byte* src_a,
        int src_stride_a,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:267
        /// <summary>
        /// Merge separate 'depth' bit R, G and B planes stored in lsb
        /// into one interleaved XR30 plane.
        /// depth should in range [10, 16]
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void MergeXR30Plane(ushort* src_r,
        int src_stride_r,
        ushort* src_g,
        int src_stride_g,
        ushort* src_b,
        int src_stride_b,
        byte* dst_ar30,
        int dst_stride_ar30,
        int width,
        int height,
        int depth);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:286
        /// <summary>
        /// Merge separate 'depth' bit R, G, B and A planes stored in lsb
        /// into one interleaved AR64 plane.
        /// src_a can be NULL to fill opaque value to alpha.
        /// depth should in range [1, 16]
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void MergeAR64Plane(ushort* src_r,
        int src_stride_r,
        ushort* src_g,
        int src_stride_g,
        ushort* src_b,
        int src_stride_b,
        ushort* src_a,
        int src_stride_a,
        ushort* dst_ar64,
        int dst_stride_ar64,
        int width,
        int height,
        int depth);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:305
        /// <summary>
        /// Merge separate 'depth' bit R, G, B and A planes stored in lsb
        /// into one interleaved ARGB plane.
        /// src_a can be NULL to fill opaque value to alpha.
        /// depth should in range [8, 16]
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void MergeARGB16To8Plane(ushort* src_r,
        int src_stride_r,
        ushort* src_g,
        int src_stride_g,
        ushort* src_b,
        int src_stride_b,
        ushort* src_a,
        int src_stride_a,
        byte* dst_argb,
        int dst_stride_argb,
        int width,
        int height,
        int depth);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:314
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:334
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:352
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:370
        /// <summary>
        /// Copy I210 to I210.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I210Copy(ushort* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:407
        /// <summary>
        /// Copy NV12. Supports inverting.
        /// Copy NV21. Supports inverting.
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:420
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:430
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:440
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:453
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:461
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int YUY2ToY(byte* src_yuy2,
        int src_stride_yuy2,
        byte* dst_y,
        int dst_stride_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:474
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:495
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:508
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:524
        /// <summary>
        /// Alias
        /// NV12 mirror.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV12Mirror(byte* src_y,
        int src_stride_y,
        byte* src_uv,
        int src_stride_uv,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:536
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:548
        /// <summary>
        /// Alias
        /// RGB24 mirror.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RGB24Mirror(byte* src_rgb24,
        int src_stride_rgb24,
        byte* dst_rgb24,
        int dst_stride_rgb24,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:557
        /// <summary>
        /// Mirror a plane of data.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void MirrorPlane(byte* src_y,
        int src_stride_y,
        byte* dst_y,
        int dst_stride_y,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:566
        /// <summary>
        /// Mirror a plane of UV data.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void MirrorUVPlane(byte* src_uv,
        int src_stride_uv,
        byte* dst_uv,
        int dst_stride_uv,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:577
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:593
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:603
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:612
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:621
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:630
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:645
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:660
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:671
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:682
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:694
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:711
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:722
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:726
        /// <summary>
        /// Convert a buffer of bytes to floats, scale the values and store as floats.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ByteToFloat(byte* src_y, float* dst_y, float scale, int width);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:741
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:759
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:768
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:777
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:786
        /// <summary>
        /// Get function to Alpha Blend ARGB pixels and store to destination.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void* GetARGBBlend();

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:799
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:813
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:840
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:851
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:862
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:873
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:886
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:899
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:908
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:917
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:928
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:945
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:958
        /// <summary>
        /// Gaussian 5x5 blur a float plane.
        /// Coefficients of 1, 4, 6, 4, 1.
        /// Each destination pixel is a blur of the 5x5
        /// pixels from the source.
        /// Source edges are clamped.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int GaussPlane_F32(float* src,
        int src_stride,
        float* dst,
        int dst_stride,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:968
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:983
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:996
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:1022
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:1031
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:1038
        /// <summary>
        /// TODO(fbarchard): Move ARGBAffineRow_SSE2 to row.h
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void ARGBAffineRow_SSE2(byte* src_argb,
        int src_argb_stride,
        byte* dst_argb,
        float* uv_dudv,
        int width);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:1049
        /// <summary>
        /// Shuffle ARGB channel order.  e.g. BGRA to ARGB.
        /// shuffler is 16 bytes.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int ARGBShuffle(byte* src_bgra,
        int src_stride_bgra,
        byte* dst_argb,
        int dst_stride_argb,
        byte* shuffler,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:1060
        /// <summary>
        /// Shuffle AR64 channel order.  e.g. AR64 to AB64.
        /// shuffler is 16 bytes.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int AR64Shuffle(ushort* src_ar64,
        int src_stride_ar64,
        ushort* dst_ar64,
        int dst_stride_ar64,
        byte* shuffler,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:1069
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:1078
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

        // C:\code2\libyuv\src\include\libyuv\planar_functions.h:1087
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
        /// Rotate I422 frame.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422Rotate(byte* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\rotate.h:85
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

        // C:\code2\libyuv\src\include\libyuv\rotate.h:101
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

        // C:\code2\libyuv\src\include\libyuv\rotate.h:121
        /// <summary>
        /// Convert Android420 to I420 with rotation.
        /// "rotation" can be 0, 90, 180 or 270.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int Android420ToI420Rotate(byte* src_y,
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
        int height,
        RotationMode rotation);

        // C:\code2\libyuv\src\include\libyuv\rotate.h:131
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

        // C:\code2\libyuv\src\include\libyuv\rotate.h:140
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

        // C:\code2\libyuv\src\include\libyuv\rotate.h:148
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void RotatePlane180(byte* src,
        int src_stride,
        byte* dst,
        int dst_stride,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\rotate.h:156
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void RotatePlane270(byte* src,
        int src_stride,
        byte* dst,
        int dst_stride,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\rotate.h:172
        /// <summary>
        /// Rotations for when U and V are interleaved.
        /// These functions take one UV input pointer and
        /// split the data into two buffers while
        /// rotating them.
        /// width and height expected to be half size for NV12.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int SplitRotateUV(byte* src_uv,
        int src_stride_uv,
        byte* dst_u,
        int dst_stride_u,
        byte* dst_v,
        int dst_stride_v,
        int width,
        int height,
        RotationMode mode);

        // C:\code2\libyuv\src\include\libyuv\rotate.h:182
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void SplitRotateUV90(byte* src,
        int src_stride,
        byte* dst_a,
        int dst_stride_a,
        byte* dst_b,
        int dst_stride_b,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\rotate.h:192
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void SplitRotateUV180(byte* src,
        int src_stride,
        byte* dst_a,
        int dst_stride_a,
        byte* dst_b,
        int dst_stride_b,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\rotate.h:202
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void SplitRotateUV270(byte* src,
        int src_stride,
        byte* dst_a,
        int dst_stride_a,
        byte* dst_b,
        int dst_stride_b,
        int width,
        int height);

        // C:\code2\libyuv\src\include\libyuv\rotate.h:214
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

        // C:\code2\libyuv\src\include\libyuv\rotate.h:224
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void SplitTransposeUV(byte* src,
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

        // C:\code2\libyuv\src\include\libyuv\scale.h:61
        /// <summary>
        /// Sample is expected to be in the low 12 bits.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern void ScalePlane_12(ushort* src,
        int src_stride,
        int src_width,
        int src_height,
        ushort* dst,
        int dst_stride,
        int dst_width,
        int dst_height,
        FilterMode filtering);

        // C:\code2\libyuv\src\include\libyuv\scale.h:90
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

        // C:\code2\libyuv\src\include\libyuv\scale.h:109
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

        // C:\code2\libyuv\src\include\libyuv\scale.h:128
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I420Scale_12(ushort* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\scale.h:157
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

        // C:\code2\libyuv\src\include\libyuv\scale.h:176
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

        // C:\code2\libyuv\src\include\libyuv\scale.h:195
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I444Scale_12(ushort* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\scale.h:223
        /// <summary>
        /// Scales a YUV 4:2:2 image from the src width and height to the
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
        public static extern int I422Scale(byte* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\scale.h:242
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422Scale_16(ushort* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\scale.h:261
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int I422Scale_12(ushort* src_y,
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

        // C:\code2\libyuv\src\include\libyuv\scale.h:286
        /// <summary>
        /// Scales an NV12 image from the src width and height to the
        /// dst width and height.
        /// If filtering is kFilterNone, a simple nearest-neighbor algorithm is
        /// used. This produces basic (blocky) quality at the fastest speed.
        /// If filtering is kFilterBilinear, interpolation is used to produce a better
        /// quality image, at the expense of speed.
        /// kFilterBox is not supported for the UV channel and will be treated as
        /// bilinear.
        /// Returns 0 if successful.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int NV12Scale(byte* src_y,
        int src_stride_y,
        byte* src_uv,
        int src_stride_uv,
        int src_width,
        int src_height,
        byte* dst_y,
        int dst_stride_y,
        byte* dst_uv,
        int dst_stride_uv,
        int dst_width,
        int dst_height,
        FilterMode filtering);

        // C:\code2\libyuv\src\include\libyuv\scale.h:307
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

        // C:\code2\libyuv\src\include\libyuv\scale.h:311
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

        // C:\code2\libyuv\src\include\libyuv\scale_rgb.h:34
        /// <summary>
        /// RGB can be RAW, RGB24 or YUV24
        /// RGB scales 24 bit images by converting a row at a time to ARGB
        /// and using ARGB row functions to scale, then convert to RGB.
        /// TODO(fbarchard): Allow input/output formats to be specified.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int RGBScale(byte* src_rgb,
        int src_stride_rgb,
        int src_width,
        int src_height,
        byte* dst_rgb,
        int dst_stride_rgb,
        int dst_width,
        int dst_height,
        FilterMode filtering);

        // C:\code2\libyuv\src\include\libyuv\scale_uv.h:30
        /// <summary>
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int UVScale(byte* src_uv,
        int src_stride_uv,
        int src_width,
        int src_height,
        byte* dst_uv,
        int dst_stride_uv,
        int dst_width,
        int dst_height,
        FilterMode filtering);

        // C:\code2\libyuv\src\include\libyuv\scale_uv.h:43
        /// <summary>
        /// Scale a 16 bit UV image.
        /// This function is currently incomplete, it can't handle all cases.
        /// </summary>
        [DllImport(_path, SetLastError = true)]
        public static extern int UVScale_16(ushort* src_uv,
        int src_stride_uv,
        int src_width,
        int src_height,
        ushort* dst_uv,
        int dst_stride_uv,
        int dst_width,
        int dst_height,
        FilterMode filtering);
    }
}