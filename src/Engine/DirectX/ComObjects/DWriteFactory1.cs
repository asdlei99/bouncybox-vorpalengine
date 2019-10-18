using System;
using System.Globalization;
using BouncyBox.VorpalEngine.Engine.Interop;
using TerraFX.Interop;

namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>Proxies the <see cref="IDWriteFactory1" /> COM interface.</summary>
    public unsafe class DWriteFactory1 : ComObject<IDWriteFactory1>
    {
        private ComPtr<IDWriteFactory1> _dWriteFactory1;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="DWriteFactory1" /> type by proxying
        ///     <see cref="DWrite.DWriteCreateFactory" />.
        /// </summary>
        public DWriteFactory1()
        {
            Guid iid = DWrite.IID_IDWriteFactory1;

            // ReSharper disable once UnusedVariable
            fixed (ComPtr<IDWriteFactory1>* a = &_dWriteFactory1)
            {
                CheckResultHandle(
                    DWrite.DWriteCreateFactory(DWRITE_FACTORY_TYPE.DWRITE_FACTORY_TYPE_SHARED, &iid, (IUnknown**)_dWriteFactory1.GetAddressOf()),
                    $"Failed to create {nameof(IDWriteFactory1)}.");
            }
        }

        /// <inheritdoc />
        public override IDWriteFactory1* Pointer => _dWriteFactory1;

        /// <summary>Proxies <see cref="IDWriteFactory1.CreateTextFormat" />.</summary>
        public DWriteTextFormat CreateTextFormat(
            ReadOnlySpan<char> fontFamilyName,
            float fontSize,
            DWRITE_FONT_WEIGHT fontWeight = DWRITE_FONT_WEIGHT.DWRITE_FONT_WEIGHT_NORMAL,
            DWRITE_FONT_STYLE fontStyle = DWRITE_FONT_STYLE.DWRITE_FONT_STYLE_NORMAL,
            DWRITE_FONT_STRETCH fontStretch = DWRITE_FONT_STRETCH.DWRITE_FONT_STRETCH_NORMAL,
            CultureInfo? locale = null)
        {
            var dWriteTextFormat = new ComPtr<IDWriteTextFormat>();

            locale ??= CultureInfo.InvariantCulture;

            fixed (char* pFontFamilyName = fontFamilyName)
            fixed (char* localeName = locale.Name)
            {
                CheckResultHandle(
                    Pointer->CreateTextFormat(
                        (ushort*)pFontFamilyName,
                        null,
                        fontWeight,
                        fontStyle,
                        fontStretch,
                        fontSize,
                        (ushort*)localeName,
                        dWriteTextFormat.GetAddressOf()),
                    $"Failed to create {nameof(IDWriteTextFormat)}.");
            }

            return new DWriteTextFormat(dWriteTextFormat);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _dWriteFactory1.Dispose();

            base.Dispose(disposing);
        }
    }
}