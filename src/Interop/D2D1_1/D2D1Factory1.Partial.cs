using System;
using System.Runtime.InteropServices;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_1
{
    public unsafe partial class D2D1Factory1
    {
        public HResult CreateDevice(IDXGIDevice* dxgiDevice, out D2D1Device? d2dDevice)
        {
            ID2D1Device* pD2DDevice;
            int hr = Pointer->CreateDevice(dxgiDevice, &pD2DDevice);

            d2dDevice = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Device(pD2DDevice) : null;

            return hr;
        }

        public HResult CreateDrawingStateBlock1(D2D1_DRAWING_STATE_DESCRIPTION1* drawingStateDescription, out D2D1DrawingStateBlock1? drawingStateBlock)
        {
            ID2D1DrawingStateBlock1* pDrawingStateBlock;
            int hr = Pointer->CreateDrawingStateBlock(drawingStateDescription, &pDrawingStateBlock);

            drawingStateBlock = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1DrawingStateBlock1(pDrawingStateBlock) : null;

            return hr;
        }

        public HResult CreateDrawingStateBlock1(out D2D1DrawingStateBlock1? drawingStateBlock)
        {
            ID2D1DrawingStateBlock1* pDrawingStateBlock;
            int hr = Pointer->CreateDrawingStateBlock(&pDrawingStateBlock);

            drawingStateBlock = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1DrawingStateBlock1(pDrawingStateBlock) : null;

            return hr;
        }

        public HResult CreateDrawingStateBlock1(
            [Optional] D2D1_DRAWING_STATE_DESCRIPTION1* drawingStateDescription,
            IDWriteRenderingParams* textRenderingParams,
            out D2D1DrawingStateBlock1? drawingStateBlock)
        {
            ID2D1DrawingStateBlock1* pDrawingStateBlock;
            int hr = Pointer->CreateDrawingStateBlock(drawingStateDescription, textRenderingParams, &pDrawingStateBlock);

            drawingStateBlock = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1DrawingStateBlock1(pDrawingStateBlock) : null;

            return hr;
        }

        public HResult CreateGdiMetafile(IStream* metafileStream, out D2D1GdiMetafile? metafile)
        {
            ID2D1GdiMetafile* pMetafile;
            int hr = Pointer->CreateGdiMetafile(metafileStream, &pMetafile);

            metafile = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1GdiMetafile(pMetafile) : null;

            return hr;
        }

        public HResult CreatePathGeometry1(out D2D1PathGeometry1? pathGeometry)
        {
            ID2D1PathGeometry1* pPathGeometry;
            int hr = Pointer->CreatePathGeometry(&pPathGeometry);

            pathGeometry = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1PathGeometry1(pPathGeometry) : null;

            return hr;
        }

        public HResult CreateStrokeStyle1(
            D2D1_STROKE_STYLE_PROPERTIES1* strokeStyleProperties,
            [Optional] ReadOnlySpan<float> dashes,
            out D2D1StrokeStyle1? strokeStyle)
        {
            ID2D1StrokeStyle1* pStrokeStyle;
            int hr;

            fixed (float* pDashes = dashes)
            {
                hr = Pointer->CreateStrokeStyle(strokeStyleProperties, pDashes, (uint)dashes.Length, &pStrokeStyle);
            }

            strokeStyle = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1StrokeStyle1(pStrokeStyle) : null;

            return hr;
        }

        public HResult GetEffectProperties(Guid* effectId, out D2D1Properties? properties)
        {
            ID2D1Properties* pProperties;
            int hr = Pointer->GetEffectProperties(effectId, &pProperties);

            properties = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new D2D1Properties(pProperties) : null;

            return hr;
        }

        public HResult GetRegisteredEffects(Span<Guid> effects, [Optional] uint* effectsReturned, [Optional] uint* effectsRegistered)
        {
            fixed (Guid* pEffects = effects)
            {
                return Pointer->GetRegisteredEffects(pEffects, (uint)effects.Length, effectsReturned, effectsRegistered);
            }
        }

        public HResult RegisterEffectFromStream(
            Guid* classId,
            IStream* propertyXml,
            [Optional] ReadOnlySpan<D2D1_PROPERTY_BINDING> bindings,
            IntPtr effectFactory)
        {
            fixed (D2D1_PROPERTY_BINDING* pBindings = bindings)
            {
                return Pointer->RegisterEffectFromStream(classId, propertyXml, pBindings, (uint)bindings.Length, effectFactory);
            }
        }

        public HResult RegisterEffectFromString(Guid* classId, ReadOnlySpan<char> propertyXml, ReadOnlySpan<D2D1_PROPERTY_BINDING> bindings, IntPtr effectFactory)
        {
            fixed (char* pPropertyXml = propertyXml)
            fixed (D2D1_PROPERTY_BINDING* pBindings = bindings)
            {
                return Pointer->RegisterEffectFromString(classId, (ushort*)pPropertyXml, pBindings, (uint)bindings.Length, effectFactory);
            }
        }
    }
}