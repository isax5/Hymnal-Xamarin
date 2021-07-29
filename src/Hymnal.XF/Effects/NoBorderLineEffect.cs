using Hymnal.XF.Constants;
using Xamarin.Forms;

namespace Hymnal.XF.Effects
{
    public class NoBorderLineEffect : RoutingEffect
    {
        public NoBorderLineEffect() : base($"{EffectConstants.Group}.{EffectConstants.NoBorderLineEffect}")
        { }
    }
}
