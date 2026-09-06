using System.Runtime.InteropServices;

namespace Lennox.LibYuvSharp
{
    public static unsafe partial class LibYuv
    {
        [DllImport(_path, EntryPoint = "ARGBAffineRow_SSE2", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        private static extern void ARGBAffineRowSse2(byte* src_argb, int src_argb_stride,
            byte* dst_argb, float* src_dudv, int width);

        /// <summary>Uses the C implementation on architectures without SSE2.</summary>
        public static void ARGBAffineRow_SSE2(byte* src_argb, int src_argb_stride,
            byte* dst_argb, float* src_dudv, int width)
        {
#if !NET461
            if (RuntimeInformation.ProcessArchitecture == Architecture.X64 ||
                RuntimeInformation.ProcessArchitecture == Architecture.X86)
                ARGBAffineRowSse2(src_argb, src_argb_stride, dst_argb, src_dudv, width);
            else
#endif
                ARGBAffineRow_C(src_argb, src_argb_stride, dst_argb, src_dudv, width);
        }
    }
}
