using ConfigDatas;
using TaleofMonsters.DataType.Buffs;

namespace TaleofMonsters.Controler.Battle.Data.Players
{
    internal class PlayerManager
    {
        private static Player leftPlayer;
        private static Player rightPlayer;

        public static Player LeftPlayer
        {
            get { return leftPlayer; }
        }

        public static Player RightPlayer
        {
            get { return rightPlayer; }
        }

        public static void Init(int left, int right, int rlevel)
        {
            PeopleConfig peopleConfig = ConfigData.GetPeopleConfig(right);
            if (left == 0)
            {
                switch (peopleConfig.Method)
                {
                    case "common": leftPlayer = new HumanPlayer(PlayerIndex.Player1); break;
                    case "rand": leftPlayer = new RandomPlayer(right, PlayerIndex.Player1); break;
                    default: leftPlayer = new AIPlayer(right, peopleConfig.Method, PlayerIndex.Player1, rlevel); break;
                }
            }
            else //观看比赛
            {
                leftPlayer = new AIPlayer(left, ConfigData.GetPeopleConfig(left).Emethod, PlayerIndex.Player1, rlevel);
            }

            switch (peopleConfig.Emethod)
            {
                case "common": rightPlayer = new HumanPlayer(PlayerIndex.Player2); rightPlayer.PeopleId = right; break;
                case "rand": rightPlayer = new RandomPlayer(right, PlayerIndex.Player2); break;
                case "mirror": rightPlayer = new MirrorPlayer(right, leftPlayer.Cards, PlayerIndex.Player2); break;
                default: rightPlayer = new AIPlayer(right, peopleConfig.Emethod, PlayerIndex.Player2, rlevel); break;
            }
        }

        public static void Clear()
        {
            leftPlayer = null;
            rightPlayer = null;
        }

        public static void RoundRecover(bool isFast)
        {
            leftPlayer.RoundRecover(isFast);
            rightPlayer.RoundRecover(isFast);
        }

        public static void CheckRoundCard()
        {
            leftPlayer.GetNextCard();
            rightPlayer.GetNextCard();
            if (leftPlayer.HasBuff(BuffEffectTypes.RoundCardExtra))
            {
                leftPlayer.GetNextCard();
            }
            if (rightPlayer.HasBuff(BuffEffectTypes.RoundCardExtra))
            {
                rightPlayer.GetNextCard();
            }
        }
    }
}
