namespace EndlessRacer.GameObjects
{
    internal enum Angle
    {
        // ground
        Left,
        LeftDown1,
        LeftDown2,
        LeftDown3,
        Down,
        RightDown3,
        RightDown2,
        RightDown1,
        Right,

        // ground & air
        RightUp1,
        RightUp2,
        RightUp3,
        Up,
        LeftUp3,
        LeftUp2,
        LeftUp1,
    }

    internal static class AngleExtensions
    {
        public static double ToXIntensity(this Angle angle) =>
            angle switch
            {
                Angle.Left => -1,
                Angle.LeftDown1 => -0.75,
                Angle.LeftDown2 => -0.5,
                Angle.LeftDown3 => -0.25,
                Angle.Down => 0,
                Angle.RightDown3 => 0.25,
                Angle.RightDown2 => 0.5,
                Angle.RightDown1 => 0.75,
                Angle.Right => 1
            };

        public static double ToYIntensity(this Angle angle) =>
            angle switch
            {
                Angle.Left => 0,
                Angle.LeftDown1 => 0.25,
                Angle.LeftDown2 => 0.5,
                Angle.LeftDown3 => 0.75,
                Angle.Down => 1,
                Angle.RightDown3 => 0.75,
                Angle.RightDown2 => 0.5,
                Angle.RightDown1 => 0.25,
                Angle.Right => 0
            };
    }
}
