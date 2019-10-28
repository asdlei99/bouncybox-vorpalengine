using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1
{
    public unsafe partial class D2D1Resource
    {
        public D2D1Factory GetFactory()
        {
            ID2D1Factory* factory;

            Pointer->GetFactory(&factory);

            return new D2D1Factory(factory);
        }
    }
}