using NarlonLib.Math;
using TaleofMonsters.Controler.Battle.Data.MemMonster;
using TaleofMonsters.Controler.Battle.DataTent;
using TaleofMonsters.DataType.Buffs;

namespace TaleofMonsters.Controler.Battle.Data
{
    interface IMonsterAuro
    {
        void CheckAuroState();
    }

    class MonsterCommonAuro:IMonsterAuro
    {
        private LiveMonster self;
        private int buffId;
        private int level;
        private int element;
        private int race;
        private int range;
        private int target;

        public MonsterCommonAuro(LiveMonster mon, int buff, int lv, int ele, int rac, int ran, int tar)
        {
            self = mon;
            buffId = buff;
            level = lv;
            element = ele;
            race = rac;
            range = ran;
            target = tar;
        }

        #region IMonsterAuro 成员

        public void CheckAuroState()
        {
            int size = BattleInfo.Instance.MemMap.CardSize;
            foreach (LiveMonster mon in MonsterQueue.Instance.Enumerator)
            {
                if (mon.IsGhost || mon.Id == self.Id)
                    continue;

                if (mon.HasBuff(BuffEffectTypes.NoAuro) && target != 1)
                    continue;

                if ((element & (1 << mon.Avatar.MonsterConfig.Type)) != 0)
                    continue;
                if ((race & (1 << mon.Avatar.MonsterConfig.Race)) != 0)
                    continue;
                if (target == 0 && self.PPos != mon.PPos)
                    continue;
                if (target == 1 && self.PPos == mon.PPos)
                    continue;

                int truedis = MathTool.GetDistance(self.Position, mon.Position);
                if (range == -1 || range * size / 10 > truedis)
                {
                    mon.AddBuff(buffId, level, 5);
                }
            }
        }

        #endregion
    }

    class MonsterSpecificAuro : IMonsterAuro
    {
        private LiveMonster self;
        private int buffId;
        private int level;
        private int monId;

        public MonsterSpecificAuro(LiveMonster mon, int buff, int lv,int mid)
        {
            self = mon;
            buffId = buff;
            level = lv;
            monId = mid;
        }

        #region IMonsterAuro 成员

        public void CheckAuroState()
        {
            foreach (LiveMonster mon in MonsterQueue.Instance.Enumerator)
            {
                if (mon.IsGhost || mon.Avatar.Id != monId)
                    continue;

                if (mon.HasBuff(BuffEffectTypes.NoAuro))
                    continue;

                if (self.PPos != mon.PPos)
                    continue;

                mon.AddBuff(buffId, level, 5);
            }
        }

        #endregion
    }
}
