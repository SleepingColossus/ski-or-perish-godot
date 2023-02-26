namespace EndlessRacer.Career
{
    internal class CareerProgress
    {
        public const int FinalLevelId = 9;
        public int CurrentLevel { get; private set; }

        public CareerProgress()
        {
            CurrentLevel = 0;
        }

        public void NextLevel()
        {
            CurrentLevel++;
        }

        public bool IsCareerOver => CurrentLevel > FinalLevelId;
    }
}
