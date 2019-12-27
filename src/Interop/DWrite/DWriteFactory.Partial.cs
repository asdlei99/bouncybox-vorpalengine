using System;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.DWrite
{
    public unsafe partial class DWriteFactory
    {
        public HResult CreateCustomFontCollection(
            IDWriteFontCollectionLoader* collectionLoader,
            ReadOnlySpan<byte> collectionKey,
            out DWriteFontCollection? fontCollection)
        {
            IDWriteFontCollection* pFontCollection;
            int hr;

            fixed (byte* pCollectionKey = collectionKey)
            {
                hr = Pointer->CreateCustomFontCollection(collectionLoader, pCollectionKey, (uint)collectionKey.Length, &pFontCollection);
            }

            fontCollection = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFontCollection(pFontCollection) : null;

            return hr;
        }

        public HResult CreateCustomFontFileReference(
            ReadOnlySpan<byte> fontFileReferenceKey,
            IDWriteFontFileLoader* fontFileLoader,
            out DWriteFontFile? fontFile)
        {
            IDWriteFontFile* pFontFile;
            int hr;

            fixed (byte* pFontFileReferenceKey = fontFileReferenceKey)
            {
                hr = Pointer->CreateCustomFontFileReference(pFontFileReferenceKey, (uint)fontFileReferenceKey.Length, fontFileLoader, &pFontFile);
            }

            fontFile = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFontFile(pFontFile) : null;

            return hr;
        }

        public HResult CreateCustomRenderingParams(
            float gamma,
            float enhancedContrast,
            float clearTypeLevel,
            DWRITE_PIXEL_GEOMETRY pixelGeometry,
            DWRITE_RENDERING_MODE renderingMode,
            out DWriteRenderingParams? renderingParams)
        {
            IDWriteRenderingParams* pRenderingParams;
            int hr = Pointer->CreateCustomRenderingParams(gamma, enhancedContrast, clearTypeLevel, pixelGeometry, renderingMode, &pRenderingParams);

            renderingParams = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteRenderingParams(pRenderingParams) : null;

            return hr;
        }

        public HResult CreateEllipsisTrimmingSign(IDWriteTextFormat* textFormat, out DWriteInlineObject? trimmingSign)
        {
            IDWriteInlineObject* pTrimmingSign;
            int hr = Pointer->CreateEllipsisTrimmingSign(textFormat, &pTrimmingSign);

            trimmingSign = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteInlineObject(pTrimmingSign) : null;

            return hr;
        }

        public HResult CreateFontFace(
            DWRITE_FONT_FACE_TYPE fontFaceType,
            ReadOnlySpan<Pointer<IDWriteFontFile>> fontFiles,
            uint faceIndex,
            DWRITE_FONT_SIMULATIONS fontFaceSimulationFlags,
            out DWriteFontFace? fontFace)
        {
            IDWriteFontFace* pFontFace;
            int hr;

            fixed (Pointer<IDWriteFontFile>* pFontFiles = fontFiles)
            {
                hr = Pointer->CreateFontFace(
                    fontFaceType,
                    (uint)fontFiles.Length,
                    (IDWriteFontFile**)pFontFiles,
                    faceIndex,
                    fontFaceSimulationFlags,
                    &pFontFace);
            }

            fontFace = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFontFace(pFontFace) : null;

            return hr;
        }

        public HResult CreateFontFileReference(ReadOnlySpan<char> filePath, [Optional] FILETIME* lastWriteTime, out DWriteFontFile? fontFile)
        {
            IDWriteFontFile* pFontFile;
            int hr;

            fixed (char* pFilePath = filePath)
            {
                hr = Pointer->CreateFontFileReference((ushort*)pFilePath, lastWriteTime, &pFontFile);
            }

            fontFile = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFontFile(pFontFile) : null;

            return hr;
        }

        public HResult CreateGdiCompatibleTextLayout(
            ReadOnlySpan<char> @string,
            IDWriteTextFormat* textFormat,
            float layoutWidth,
            float layoutHeight,
            float pixelsPerDip,
            [Optional] DWRITE_MATRIX* transform,
            bool useGdiNatural,
            out DWriteTextLayout? textLayout)
        {
            IDWriteTextLayout* pTextLayout;
            int hr;

            fixed (char* pString = @string)
            {
                hr = Pointer->CreateGdiCompatibleTextLayout(
                    (ushort*)pString,
                    (uint)@string.Length,
                    textFormat,
                    layoutWidth,
                    layoutHeight,
                    pixelsPerDip,
                    transform,
                    useGdiNatural ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                    &pTextLayout);
            }

            textLayout = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteTextLayout(pTextLayout) : null;

            return hr;
        }

        public HResult CreateGlyphRunAnalysis(
            DWRITE_GLYPH_RUN* glyphRun,
            float pixelsPerDip,
            [Optional] DWRITE_MATRIX* transform,
            DWRITE_RENDERING_MODE renderingMode,
            DWRITE_MEASURING_MODE measuringMode,
            float baselineOriginX,
            float baselineOriginY,
            out DWriteGlyphRunAnalysis? glyphRunAnalysis)
        {
            IDWriteGlyphRunAnalysis* pGlyphRunAnalysis;
            int hr = Pointer->CreateGlyphRunAnalysis(
                glyphRun,
                pixelsPerDip,
                transform,
                renderingMode,
                measuringMode,
                baselineOriginX,
                baselineOriginY,
                &pGlyphRunAnalysis);

            glyphRunAnalysis = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteGlyphRunAnalysis(pGlyphRunAnalysis) : null;

            return hr;
        }

        public HResult CreateMonitorRenderingParams(IntPtr monitor, out DWriteRenderingParams? renderingParams)
        {
            IDWriteRenderingParams* pRenderingParams;
            int hr = Pointer->CreateMonitorRenderingParams(monitor, &pRenderingParams);

            renderingParams = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteRenderingParams(pRenderingParams) : null;

            return hr;
        }

        public HResult CreateNumberSubstitution(
            DWRITE_NUMBER_SUBSTITUTION_METHOD substitutionMethod,
            ReadOnlySpan<char> localeName,
            bool ignoreUserOverride,
            IDWriteNumberSubstitution** numberSubstitution)
        {
            fixed (char* pLocaleName = localeName)
            {
                return Pointer->CreateNumberSubstitution(
                    substitutionMethod,
                    (ushort*)pLocaleName,
                    ignoreUserOverride ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE,
                    numberSubstitution);
            }
        }

        public HResult CreateRenderingParams(out DWriteRenderingParams? renderingParams)
        {
            IDWriteRenderingParams* pRenderingParams;
            int hr = Pointer->CreateRenderingParams(&pRenderingParams);

            renderingParams = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteRenderingParams(pRenderingParams) : null;

            return hr;
        }

        public HResult CreateTextAnalyzer(out DWriteTextAnalyzer? textAnalyzer)
        {
            IDWriteTextAnalyzer* pTextAnalyzer;
            int hr = Pointer->CreateTextAnalyzer(&pTextAnalyzer);

            textAnalyzer = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteTextAnalyzer(pTextAnalyzer) : null;

            return hr;
        }

        public HResult CreateTextFormat(
            ReadOnlySpan<char> fontFamilyName,
            IDWriteFontCollection* fontCollection,
            DWRITE_FONT_WEIGHT fontWeight,
            DWRITE_FONT_STYLE fontStyle,
            DWRITE_FONT_STRETCH fontStretch,
            float fontSize,
            ReadOnlySpan<char> localeName,
            out DWriteTextFormat? textFormat)
        {
            IDWriteTextFormat* pTextFormat;
            int hr;

            fixed (char* pFontFamilyName = fontFamilyName)
            fixed (char* pLocaleName = localeName)
            {
                hr = Pointer->CreateTextFormat(
                    (ushort*)pFontFamilyName,
                    fontCollection,
                    fontWeight,
                    fontStyle,
                    fontStretch,
                    fontSize,
                    (ushort*)pLocaleName,
                    &pTextFormat);
            }

            textFormat = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteTextFormat(pTextFormat) : null;

            return hr;
        }

        public HResult CreateTextLayout(
            ReadOnlySpan<char> @string,
            IDWriteTextFormat* textFormat,
            float maxWidth,
            float maxHeight,
            out DWriteTextLayout? textLayout)
        {
            IDWriteTextLayout* pTextLayout;
            int hr;

            fixed (char* pString = @string)
            {
                hr = Pointer->CreateTextLayout((ushort*)pString, (uint)@string.Length, textFormat, maxWidth, maxHeight, &pTextLayout);
            }

            textLayout = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteTextLayout(pTextLayout) : null;

            return hr;
        }

        public HResult CreateTypography(out DWriteTypography? typography)
        {
            IDWriteTypography* pTypography;
            int hr = Pointer->CreateTypography(&pTypography);

            typography = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteTypography(pTypography) : null;

            return hr;
        }

        public HResult GetGdiInterop(out DWriteGdiInterop? gdiInterop)
        {
            IDWriteGdiInterop* pGdiInterop;
            int hr = Pointer->GetGdiInterop(&pGdiInterop);

            gdiInterop = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteGdiInterop(pGdiInterop) : null;

            return hr;
        }

        public HResult GetSystemFontCollection(out DWriteFontCollection? fontCollection, bool checkForUpdates = false)
        {
            IDWriteFontCollection* pFontCollection;
            int hr = Pointer->GetSystemFontCollection(&pFontCollection, checkForUpdates ? TerraFX.Interop.Windows.TRUE : TerraFX.Interop.Windows.FALSE);

            fontCollection = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new DWriteFontCollection(pFontCollection) : null;

            return hr;
        }
    }
}