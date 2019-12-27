using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_3
{
    public unsafe partial class D2D1DeviceContext3
    {
        public HResult CreateSpriteBatch(out D2D1SpriteBatch? spriteBatch)
        {
            ID2D1SpriteBatch* pSpriteBatch;
            int hr = Pointer->CreateSpriteBatch(&pSpriteBatch);

            spriteBatch = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1SpriteBatch(pSpriteBatch) : null;

            return hr;
        }
    }
}