using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.GameObjects
{
    internal class PlayerSprites
    {
        public Texture2D MoveSprite { get; init; }
        public Texture2D JumpSprite { get; init; }
        public Texture2D HurtSprite { get; init; }
        public Texture2D WinSprite { get; init; }

        public PlayerSprites(Texture2D moveSprite, Texture2D jumpSprite, Texture2D hurtSprite, Texture2D winSprite)
        {
            MoveSprite = moveSprite;
            JumpSprite = jumpSprite;
            HurtSprite = hurtSprite;
            WinSprite = winSprite;
        }
    }
}
