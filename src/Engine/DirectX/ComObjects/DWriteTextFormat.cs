using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>Proxies the <see cref="IDWriteTextFormat" /> COM interface.</summary>
    public unsafe class DWriteTextFormat : ComObject<IDWriteTextFormat>
    {
        private ComPtr<IDWriteTextFormat> _dWriteTextFormat;

        /// <inheritdoc />
        /// <summary>Initializes a new instance of the <see cref="DWriteTextFormat" /> type.</summary>
        public DWriteTextFormat(ComPtr<IDWriteTextFormat> dWriteTextFormat)
        {
            _dWriteTextFormat = dWriteTextFormat;
        }

        /// <inheritdoc />
        public override IDWriteTextFormat* Pointer => _dWriteTextFormat;

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _dWriteTextFormat.Dispose();

            base.Dispose(disposing);
        }
    }
}