using ConfigDatas;
using TaleofMonsters.Controler.Battle.Data.MemMonster;
using TaleofMonsters.DataType.Buffs;

namespace TaleofMonsters.Controler.Battle.Data
{
    internal class MemBaseBuff
    {
        private int level;
        private int timeLeft;
        private int timeGo;
        protected Buff buffInfo;

        internal int Id
        {
            get { return buffInfo.BuffConfig.Id; }
        }

        internal string Name
        {
            get { return BuffConfig.Name; }
        }

        internal int TimeLeft
        {
            get { return timeLeft; }
            set { timeLeft = value; }
        }

        internal int TimeGo
        {
            get { return timeGo; }
            set { timeGo = value; }
        }

        internal string Type
        {
            get { return BuffConfig.Type; }
        }

        internal int Level
        {
            get { return level; }
            set { level = value; }
        }

        internal Buff BuffInfo
        {
            get { return buffInfo; }
        }

        internal BuffConfig BuffConfig
        {
            get { return buffInfo.BuffConfig; }
        }

        internal MemBaseBuff(Buff buff, int timeLeft)
        {
            buffInfo = buff;
            this.timeLeft = timeLeft;
            timeGo = 0;
        }

        internal void OnAddBuff(LiveMonster src)
        {
            if (BuffConfig.OnAdd!=null)
            {
                BuffConfig.OnAdd(src,level);
            }
        }

        internal void OnRemoveBuff(LiveMonster src)
        {
            if (BuffConfig.OnRemove != null)
            {
                BuffConfig.OnRemove(src, level);
            }
        }

        internal void OnRoundEffect(LiveMonster src)
        {
            if (BuffConfig.OnRound != null)
            {
                BuffConfig.OnRound(src, level);
            }
        }

        internal virtual void CheckBuffEffect(LiveMonster src, int symbol)
        {
        }
    }
}