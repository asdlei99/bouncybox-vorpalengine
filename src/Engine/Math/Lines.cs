namespace BouncyBox.VorpalEngine.Engine.Math
{
    /// <summary>Mathematical line formulas.</summary>
    public static class Lines
    {
        /// <summary>A configurable line formula.</summary>
        /// <param name="x">The x-axis value.</param>
        /// <param name="slope">The slope of the line, usually called "m."</param>
        /// <param name="yIntercept">The y-intercept, usually called "b."</param>
        /// <returns></returns>
        public static float Line(float x, float slope, float yIntercept)
        {
            return slope * x + yIntercept;
        }
    }
}