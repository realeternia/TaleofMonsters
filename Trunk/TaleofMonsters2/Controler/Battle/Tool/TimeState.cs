namespace TaleofMonsters.Controler.Battle.Tool
{
    class TimeState
    {
        static TimeState instance;
        public static TimeState Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TimeState();
                }
                return instance;
            }
        }

        public bool IsNight;
    }
}
