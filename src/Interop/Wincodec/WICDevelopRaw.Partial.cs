using System.Diagnostics.CodeAnalysis;
using BouncyBox.VorpalEngine.Interop.OCIdl;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Interop.Wincodec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public unsafe partial class WICDevelopRaw
    {
        public HResult GetContrast(out double contrast)
        {
            fixed (double* pContrast = &contrast)
            {
                return Pointer->GetContrast(pContrast);
            }
        }

        public HResult GetCurrentParameterSet(out PropertyBag2? currentParameterSet)
        {
            IPropertyBag2* pCurrentParameterSet;
            int hr = Pointer->GetCurrentParameterSet(&pCurrentParameterSet);

            currentParameterSet = TerraFX.Interop.Windows.SUCCEEDED(hr) ? new PropertyBag2(pCurrentParameterSet) : null;

            return hr;
        }

        public HResult GetExposureCompensation(out double ev)
        {
            fixed (double* pEv = &ev)
            {
                return Pointer->GetExposureCompensation(pEv);
            }
        }

        public HResult GetGamma(out double gamma)
        {
            fixed (double* pGamma = &gamma)
            {
                return Pointer->GetGamma(pGamma);
            }
        }

        public HResult GetKelvinRangeInfo(out uint minKelvinTemp, out uint maxKelvinTemp, out uint kelvinTempStepValue)
        {
            fixed (uint* pMinKelvinTemp = &minKelvinTemp)
            fixed (uint* pMaxKelvinTemp = &maxKelvinTemp)
            fixed (uint* pKelvinTempStepValue = &kelvinTempStepValue)
            {
                return Pointer->GetKelvinRangeInfo(pMinKelvinTemp, pMaxKelvinTemp, pKelvinTempStepValue);
            }
        }

        public HResult GetNamedWhitePoint(out WICNamedWhitePoint whitePoint)
        {
            fixed (WICNamedWhitePoint* pWhitePoint = &whitePoint)
            {
                return Pointer->GetNamedWhitePoint(pWhitePoint);
            }
        }

        public HResult GetNoiseReduction(out double noiseReduction)
        {
            fixed (double* pNoiseReduction = &noiseReduction)
            {
                return Pointer->GetNoiseReduction(pNoiseReduction);
            }
        }

        public HResult GetRenderMode(out WICRawRenderMode renderMode)
        {
            fixed (WICRawRenderMode* pRenderMode = &renderMode)
            {
                return Pointer->GetRenderMode(pRenderMode);
            }
        }

        public HResult GetRotation(out double rotation)
        {
            fixed (double* pRotation = &rotation)
            {
                return Pointer->GetRotation(pRotation);
            }
        }

        public HResult GetSaturation(out double saturation)
        {
            fixed (double* pSaturation = &saturation)
            {
                return Pointer->GetSaturation(pSaturation);
            }
        }

        public HResult GetSharpness(out double sharpness)
        {
            fixed (double* pSharpness = &sharpness)
            {
                return Pointer->GetSharpness(pSharpness);
            }
        }

        public HResult GetTint(out double tint)
        {
            fixed (double* pTint = &tint)
            {
                return Pointer->GetTint(pTint);
            }
        }

        public HResult GetToneCurve(uint toneCurveBufferSize, out WICRawToneCurve toneCurve, out uint actualToneCurveBufferSize)
        {
            fixed (WICRawToneCurve* pToneCurve = &toneCurve)
            fixed (uint* pActualToneCurveBufferSize = &actualToneCurveBufferSize)
            {
                return Pointer->GetToneCurve(toneCurveBufferSize, pToneCurve, pActualToneCurveBufferSize);
            }
        }

        public HResult GetWhitePointKelvin(out uint whitePointKelvin)
        {
            fixed (uint* pWhitePointKelvin = &whitePointKelvin)
            {
                return Pointer->GetWhitePointKelvin(pWhitePointKelvin);
            }
        }

        public HResult GetWhitePointRGB(out uint red, out uint green, out uint blue)
        {
            fixed (uint* pRed = &red)
            fixed (uint* pGreen = &green)
            fixed (uint* pBlue = &blue)
            {
                return Pointer->GetWhitePointRGB(pRed, pGreen, pBlue);
            }
        }

        public HResult QueryRawCapabilitiesInfo(out WICRawCapabilitiesInfo info)
        {
            fixed (WICRawCapabilitiesInfo* pInfo = &info)
            {
                return Pointer->QueryRawCapabilitiesInfo(pInfo);
            }
        }
    }
}