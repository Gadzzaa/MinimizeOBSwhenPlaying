using System;
using System.Runtime.InteropServices;

namespace OsuHG
{
    public class GammaLibrary : IDisposable
    {
        private static WinApi.RAMP? _orginalRamp;
        private static IntPtr createdDC;

        public GammaLibrary(string screenDeviceName)
        {
            createdDC = WinApi.CreateDC(null, screenDeviceName, null, IntPtr.Zero);
        }

        public static float CurrentGamma { get; private set; } = float.NaN;

        public void Dispose()
        {
            Restore();
            if (createdDC != IntPtr.Zero)
                WinApi.DeleteDC(createdDC);
        }

        public static bool Set(float gamma)
        {
            if (_orginalRamp == null && !TrySetDefaultRamp())
                return false;

            if (gamma <= 5 && gamma >= 0)
            {
                var ramp = new WinApi.RAMP();
                ramp.Red = new ushort[256];
                ramp.Green = new ushort[256];
                ramp.Blue = new ushort[256];
                for (var i = 1; i < 256; i++)
                {
                    var iArrayValue = Math.Pow((i + 1) / 256.0, gamma) * 65535 + 0.5;
                    if (iArrayValue > 65535)
                        iArrayValue = 65535;
                    ramp.Red[i] = ramp.Blue[i] = ramp.Green[i] = (ushort)iArrayValue;
                }

                CurrentGamma = gamma;
                return WinApi.SetDeviceGammaRamp(createdDC, ref ramp);
            }

            return false;
        }

        public bool Restore()
        {
            if (_orginalRamp == null)
                return false;

            var ramp = _orginalRamp.Value;
            var restored = WinApi.SetDeviceGammaRamp(createdDC, ref ramp);
            if (restored)
                CurrentGamma = float.NaN;

            return restored;
        }

        private static bool TrySetDefaultRamp()
        {
            var ramp = new WinApi.RAMP();
            if (!WinApi.GetDeviceGammaRamp(createdDC, ref ramp))
                return false;

            _orginalRamp = ramp;
            return true;
        }

        private class WinApi
        {
            [DllImport("gdi32.dll")]
            public static extern bool SetDeviceGammaRamp(IntPtr hdc, ref RAMP lpRamp);

            [DllImport("gdi32.dll")]
            public static extern bool GetDeviceGammaRamp(IntPtr hdc, ref RAMP lpRamp);

            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice,
                string lpszOutput, IntPtr lpInitData);

            [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
            public static extern bool DeleteDC([In] IntPtr hdc);

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct RAMP
            {
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
                public ushort[] Red;

                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
                public ushort[] Green;

                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
                public ushort[] Blue;
            }
        }
    }
}
