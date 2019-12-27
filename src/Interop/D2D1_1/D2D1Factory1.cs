using System;
using System.Runtime.InteropServices;
using BouncyBox.VorpalEngine.Interop.D2D1;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.D2D1_1
{
    /// <summary>Proxies the <see cref="ID2D1Factory1" /> COM interface.</summary>
    public unsafe partial class D2D1Factory1 : D2D1Factory
    {
        /// <summary>Initializes a new instance of the <see cref="D2D1Factory1" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public D2D1Factory1(ID2D1Factory1* pointer) : base((ID2D1Factory*)pointer)
        {
        }

        public new ID2D1Factory1* Pointer => (ID2D1Factory1*)base.Pointer;

        public HResult CreateDevice(IDXGIDevice* dxgiDevice, ID2D1Device** d2dDevice)
        {
            return Pointer->CreateDevice(dxgiDevice, d2dDevice);
        }

        public HResult CreateDrawingStateBlock1(D2D1_DRAWING_STATE_DESCRIPTION1* drawingStateDescription, ID2D1DrawingStateBlock1** drawingStateBlock)
        {
            return Pointer->CreateDrawingStateBlock(drawingStateDescription, drawingStateBlock);
        }

        public HResult CreateDrawingStateBlock1(ID2D1DrawingStateBlock1** drawingStateBlock)
        {
            return Pointer->CreateDrawingStateBlock(drawingStateBlock);
        }

        public HResult CreateDrawingStateBlock1(
            [Optional] D2D1_DRAWING_STATE_DESCRIPTION1* drawingStateDescription,
            IDWriteRenderingParams* textRenderingParams,
            ID2D1DrawingStateBlock1** drawingStateBlock)
        {
            return Pointer->CreateDrawingStateBlock(drawingStateDescription, textRenderingParams, drawingStateBlock);
        }

        public HResult CreateGdiMetafile(IStream* metafileStream, ID2D1GdiMetafile** metafile)
        {
            return Pointer->CreateGdiMetafile(metafileStream, metafile);
        }

        public HResult CreatePathGeometry1(ID2D1PathGeometry1** pathGeometry)
        {
            return Pointer->CreatePathGeometry(pathGeometry);
        }

        public HResult CreateStrokeStyle1(
            D2D1_STROKE_STYLE_PROPERTIES1* strokeStyleProperties,
            [Optional] float* dashes,
            uint dashesCount,
            ID2D1StrokeStyle1** strokeStyle)
        {
            return Pointer->CreateStrokeStyle(strokeStyleProperties, dashes, dashesCount, strokeStyle);
        }

        public HResult GetEffectProperties(Guid* effectId, ID2D1Properties** properties)
        {
            return Pointer->GetEffectProperties(effectId, properties);
        }

        public HResult GetRegisteredEffects(Guid* effects, uint effectsCount, uint* effectsReturned = null, uint* effectsRegistered = null)
        {
            return Pointer->GetRegisteredEffects(effects, effectsCount, effectsReturned, effectsRegistered);
        }

        public HResult RegisterEffectFromStream(
            Guid* classId,
            IStream* propertyXml,
            [Optional] D2D1_PROPERTY_BINDING* bindings,
            uint bindingsCount,
            IntPtr effectFactory)
        {
            return Pointer->RegisterEffectFromStream(classId, propertyXml, bindings, bindingsCount, effectFactory);
        }

        public HResult RegisterEffectFromString(Guid* classId, ushort* propertyXml, D2D1_PROPERTY_BINDING* bindings, uint bindingsCount, IntPtr effectFactory)
        {
            return Pointer->RegisterEffectFromString(classId, propertyXml, bindings, bindingsCount, effectFactory);
        }

        public HResult UnregisterEffect(Guid* classId)
        {
            return Pointer->UnregisterEffect(classId);
        }

        public static implicit operator ID2D1Factory1*(D2D1Factory1 value)
        {
            return value.Pointer;
        }
    }
}