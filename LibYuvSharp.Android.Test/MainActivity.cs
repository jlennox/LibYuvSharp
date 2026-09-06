using System;
using Android.App;
using Android.OS;
using Android.Util;
using Lennox.LibYuvSharp.Tests;

namespace Lennox.LibYuvSharp.Android.Tests
{
    [Activity(Name = "dev.libyuvsharp.tests.MainActivity", MainLauncher = true, Exported = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            try
            {
                var tests = new LibYuvTests();
                tests.AllBindingsResolveToNativeExports();
                tests.ScaleSixteenBitPlanePreservesConstantPixels();
                tests.RenamedUvRotationPreservesCompatibility();
                tests.AffineRowWorksOnEveryArchitecture();
                tests.DecodeJpegToArgbAndYuv();
                tests.RejectInvalidJpeg(false);
                tests.RejectInvalidJpeg(true);
                tests.EnsureLossLessRoundTrip();
                Log.Info("LibYuvSharpTests", "PASS");
            }
            catch (Exception exception)
            {
                Log.Error("LibYuvSharpTests", "FAIL: " + exception);
            }
            finally { Finish(); }
        }
    }
}
