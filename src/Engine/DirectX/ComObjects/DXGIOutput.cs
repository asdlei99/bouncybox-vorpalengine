using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>
    ///     Proxies the <see cref="IDXGIOutput" /> COM interface.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public unsafe class DXGIOutput : ComObject<IDXGIOutput>
    {
        private ComPtr<IDXGIOutput> _dxgiOutput;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="DXGIOutput" /> type.
        /// </summary>
        public DXGIOutput(ComPtr<IDXGIOutput> dxgiOutput)
        {
            _dxgiOutput = dxgiOutput;
        }

        /// <inheritdoc />
        public override IDXGIOutput* Pointer => _dxgiOutput;

        /// <summary>
        ///     Queries the <see cref="IDXGIOutput" /> for <see cref="IDXGIOutput1" />.
        /// </summary>
        /// <param name="allowNoInterface">
        ///     A value that determines whether to allow an <see cref="Engine.Interop.Windows.E_NOINTERFACE" /> HRESULT.
        /// </param>
        // ReSharper disable once InconsistentNaming
        public DXGIOutput1? QueryDXGIOutput1(bool allowNoInterface = false)
        {
            ComPtr<IDXGIOutput1> dxgiOutput1;

            _dxgiOutput.CheckedAs(&dxgiOutput1, allowNoInterface);

            return dxgiOutput1.Get() != null ? new DXGIOutput1(dxgiOutput1) : null;
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _dxgiOutput.Dispose();

            base.Dispose(disposing);
        }
    }
}