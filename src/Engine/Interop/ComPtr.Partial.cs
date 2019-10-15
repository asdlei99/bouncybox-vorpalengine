using System.Diagnostics.CodeAnalysis;
using BouncyBox.VorpalEngine.Engine.DirectX.ComObjects;
using TerraFX.Interop;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Interop
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    // ReSharper disable once UnusedTypeParameter
    public unsafe partial struct ComPtr<T>
        where T : unmanaged
    {
        public int CheckedAs<U>(ComPtr<U>* p, bool allowNoInterface = false)
            where U : unmanaged
        {
            int result = As(p);

            ComObject<IUnknown>.CheckResultHandle(result, $"Failed to query {nameof(U)}.", allowNoInterface);

            return result;
        }
    }
}