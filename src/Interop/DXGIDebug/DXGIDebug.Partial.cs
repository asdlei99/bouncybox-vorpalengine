using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DXGIDebug
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIDebug
    {
        public static HResult Create(out DXGIDebug? dxgiDebug)
        {
            Guid iid = TerraFX.Interop.DXGIDebug.IID_IDXGIDebug;
            IDXGIDebug* pDebug;
            int hr = TerraFX.Interop.DXGIDebug.DXGIGetDebugInterface(&iid, (void**)&pDebug);

            dxgiDebug = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DXGIDebug(pDebug) : null;

            return hr;
        }
    }
}