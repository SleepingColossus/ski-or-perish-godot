using Microsoft.Xna.Framework.Audio;

namespace EndlessRacer.GameObjects
{
    internal class PlayerSounds
    {
        public SoundEffect CrashSound { get; init; }
        public SoundEffect JumpSound { get; init; }
        public SoundEffect Spin360Sound { get; init; }
        public SoundEffect WinSound { get; init; }

        public PlayerSounds(SoundEffect crashSound, SoundEffect jumpSound, SoundEffect spin360Sound, SoundEffect winSound)
        {
            CrashSound = crashSound;
            JumpSound = jumpSound;
            Spin360Sound = spin360Sound;
            WinSound = winSound;
        }
    }
}
