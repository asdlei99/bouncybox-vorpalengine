using System;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    /// <summary>Proxies the <see cref="IDWriteFactory" /> COM interface.</summary>
    public unsafe partial class DWriteFactory : ComObject
    {
        /// <summary>Initializes a new instance of the <see cref="DWriteFactory" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public DWriteFactory(IDWriteFactory* pointer) : base((IUnknown*)pointer)
        {
        }

        public new IDWriteFactory* Pointer => (IDWriteFactory*)base.Pointer;

        public HResult CreateCustomFontCollection(
            IDWriteFontCollectionLoader* collectionLoader,
            void* collectionKey,
            uint collectionKeySize,
            IDWriteFontCollection** fontCollection)
        {
            return Pointer->CreateCustomFontCollection(collectionLoader, collectionKey, collectionKeySize, fontCollection);
        }

        public HResult CreateCustomFontFileReference(
            void* fontFileReferenceKey,
            uint fontFileReferenceKeySize,
            IDWriteFontFileLoader* fontFileLoader,
            IDWriteFontFile** fontFile)
        {
            return Pointer->CreateCustomFontFileReference(fontFileReferenceKey, fontFileReferenceKeySize, fontFileLoader, fontFile);
        }

        public HResult CreateCustomRenderingParams(
            float gamma,
            float enhancedContrast,
            float clearTypeLevel,
            DWRITE_PIXEL_GEOMETRY pixelGeometry,
            DWRITE_RENDERING_MODE renderingMode,
            IDWriteRenderingParams** renderingParams)
        {
            return Pointer->CreateCustomRenderingParams(gamma, enhancedContrast, clearTypeLevel, pixelGeometry, renderingMode, renderingParams);
        }

        public HResult CreateEllipsisTrimmingSign(IDWriteTextFormat* textFormat, IDWriteInlineObject** trimmingSign)
        {
            return Pointer->CreateEllipsisTrimmingSign(textFormat, trimmingSign);
        }

        public HResult CreateFontFace(
            DWRITE_FONT_FACE_TYPE fontFaceType,
            uint numberOfFiles,
            IDWriteFontFile** fontFiles,
            uint faceIndex,
            DWRITE_FONT_SIMULATIONS fontFaceSimulationFlags,
            IDWriteFontFace** fontFace)
        {
            return Pointer->CreateFontFace(fontFaceType, numberOfFiles, fontFiles, faceIndex, fontFaceSimulationFlags, fontFace);
        }

        public HResult CreateFontFileReference(ushort* filePath, [Optional] FILETIME* lastWriteTime, IDWriteFontFile** fontFile)
        {
            return Pointer->CreateFontFileReference(filePath, lastWriteTime, fontFile);
        }

        public HResult CreateGdiCompatibleTextLayout(
            ushort* @string,
            uint stringLength,
            IDWriteTextFormat* textFormat,
            float layoutWidth,
            float layoutHeight,
            float pixelsPerDip,
            [Optional] DWRITE_MATRIX* transform,
            bool useGdiNatural,
            IDWriteTextLayout** textLayout)
        {
            return Pointer->CreateGdiCompatibleTextLayout(
                @string,
                stringLength,
                textFormat,
                layoutWidth,
                layoutHeight,
                pixelsPerDip,
                transform,
                useGdiNatural ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                textLayout);
        }

        public HResult CreateGlyphRunAnalysis(
            DWRITE_GLYPH_RUN* glyphRun,
            float pixelsPerDip,
            [Optional] DWRITE_MATRIX* transform,
            DWRITE_RENDERING_MODE renderingMode,
            DWRITE_MEASURING_MODE measuringMode,
            float baselineOriginX,
            float baselineOriginY,
            IDWriteGlyphRunAnalysis** glyphRunAnalysis)
        {
            return Pointer->CreateGlyphRunAnalysis(
                glyphRun,
                pixelsPerDip,
                transform,
                renderingMode,
                measuringMode,
                baselineOriginX,
                baselineOriginY,
                glyphRunAnalysis);
        }

        public HResult CreateMonitorRenderingParams(IntPtr monitor, IDWriteRenderingParams** renderingParams)
        {
            return Pointer->CreateMonitorRenderingParams(monitor, renderingParams);
        }

        public HResult CreateNumberSubstitution(
            DWRITE_NUMBER_SUBSTITUTION_METHOD substitutionMethod,
            ushort* localeName,
            bool ignoreUserOverride,
            IDWriteNumberSubstitution** numberSubstitution)
        {
            return Pointer->CreateNumberSubstitution(
                substitutionMethod,
                localeName,
                ignoreUserOverride ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                numberSubstitution);
        }

        public HResult CreateRenderingParams(IDWriteRenderingParams** renderingParams)
        {
            return Pointer->CreateRenderingParams(renderingParams);
        }

        public HResult CreateTextAnalyzer(IDWriteTextAnalyzer** textAnalyzer)
        {
            return Pointer->CreateTextAnalyzer(textAnalyzer);
        }

        public HResult CreateTextFormat(
            ushort* fontFamilyName,
            [Optional] IDWriteFontCollection* fontCollection,
            DWRITE_FONT_WEIGHT fontWeight,
            DWRITE_FONT_STYLE fontStyle,
            DWRITE_FONT_STRETCH fontStretch,
            float fontSize,
            ushort* localeName,
            IDWriteTextFormat** textFormat)
        {
            return Pointer->CreateTextFormat(fontFamilyName, fontCollection, fontWeight, fontStyle, fontStretch, fontSize, localeName, textFormat);
        }

        public HResult CreateTextLayout(
            ushort* @string,
            uint stringLength,
            IDWriteTextFormat* textFormat,
            float maxWidth,
            float maxHeight,
            IDWriteTextLayout** textLayout)
        {
            return Pointer->CreateTextLayout(@string, stringLength, textFormat, maxWidth, maxHeight, textLayout);
        }

        public HResult CreateTypography(IDWriteTypography** typography)
        {
            return Pointer->CreateTypography(typography);
        }

        public HResult GetGdiInterop(IDWriteGdiInterop** gdiInterop)
        {
            return Pointer->GetGdiInterop(gdiInterop);
        }

        public HResult GetSystemFontCollection(IDWriteFontCollection** fontCollection, bool checkForUpdates = false)
        {
            return Pointer->GetSystemFontCollection(fontCollection, checkForUpdates ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE);
        }

        public HResult RegisterFontCollectionLoader(IDWriteFontCollectionLoader* fontCollectionLoader)
        {
            return Pointer->RegisterFontCollectionLoader(fontCollectionLoader);
        }

        public HResult RegisterFontFileLoader(IDWriteFontFileLoader* fontFileLoader)
        {
            return Pointer->RegisterFontFileLoader(fontFileLoader);
        }

        public HResult UnregisterFontCollectionLoader(IDWriteFontCollectionLoader* fontCollectionLoader)
        {
            return Pointer->UnregisterFontCollectionLoader(fontCollectionLoader);
        }

        public HResult UnregisterFontFileLoader(IDWriteFontFileLoader* fontFileLoader)
        {
            return Pointer->UnregisterFontFileLoader(fontFileLoader);
        }

        public static implicit operator IDWriteFactory*(DWriteFactory value)
        {
            return value.Pointer;
        }

        public static HResult Create(out DWriteFactory? dWriteFactory)
        {
            Guid iid = TerraFX.Interop.DWrite.IID_IDWriteFactory;
            IDWriteFactory* pDWriteFactory;
            int hr = TerraFX.Interop.DWrite.DWriteCreateFactory(DWRITE_FACTORY_TYPE.DWRITE_FACTORY_TYPE_SHARED, &iid, (IUnknown**)&pDWriteFactory);

            dWriteFactory = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFactory(pDWriteFactory) : null;

            return hr;
        }
    }
}