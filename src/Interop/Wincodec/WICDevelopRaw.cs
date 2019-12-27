using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    /// <summary>Proxies the <see cref="IWICDevelopRaw" /> COM interface.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICDevelopRaw : WICBitmapFrameDecode
    {
        /// <summary>Initializes a new instance of the <see cref="WICDevelopRaw" /> type.</summary>
        /// <param name="pointer">A COM object pointer.</param>
        public WICDevelopRaw(IWICDevelopRaw* pointer) : base((IWICBitmapFrameDecode*)pointer)
        {
        }

        public new IWICDevelopRaw* Pointer => (IWICDevelopRaw*)base.Pointer;

        public HResult GetContrast(double* contrast)
        {
            return Pointer->GetContrast(contrast);
        }

        public HResult GetCurrentParameterSet(IPropertyBag2** currentParameterSet)
        {
            return Pointer->GetCurrentParameterSet(currentParameterSet);
        }

        public HResult GetExposureCompensation(double* ev)
        {
            return Pointer->GetExposureCompensation(ev);
        }

        public HResult GetGamma(double* gamma)
        {
            return Pointer->GetGamma(gamma);
        }

        public HResult GetKelvinRangeInfo(uint* minKelvinTemp, uint* maxKelvinTemp, uint* kelvinTempStepValue)
        {
            return Pointer->GetKelvinRangeInfo(minKelvinTemp, maxKelvinTemp, kelvinTempStepValue);
        }

        public HResult GetNamedWhitePoint(WICNamedWhitePoint* whitePoint)
        {
            return Pointer->GetNamedWhitePoint(whitePoint);
        }

        public HResult GetNoiseReduction(double* noiseReduction)
        {
            return Pointer->GetNoiseReduction(noiseReduction);
        }

        public HResult GetRenderMode(WICRawRenderMode* renderMode)
        {
            return Pointer->GetRenderMode(renderMode);
        }

        public HResult GetRotation(double* rotation)
        {
            return Pointer->GetRotation(rotation);
        }

        public HResult GetSaturation(double* saturation)
        {
            return Pointer->GetSaturation(saturation);
        }

        public HResult GetSharpness(double* sharpness)
        {
            return Pointer->GetSharpness(sharpness);
        }

        public HResult GetTint(double* tint)
        {
            return Pointer->GetTint(tint);
        }

        public HResult GetToneCurve(uint toneCurveBufferSize, WICRawToneCurve* toneCurve, uint* actualToneCurveBufferSize)
        {
            return Pointer->GetToneCurve(toneCurveBufferSize, toneCurve, actualToneCurveBufferSize);
        }

        public HResult GetWhitePointKelvin(uint* whitePointKelvin)
        {
            return Pointer->GetWhitePointKelvin(whitePointKelvin);
        }

        public HResult GetWhitePointRGB(uint* red, uint* green, uint* blue)
        {
            return Pointer->GetWhitePointRGB(red, green, blue);
        }

        public HResult LoadParameterSet(WICRawParameterSet parameterSet)
        {
            return Pointer->LoadParameterSet(parameterSet);
        }

        public HResult QueryRawCapabilitiesInfo(WICRawCapabilitiesInfo* info)
        {
            return Pointer->QueryRawCapabilitiesInfo(info);
        }

        public HResult SetContrast(double contrast)
        {
            return Pointer->SetContrast(contrast);
        }

        public HResult SetDestinationColorContext(IWICColorContext* colorContext = null)
        {
            return Pointer->SetDestinationColorContext(colorContext);
        }

        public HResult SetExposureCompensation(double ev)
        {
            return Pointer->SetExposureCompensation(ev);
        }

        public HResult SetGamma(double gamma)
        {
            return Pointer->SetGamma(gamma);
        }

        public HResult SetNamedWhitePoint(WICNamedWhitePoint whitePoint)
        {
            return Pointer->SetNamedWhitePoint(whitePoint);
        }

        public HResult SetNoiseReduction(double noiseReduction)
        {
            return Pointer->SetNoiseReduction(noiseReduction);
        }

        public HResult SetNotificationCallback(IWICDevelopRawNotificationCallback* callback = null)
        {
            return Pointer->SetNotificationCallback(callback);
        }

        public HResult SetRenderMode(WICRawRenderMode renderMode)
        {
            return Pointer->SetRenderMode(renderMode);
        }

        public HResult SetRotation(double rotation)
        {
            return Pointer->SetRotation(rotation);
        }

        public HResult SetSaturation(double saturation)
        {
            return Pointer->SetSaturation(saturation);
        }

        public HResult SetSharpness(double sharpness)
        {
            return Pointer->SetSharpness(sharpness);
        }

        public HResult SetTint(double tint)
        {
            return Pointer->SetTint(tint);
        }

        public HResult SetToneCurve(uint toneCurveSize, WICRawToneCurve* toneCurve)
        {
            return Pointer->SetToneCurve(toneCurveSize, toneCurve);
        }

        public HResult SetWhitePointKelvin(uint whitePointKelvin)
        {
            return Pointer->SetWhitePointKelvin(whitePointKelvin);
        }

        public HResult SetWhitePointRGB(uint red, uint green, uint blue)
        {
            return Pointer->SetWhitePointRGB(red, green, blue);
        }

        public static implicit operator IWICDevelopRaw*(WICDevelopRaw value)
        {
            return value.Pointer;
        }
    }
}