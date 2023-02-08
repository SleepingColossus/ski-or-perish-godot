using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.Constants
{
    // TODO: Give this class a better name
    internal static class EngineComponents
    {
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static SpriteBatch SpriteBatch { get; private set; }

        public static void Set(GraphicsDeviceManager gdm, SpriteBatch sb)
        {
            Graphics = gdm;
            SpriteBatch = sb;
        }
    }
}
