using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace Lennox.LibYuvSharp.Tests
{
    [TestFixture]
    public unsafe class LibYuvTests
    {
        [Test]
        public void AllBindingsResolveToNativeExports()
        {
            var library = NativeLibrary.Load(Path.Combine(
                TestContext.CurrentContext.TestDirectory, "libyuv_internal.dll"));
            try
            {
                foreach (var method in typeof(LibYuv).GetMethods(BindingFlags.Public | BindingFlags.Static))
                {
                    var import = method.GetCustomAttribute<DllImportAttribute>();
                    if (import == null) continue;
                    Assert.That(NativeLibrary.TryGetExport(library, import.EntryPoint, out _),
                        Is.True, method.Name + " has no native export.");
                    Assert.That(import.CallingConvention, Is.EqualTo(CallingConvention.Cdecl));
                }
            }
            finally { NativeLibrary.Free(library); }
        }

        [Test]
        public void ScaleSixteenBitPlanePreservesConstantPixels()
        {
            var source = Enumerable.Repeat((ushort)1023, 16).ToArray();
            var destination = new ushort[4];
            fixed (ushort* src = source)
            fixed (ushort* dst = destination)
                LibYuv.ScalePlane_16(src, 4, 4, 4, dst, 2, 2, 2, FilterMode.Box);
            CollectionAssert.AreEqual(new ushort[] { 1023, 1023, 1023, 1023 }, destination);
        }

        [Test]
        public void RenamedUvRotationPreservesCompatibility()
        {
            var source = new byte[] { 1, 11, 2, 12, 3, 13, 4, 14 };
            var u = new byte[4];
            var v = new byte[4];
            fixed (byte* src = source)
            fixed (byte* dstU = u)
            fixed (byte* dstV = v)
                LibYuv.RotateUV90(src, 4, dstU, 2, dstV, 2, 2, 2);
            CollectionAssert.AreEqual(new byte[] { 3, 1, 4, 2 }, u);
            CollectionAssert.AreEqual(new byte[] { 13, 11, 14, 12 }, v);
        }

        private static byte[] CreateJpeg()
        {
            using (var bitmap = new Bitmap(16, 16))
            using (var graphics = Graphics.FromImage(bitmap))
            using (var stream = new MemoryStream())
            {
                graphics.Clear(Color.FromArgb(200, 100, 50));
                bitmap.Save(stream, ImageFormat.Jpeg);
                return stream.ToArray();
            }
        }

        [Test]
        public void DecodeJpegToArgbAndYuv()
        {
            var jpeg = CreateJpeg();
            var size = new UIntPtr((uint)jpeg.Length);
            var argb = new byte[16 * 16 * 4];
            var convertedArgb = new byte[argb.Length];
            var y = new byte[256];
            var u = new byte[64];
            var v = new byte[64];
            var convertedY = new byte[256];
            var convertedU = new byte[64];
            var convertedV = new byte[64];
            var uv = new byte[128];
            var vu = new byte[128];
            const uint mjpg = 0x47504a4d;
            fixed (byte* src = jpeg)
            fixed (byte* dstArgb = argb)
            fixed (byte* dstConvertedArgb = convertedArgb)
            fixed (byte* dstY = y)
            fixed (byte* dstU = u)
            fixed (byte* dstV = v)
            fixed (byte* dstConvertedY = convertedY)
            fixed (byte* dstConvertedU = convertedU)
            fixed (byte* dstConvertedV = convertedV)
            fixed (byte* dstUv = uv)
            fixed (byte* dstVu = vu)
            {
                int width = 0, height = 0;
                Assert.That(LibYuv.MJPGSize(src, size, &width, &height), Is.Zero);
                Assert.That(width, Is.EqualTo(16));
                Assert.That(height, Is.EqualTo(16));
                Assert.That(LibYuv.MJPGToARGB(src, size, dstArgb, 64, 16, 16, 16, 16), Is.Zero);
                Assert.That(LibYuv.ConvertToARGB(src, size, dstConvertedArgb, 64,
                    0, 0, 16, 16, 16, 16, RotationMode.Rotate0, mjpg), Is.Zero);
                Assert.That(LibYuv.MJPGToI420(src, size, dstY, 16, dstU, 8, dstV, 8,
                    16, 16, 16, 16), Is.Zero);
                Assert.That(LibYuv.ConvertToI420(src, size, dstConvertedY, 16,
                    dstConvertedU, 8, dstConvertedV, 8, 0, 0, 16, 16, 16, 16,
                    RotationMode.Rotate0, mjpg), Is.Zero);
                CollectionAssert.AreEqual(y, convertedY);
                CollectionAssert.AreEqual(u, convertedU);
                CollectionAssert.AreEqual(v, convertedV);
                Assert.That(LibYuv.MJPGToNV12(src, size, dstConvertedY, 16, dstUv, 16,
                    16, 16, 16, 16), Is.Zero);
                CollectionAssert.AreEqual(y, convertedY);
                Assert.That(LibYuv.MJPGToNV21(src, size, dstConvertedY, 16, dstVu, 16,
                    16, 16, 16, 16), Is.Zero);
                CollectionAssert.AreEqual(y, convertedY);
            }
            CollectionAssert.AreEqual(argb, convertedArgb);
            // Upstream MJPGToARGB uses the limited-range I420 matrix. JPEG samples
            // retain full range, so use J420ToARGB to recover the original RGB.
            fixed (byte* dst = convertedArgb)
            fixed (byte* srcY = y)
            fixed (byte* srcU = u)
            fixed (byte* srcV = v)
            {
                Assert.That(LibYuv.I420ToARGB(srcY, 16, srcU, 8, srcV, 8, dst, 64, 16, 16), Is.Zero);
                CollectionAssert.AreEqual(argb, convertedArgb);
                Assert.That(LibYuv.J420ToARGB(srcY, 16, srcU, 8, srcV, 8, dst, 64, 16, 16), Is.Zero);
            }
            for (var i = 0; i < argb.Length; i += 4)
            {
                // JPEG is lossy; ARGB is stored in B,G,R,A byte order on Windows x64.
                Assert.That(convertedArgb[i], Is.EqualTo(50).Within(4));
                Assert.That(convertedArgb[i + 1], Is.EqualTo(100).Within(4));
                Assert.That(convertedArgb[i + 2], Is.EqualTo(200).Within(4));
                Assert.That(convertedArgb[i + 3], Is.EqualTo(255));
            }
            for (var i = 0; i < u.Length; ++i)
            {
                Assert.That(uv[i * 2], Is.EqualTo(u[i]));
                Assert.That(uv[i * 2 + 1], Is.EqualTo(v[i]));
                Assert.That(vu[i * 2], Is.EqualTo(v[i]));
                Assert.That(vu[i * 2 + 1], Is.EqualTo(u[i]));
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void RejectInvalidJpeg(bool truncated)
        {
            var jpeg = truncated ? CreateJpeg().Take(32).ToArray() : new byte[32];
            var argb = new byte[16 * 16 * 4];
            fixed (byte* src = jpeg)
            fixed (byte* dst = argb)
            {
                int width = 0, height = 0;
                var size = new UIntPtr((uint)jpeg.Length);
                Assert.That(LibYuv.MJPGSize(src, size, &width, &height), Is.Not.Zero);
                Assert.That(LibYuv.MJPGToARGB(src, size, dst, 64, 16, 16, 16, 16), Is.Not.Zero);
            }
        }

        private static Stream GetResource(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var resourceName = assembly
                .GetManifestResourceNames()
                .Single(t => t.EndsWith(name));

            return assembly.GetManifestResourceStream(resourceName);
        }

        /// <summary>
        /// This basic test is to ensure that the library can and is being
        /// called into successfully. It does several lossless color space
        /// conversions and ensures the resulting data is the same as the input
        /// data.
        /// </summary>
        [Test]
        public void EnsureLossLessRoundTrip()
        {
            using (var stream = GetResource("scary_face.bmp"))
            using (var image = Image.FromStream(stream))
            using (var bmp = new Bitmap(image))
            {
                var data = bmp.LockBits(
                    new Rectangle(Point.Empty, image.Size),
                    ImageLockMode.ReadWrite,
                    PixelFormat.Format24bppRgb);

                var argbStride = image.Width * 4;
                var rgbStride = image.Width * 3;
                var dest = new byte[argbStride * image.Height];
                var dest2 = new byte[rgbStride * image.Height];
                var original = new byte[rgbStride * image.Height];

                fixed (byte* originalPtr = original)
                fixed (byte* destPtr = dest)
                fixed (byte* dest2Ptr = dest2)
                {
                    // Put the original 24bit RGB pixel data into an array for
                    // later validation.
                    Buffer.MemoryCopy(
                        (void*) data.Scan0, originalPtr,
                        original.Length, original.Length);

                    // Convert the source 24bit RGB pixel data to 32bit ARGB.
                    // This conversion is lossless.
                    LibYuv.RGB24ToARGB(
                        (byte*)data.Scan0, rgbStride,
                        destPtr, argbStride,
                        image.Width, image.Height);

                    // Convert the newly created 32bit ARGB back to the original
                    // 24bit RGB. This conversion is lossless.
                    LibYuv.ARGBToRGB24(
                        destPtr, argbStride,
                        dest2Ptr, rgbStride,
                        image.Width, image.Height);
                }

                // Ensure that the data survived the round trip.
                CollectionAssert.AreEqual(original, dest2);

                // And this is sanity check, to ensure that we're infact
                // doing something.
                CollectionAssert.AreNotEqual(original, dest);

                bmp.UnlockBits(data);
            }
        }
    }
}
