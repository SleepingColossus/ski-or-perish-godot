using Microsoft.Xna.Framework.Graphics;

namespace EndlessRacer.GameObjects
{
    internal class PlayerSprites
    {
        public Texture2D MoveSprite { get; }
        public Texture2D JumpSprite { get; }
        public Texture2D HurtSprite { get; }
        public Texture2D WinSprite { get; }
        public Texture2D SpecialEffects { get; }


        public PlayerSprites(Texture2D moveSprite, Texture2D jumpSprite, Texture2D hurtSprite, Texture2D winSprite, Texture2D specialEffects)
        {
            MoveSprite = moveSprite;
            JumpSprite = jumpSprite;
            HurtSprite = hurtSprite;
            WinSprite = winSprite;
            SpecialEffects = specialEffects;
        }
    }
}
