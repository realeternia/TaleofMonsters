using System;
using System.Collections.Generic;
using ConfigDatas;
using NarlonLib.Math;
using TaleofMonsters.Core.Interface;
using TaleofMonsters.DataType.Shops;
using TaleofMonsters.DataType.Skills;
using TaleofMonsters.Controler.Battle.Data.Players;

namespace TaleofMonsters.DataType.Cards.Monsters
{
    internal class Monster : ISellable
    {
        public MonsterConfig MonsterConfig;

        private int atk;
        private int def;
        private int matk;
        private int mdef;
        private int hit;
        private int dhit;
        private int spd;
        private int skl;
        private int hp;

        public int Id
        {
            get { return MonsterConfig.Id; }
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

        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }

        public Monster(int id)
        {
            MonsterConfig = ConfigData.GetMonsterConfig(id);
            UpgradeToLevel1();
        }

        public Monster(PlayerState ps, MonsterSkill[] skls)
        {
            MonsterConfig = new MonsterConfig();
            MonsterConfig.Id = 9999;
            MonsterConfig.Name = ps.name;
            MonsterConfig.Star = Math.Min(ps.level / 10, 6) + 1;
            MonsterConfig.Race = (int)MonsterRaces.Hero;
            MonsterConfig.Type = (int)MonsterAttrs.None;
            MonsterConfig.AtkP = ps.Saatk + (ps.GetTotalAttr(PlayerAttrs.Atk) * 3 + ps.GetTotalAttr(PlayerAttrs.Adp)) * 3 / 5 + 30;
            MonsterConfig.DefP = ps.Sadef + (ps.GetTotalAttr(PlayerAttrs.Def) * 10 + ps.GetTotalAttr(PlayerAttrs.Adp) * 3) / 10 + 15;
            //MonsterConfig.m = ps.Samagic + ps.GetTotalAttr(PlayerAttrs.Mag) + 5;
            //MonsterConfig.Hit = ps.Sahit + (ps.GetTotalAttr(PlayerAttrs.Atk) + ps.GetTotalAttr(PlayerAttrs.Skl) * 2) * 3 / 10 + 10;
            //MonsterConfig.Dhit = ps.Sadhit + (ps.GetTotalAttr(PlayerAttrs.Luk) * 2 + ps.GetTotalAttr(PlayerAttrs.Spd)) * 3 / 10 + 10;
            //MonsterConfig.Spd = ps.Saspd + (ps.GetTotalAttr(PlayerAttrs.Spd) * 3 + ps.GetTotalAttr(PlayerAttrs.Vit)) / 5 + 10;
            //MonsterConfig.Weapon = 15;
            MonsterConfig.Tile = 255;
            MonsterConfig.VitP = ps.Sahp + (ps.GetTotalAttr(PlayerAttrs.Vit) * 3 + ps.GetTotalAttr(PlayerAttrs.Def)) + 100;
            MonsterConfig.Arrow = ConfigData.GetJobConfig(ps.job).Arrow;
            MonsterConfig.Cover = "";

            List<RLVector3> datas = new List<RLVector3>();
            for (int i = 0; i < skls.Length; i++)
            {
                if (skls[i] != null && skls[i].SkillId > 0)
                {
                    RLVector3 value = new RLVector3();
                    value.X = skls[i].SkillId;
                    value.Y = skls[i].Percent;
                    value.Z = skls[i].Level;
                    datas.Add(value);
                }
            }
            MonsterConfig.Skills = new RLVector3List(datas.ToArray());
        }

        public bool HasSkill(int skillid)
        {
            if (MonsterConfig.Skills.Count== 0)
            {
                return false;
            }

            for (int i = 0; i < MonsterConfig.Skills.Count; i++)
            {
                if (MonsterConfig.Skills[i].X == skillid)
                {
                    return true;
                }
            }

            return false;
        }

        public int GetSellRate()
        {
            if (MonsterConfig.Star >= 7)
            {
                return 0;
            }
            return 14 - MonsterConfig.Star * 2;
        }

        public CardProductMarkTypes GetSellMark()
        {
            CardProductMarkTypes mark = CardProductMarkTypes.Null;
            if (MonsterConfig.Star > 4)
            {
                mark = CardProductMarkTypes.Only;
            }
            else if (MonsterConfig.Star < 3 && MathTool.GetRandom(10) > 7)
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
            int standardValue = (35 + MonsterConfig.Star * 5) * (level + 9) / 10 * (100 + MonsterConfig.Modify) / 100 * (100 + quality * 6) / 100;
            atk = standardValue*(100 + MonsterConfig.AtkP)/100;
            def = standardValue * (100 + MonsterConfig.DefP) / 100;
            matk = standardValue * (100 + MonsterConfig.MAtkP) / 100;
            mdef = standardValue * (100 + MonsterConfig.MDef) / 100;
            hit = standardValue * (100 + MonsterConfig.HitP) / 100;
            dhit = standardValue * (100 + MonsterConfig.DhitP) / 100;
            spd = standardValue * (100 + MonsterConfig.SpdP) / 100;
            skl = standardValue * (100 + MonsterConfig.SklP) / 100;
            hp = standardValue * (100 + MonsterConfig.VitP) / 100 * 3;
        }
    }
}
