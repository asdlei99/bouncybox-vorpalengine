using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICBitmap
    {
        public HResult Lock([Optional] WICRect* lockRect, uint flags, out WICBitmapLock? @lock)
        {
            IWICBitmapLock* pLock;
            int hr = Pointer->Lock(lockRect, flags, &pLock);

            @lock = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new WICBitmapLock(pLock) : null;

            return hr;
        }
    }
}