namespace EndlessRacer.Career
{
    internal class CareerProgress
    {
        public const int FinalLevelId = 9;
        public int CurrentLevel { get; private set; }

        private static CareerProgress _instance;

        public static CareerProgress Get()
        {
            if (_instance == null)
            {
                _instance = new CareerProgress();
            }

            return _instance;
        }

        private CareerProgress()
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
