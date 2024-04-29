using ConfigDatas;
using NarlonLib.Math;
using TaleofMonsters.Core.Interface;
using TaleofMonsters.DataType.Shops;

namespace TaleofMonsters.DataType.Cards.Weapons
{
    internal class Weapon : ISellable
    {
        public WeaponConfig WeaponConfig;

        private int atk;
        private int def;
        private int matk;
        private int mdef;
        private int hit;
        private int dhit;
        private int spd;
        private int skl;

        public int Id
        {
            get { return WeaponConfig.Id; }
        }

        public int Atk
        {
            get { return atk; }
            set { atk = value; }
        }

        public int Def
        {
            get { return def; }
            set { def = value; }
        }

        public int Hit
        {
            get { return hit; }
            set { hit = value; }
        }

        public int Dhit
        {
            get { return dhit; }
            set { dhit = value; }
        }

        public int Spd
        {
            get { return spd; }
            set { spd = value; }
        }

        public int MAtk
        {
            get { return matk; }
            set { matk = value; }
        }

        public int MDef
        {
            get { return mdef; }
            set { mdef = value; }
        }

        public int Skl
        {
            get { return skl; }
            set { skl = value; }
        }

        public Weapon(int id)
        {
            WeaponConfig = ConfigData.GetWeaponConfig(id);
            UpgradeToLevel1();
        }

        public void AddAtk(int addon)
        {
            atk += addon;
        }

        public void AddDef(int addon)
        {
            def += addon;
        }

        public void AddHit(int addon)
        {
            hit += addon;
        }

        public void AddDhit(int addon)
        {
            dhit += addon;
        }

        public void AddStrengthLevel(int value)
        {
            int basedata = value*MathTool.GetSqrtMulti10(WeaponConfig.Star);
            if (WeaponConfig.Type == WeaponTypes.Weapon || WeaponConfig.Type == WeaponTypes.Scroll)
                atk += 2*basedata/10;
            else if (WeaponConfig.Type == WeaponTypes.Armor)
                def += 1*basedata/10;
        }

        public void RemoveNegaPoint()
        {
            if (atk < 0)
                atk = 0;
            if (def < 0)
                def = 0;
        }

        public override string ToString()
        {
            string s = "";
            if (WeaponConfig.Type != WeaponTypes.Scroll)
            {
                if (atk != 0) s += string.Format("攻击+{0} ", atk);
            }
            else
            {
                s += string.Format("攻击={0}", atk);
            }
            if (def != 0) s += string.Format("防御+{0} ", def);
            if (matk != 0) s += string.Format("魔攻+{0} ", matk);
            if (hit != 0) s += string.Format("命中+{0} ", hit);
            if (dhit != 0) s += string.Format("回避+{0} ", dhit);
            if (WeaponConfig.SkillId != 0)
                s += string.Format("技能-{0}{1} ", ConfigData.GetSkillConfig(WeaponConfig.SkillId).Name, WeaponConfig.Percent == 100 ? "" : "(" + WeaponConfig.Percent + "%发动)");
            return s.Replace("+-", "-");
        }

        public int GetSellRate()
        {
            if (WeaponConfig.Star >= 7)
            {
                return 0;
            }
            return 18 - WeaponConfig.Star * 2;
        }

        public CardProductMarkTypes GetSellMark()
        {
            CardProductMarkTypes mark = CardProductMarkTypes.Null;
            if (WeaponConfig.Star > 4)
            {
                mark = CardProductMarkTypes.Only;
            }
            else if (WeaponConfig.Star < 2 && MathTool.GetRandom(10) > 7)
            {
                mark = CardProductMarkTypes.Sale;
            }
            else
            {
                int roll = MathTool.GetRandom(10);
                if (roll == 0)
                    mark = CardProductMarkTypes.Hot;
                else if (roll == 1)
                    mark = CardProductMarkTypes.Gold;
            }
            return mark;
        }

        public void UpgradeToLevel1()
        {
            UpgradeToLevel(1, 0);
        }

        public void UpgradeToLevel(int level, byte quality)
        {
            int standardValue = (35 + WeaponConfig.Star * 5) * (level + 9) / 10 * (100 + WeaponConfig.Modify) / 100 * (100 + quality * 6) / 100/2;
            atk = standardValue * (WeaponConfig.Atk) / 100;
            def = standardValue * ( WeaponConfig.Def) / 100;
            matk = standardValue * ( WeaponConfig.MAtk) / 100;
            mdef = standardValue * ( WeaponConfig.MDef) / 100;
            hit = standardValue * ( WeaponConfig.Hit) / 100;
            dhit = standardValue * ( WeaponConfig.Dhit) / 100;
            spd = standardValue * ( WeaponConfig.Spd) / 100;
            skl = standardValue * ( WeaponConfig.Skl) / 100;
        }
    }
}