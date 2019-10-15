namespace BouncyBox.VorpalEngine.Engine.DirectX.ComObjects
{
    /// <summary>
    ///     Proxies the <see cref="ID2D1Brush" /> COM interface.
    /// </summary>
    public interface ID2D1Brush
    {
        /// <summary>
        ///     Proxies <see cref="TerraFX.Interop.ID2D1Brush.GetOpacity" /> and <see cref="TerraFX.Interop.ID2D1Brush.SetOpacity" />.
        /// </summary>
        float Opacity { get; set; }
    }
}