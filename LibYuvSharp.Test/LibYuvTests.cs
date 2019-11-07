using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace Lennox.LibYuvSharp.Tests
{
    [TestFixture]
    public unsafe class LibYuvTests
    {
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
