using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>Proxies the <see cref="IDXGIOutput1" /> COM interface.</summary>
    // ReSharper disable once InconsistentNaming
    public unsafe class DXGIOutput1 : ComObject<IDXGIOutput1>
    {
        private ComPtr<IDXGIOutput1> _dxgiOutput1;

        /// <inheritdoc />
        /// <summary>Initializes a new instance of the <see cref="DXGIOutput1" /> type.</summary>
        public DXGIOutput1(ComPtr<IDXGIOutput1> dxgiOutput1)
        {
            _dxgiOutput1 = dxgiOutput1;
        }

        /// <inheritdoc />
        public override IDXGIOutput1* Pointer => _dxgiOutput1;

        /// <summary>Proxies <see cref="IDXGIOutput1.FindClosestMatchingMode1" />.</summary>
        public DXGI_MODE_DESC1 FindClosestMatchingMode1(DXGI_FORMAT format = DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM)
        {
            var modeToMatch =
                new DXGI_MODE_DESC1
                {
                    Format = format
                };
            DXGI_MODE_DESC1 closestMatch;

            CheckResultHandle(Pointer->FindClosestMatchingMode1(&modeToMatch, &closestMatch, null), "Failed to find closest matching mode.");

            return closestMatch;
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _dxgiOutput1.Dispose();

            base.Dispose(disposing);
        }
    }
}