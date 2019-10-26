using System;
using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop.DXGIDebug
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class DXGIDebug
    {
        public static HResult Create(out DXGIDebug? dxgiDebug)
        {
            Guid iid = TerraFX.Interop.DXGIDebug.IID_IDXGIDebug;
            IDXGIDebug* ppDebug;
            int hr = TerraFX.Interop.DXGIDebug.DXGIGetDebugInterface(&iid, (void**)&ppDebug);

            dxgiDebug = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DXGIDebug(ppDebug) : null;

            return hr;
        }
    }
}