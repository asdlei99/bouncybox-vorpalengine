using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_1
{
    /// <summary>Proxies the <see cref="ID2D1PrintControl" /> COM interface.</summary>
    public unsafe class D2D1PrintControl : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1PrintControl" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1PrintControl(ID2D1PrintControl* pointer) : base((IUnknown*)pointer)
        {
        }

        public new ID2D1PrintControl* Pointer => (ID2D1PrintControl*)base.Pointer;

        public HResult AddPage(
            ID2D1CommandList* commandList,
            D2D_SIZE_F pageSize,
            IStream* pagePrintTicketStream = null,
            ulong* tag1 = null,
            ulong* tag2 = null)
        {
            return Pointer->AddPage(commandList, pageSize, pagePrintTicketStream, tag1, tag2);
        }

        public HResult Close()
        {
            return Pointer->Close();
        }

        public static implicit operator ID2D1PrintControl*(D2D1PrintControl value)
        {
            return value.Pointer;
        }
    }
}