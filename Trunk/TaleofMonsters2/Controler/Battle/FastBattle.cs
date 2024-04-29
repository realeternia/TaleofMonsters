using System;
using TaleofMonsters.Controler.Battle.Data;
using TaleofMonsters.Controler.Battle.Data.Players;
using TaleofMonsters.Controler.Battle.DataTent;
using TaleofMonsters.Controler.Battle.Components;

namespace TaleofMonsters.Controler.Battle
{
    internal class FastBattle
    {
        static FastBattle instance;
        public static FastBattle Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FastBattle();
                }
                return instance;
            }
        }

        public bool LeftWin
        {
            get { return leftWin; }
        }

        private bool gameEnd;
        private int roundMark;
        private bool leftWin;

        public void StartGame(int left, int right, string map, int tile)//初始化游戏
        {
            gameEnd = false;
            MonsterQueue.Init();
            EffectQueue.Init();
            EffectQueue.Instance.SetFast();
            FlowWordQueue.Init();
            FlowWordQueue.Instance.SetFast();
            PlayerManager.Init(left, right,1);//temp
            BattleInfo.Init(map, tile);

            using (CardList cl = new CardList())
            {
                cl.MaxCards = 6;
                PlayerManager.LeftPlayer.CardsDesk = cl;
                PlayerManager.LeftPlayer.InitialCards();
            }
            using (CardList cl = new CardList())
            {
                cl.MaxCards = 6;
                PlayerManager.RightPlayer.CardsDesk = cl;
                PlayerManager.RightPlayer.InitialCards();
            }

            roundMark = 0;
            BattleInfo.Instance.StartTime = DateTime.Now;

            TimerProc();
        }

        private void TimerProc()
        {
            while (true)
            {
                roundMark++;
                if (roundMark % 2 == 0)
                {
                    MonsterQueue.Instance.NextAction();
                    if (MonsterQueue.Instance.LeftCount == 0 || MonsterQueue.Instance.RightCount == 0)
                    {
                        EndGame();
                        break;
                    }
                }
                if (roundMark % 20 == 0)
                {
                    PlayerManager.RoundRecover(false);
                    if (roundMark % 600 == 0)
                    {
                        PlayerManager.CheckRoundCard();
                    }
                }
                if (roundMark % 10 == 0)
                {
                    AIStrategy.AIProc(PlayerManager.RightPlayer, false);
                    AIStrategy.AIProc(PlayerManager.LeftPlayer, false);
                }
                BattleInfo.Instance.Round = roundMark / 200 + 1;
            }
        }

        private void EndGame()
        {
            if (!gameEnd)
            {
                gameEnd = true;
                leftWin = MonsterQueue.Instance.LeftCount > 0;
                BattleInfo.Instance.EndTime = DateTime.Now;
                BattleInfo.Instance.LeftHPRate = PlayerManager.LeftPlayer.Anger * 100 / PlayerManager.LeftPlayer.MaxAnger;
                PlayerManager.Clear();
            }
        }
    }
}
