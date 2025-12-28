using TOZ.ImageFX;

namespace Q17pD.Frostwatch.Menu
{
    public class PixelationToggle : Toggle<PP_Pixelated>
    {
        protected override string PrefsKey => "PixelationValue";
    }
}
