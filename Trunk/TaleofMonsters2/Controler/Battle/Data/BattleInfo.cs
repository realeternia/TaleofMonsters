using System;
using System.Collections.Generic;
using TaleofMonsters.Controler.Battle.Data.MemMap;
using TaleofMonsters.Core;

namespace TaleofMonsters.Controler.Battle.Data
{
    internal class BattleInfo
    {
        static BattleInfo instance;
        internal static BattleInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BattleInfo();
                    instance.items = new List<int>();
                    instance.cardRatePlus = new List<IntPair>();
                }
                return instance;
            }
        }

        internal static void Init(string map, int tile)
        {
            instance = new BattleInfo();
            instance.MemMap = new MemRowColumnMap(map, tile);
            instance.items = new List<int>();
            instance.cardRatePlus = new List<IntPair>();
        }

        private MemRowColumnMap memMap;
        private DateTime startTime;
        private DateTime endTime;
        private int round;
        private int leftMonsterAdd;
        private int leftWeaponAdd;
        private int leftSpellAdd;
        private int leftHeroAdd;
        private int rightMonsterAdd;
        private int rightWeaponAdd;
        private int rightSpellAdd;
        private int rightHeroAdd;
        private int leftKill;
        private int rightKill;
        private int leftHeroKill;
        private int rightHeroKill;
        private int leftHPRate;
        private List<int> items;
        private int expRatePlus;
        private int goldRatePlus;
        private List<IntPair> cardRatePlus;
        private bool playerWin;

        internal MemRowColumnMap MemMap
        {
            get { return memMap; }
            set { memMap = value; }
        }

        internal DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        internal DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        internal int Round
        {
            get { return round; }
            set { round = value; }
        }

        internal int LeftMonsterAdd
        {
            get { return leftMonsterAdd; }
            set { leftMonsterAdd = value; }
        }

        internal int LeftWeaponAdd
        {
            get { return leftWeaponAdd; }
            set { leftWeaponAdd = value; }
        }

        internal int LeftSpellAdd
        {
            get { return leftSpellAdd; }
            set { leftSpellAdd = value; }
        }

        internal int LeftHeroAdd
        {
            get { return leftHeroAdd; }
            set { leftHeroAdd = value; }
        }

        internal int RightMonsterAdd
        {
            get { return rightMonsterAdd; }
            set { rightMonsterAdd = value; }
        }

        internal int RightWeaponAdd
        {
            get { return rightWeaponAdd; }
            set { rightWeaponAdd = value; }
        }

        internal int RightSpellAdd
        {
            get { return rightSpellAdd; }
            set { rightSpellAdd = value; }
        }

        internal int RightHeroAdd
        {
            get { return rightHeroAdd; }
            set { rightHeroAdd = value; }
        }

        internal int LeftKill
        {
            get { return leftKill; }
            set { leftKill = value; }
        }

        internal int RightKill
        {
            get { return rightKill; }
            set { rightKill = value; }
        }

        internal int LeftHeroKill
        {
            get { return leftHeroKill; }
            set { leftHeroKill = value; }
        }

        internal int RightHeroKill
        {
            get { return rightHeroKill; }
            set { rightHeroKill = value; }
        }

        internal int LeftHPRate
        {
            get { return leftHPRate; }
            set { leftHPRate = value; }
        }

        internal int ExpRatePlus
        {
            get { return expRatePlus; }
            set { expRatePlus = value; }
        }

        internal int GoldRatePlus
        {
            get { return goldRatePlus; }
            set { goldRatePlus = value; }
        }

        internal void AddItemGet(int id)
        {
            items.Add(id);
        }

        internal List<int> Items
        {
            get { return items; }
        }

        internal List<IntPair> CardRatePlus
        {
            get { return cardRatePlus; }
        }

        internal void AddCardRate(int cid, int value)
        {
            int val = 0;
            for (int i = 0; i < cardRatePlus.Count; i++)
            {
                if (cardRatePlus[i].type == cid)
                {
                    val = cardRatePlus[i].value;
                    cardRatePlus.RemoveAt(i);
                    break;
                }                
            }
            val += value;
            if (val!=0)
            {
                IntPair pair = new IntPair();
                pair.type = cid;
                pair.value = Math.Min(val, 30);
                cardRatePlus.Add(pair);
            }
        }

        internal int FastWin
        {
            get { return (leftHPRate == 0 || round == 0 || round > 50) ? 0 : 1; }
        }

        internal int ZeroDie
        {
            get { return (leftHPRate == 0 || round == 0 || rightKill > 0) ? 0 : 1; }
        }

        internal int OnlyMagic
        {
            get { return (leftHPRate == 0 || round == 0 || leftMonsterAdd > 0 || leftWeaponAdd > 0) ? 0 : 1; }
        }

        internal int OnlySummon
        {
            get { return (leftHPRate == 0 || round == 0 || leftMonsterAdd > 0 || leftSpellAdd > 0) ? 0 : 1; }
        }

        internal int AlmostLost
        {
            get { return (leftHPRate == 0 || leftHPRate > 10) ? 0 : 1; }
        }

        internal bool PlayerWin
        {
            get { return playerWin; }
            set { playerWin = value; }
        }
    }
}
