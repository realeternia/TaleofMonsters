using System;
using System.Collections.Generic;
using TaleofMonsters.Controler.Battle.Data;
using TaleofMonsters.Controler.Battle.Data.MemMonster;
using TaleofMonsters.DataType.Cards.Monsters;
using TaleofMonsters.DataType.Buffs;
using TaleofMonsters.DataType.Formulas;
using TaleofMonsters.DataType.Others;
using ConfigDatas;

namespace TaleofMonsters.DataType.Skills
{
    static class SkillAssistant
    {
        public static void CheckInitialEffect(LiveMonster src)
        {
            foreach (MemBaseSkill skill in src.Skills.ToArray())
            {
                skill.CheckInitialEffect();
            }
        }

        public static void CheckBurst(LiveMonster src, LiveMonster dest)
        {
            foreach (MemBaseSkill skill in src.Skills.ToArray())
            {
                skill.CheckBurst(src, dest,true);
            }
            foreach (MemBaseSkill skill in dest.Skills.ToArray())
            {
                skill.CheckBurst(src, dest,false);
            }
        }

        public static int GetHit(LiveMonster src, LiveMonster dest)
        {
            int rhit = (int)FormulaBook.GetHitRate(src.RealHit, dest.RealDhit);
            foreach (MemBaseSkill skill in src.Skills.ToArray())
            {
                if (skill.IsBurst(src.Id + dest.Id))
                {
                    skill.CheckHit(src, dest, ref rhit);
                }
            }
            foreach (MemBaseSkill skill in dest.Skills.ToArray())
            {
                if (skill.IsBurst(src.Id + dest.Id))
                {
                    skill.CheckHit(src, dest, ref rhit);
                }
            }
            return Math.Max(rhit, 0);
        }

        public static HitDamage GetDamage(LiveMonster src, LiveMonster dest)
        {
            int damvalue;
            DamageTypes damageType = DamageTypes.Physical;
            int element = 0;

            if (src.AttackType == (int)MonsterAttrs.None)
            {
                damvalue = (int)FormulaBook.GetPhysicalDamage(src.RealAtk, dest.RealDef);
            }
            else
            {
                element = src.AttackType;
                damvalue = (int)FormulaBook.GetMagicDamage(src.RealAtk);
                damageType = DamageTypes.Magic;
            }

            bool deathHit = false; //一击必杀
            int minDamage = 1; //最小伤害
            HitDamage damage = new HitDamage(damvalue, element, damageType);
            foreach (var skill in src.Skills.ToArray())
            {
                if (skill.IsBurst(src.Id + dest.Id))
                {
                    skill.CheckDamage(src, dest,true, damage, ref minDamage, ref deathHit);
                }
            }
            foreach (var skill in dest.Skills.ToArray())
            {
                if (skill.IsBurst(src.Id + dest.Id))
                {
                    skill.CheckDamage(src, dest,false, damage, ref minDamage, ref deathHit);
                }
            }

            damage.SetDamage(DamageTypes.All, deathHit ? dest.Life : Math.Max(damage.Value, minDamage));
            return damage;
        }

        public static void CheckHitEffectAfter(LiveMonster src, LiveMonster dest, HitDamage dam)
        {
            foreach (MemBaseSkill skill in src.Skills.ToArray())
            {
                if (skill.IsBurst(src.Id + dest.Id))
                {
                    skill.CheckHitEffectAfter(src, dest,dam);
                }
            }
            foreach (MemBaseSkill skill in dest.Skills.ToArray())
            {
                if (skill.IsBurst(src.Id + dest.Id))
                {
                    skill.CheckHitEffectAfter(src, dest,dam);
                }
            }
        }

        public static void CheckTimelySkillEffect(LiveMonster src)
        {
            foreach (MemBaseSkill skill in src.Skills.ToArray())
            {
                skill.CheckTimelySkillEffect();
            }
        }

        public static bool CheckSpecial(LiveMonster src)
        {
            foreach (MemBaseSkill skill in src.Skills.ToArray())
            {
                if (skill.CheckSpecial())
                    return true;
            }
            return false;
        }

        public static void CheckAuroState(LiveMonster src, TileMatchResult tileMatching)
        {
            int tileBuffId = src.TileBuff == 0 ? (int) BuffIds.Tile : src.TileBuff;
            src.CheckAuroEffect();

            if (tileMatching == TileMatchResult.Enhance && !src.HasBuff(BuffEffectTypes.NoTile))//地形
            {
                src.AddBuff(tileBuffId, 1, 5);
            }
            else if (tileMatching == TileMatchResult.Weaken)
            {
                src.AddBuff((int)BuffIds.Dtile, 1, 5);
            }
        }

        public static int GetMagicDamage(LiveMonster dest, HitDamage damage)
        {
            if (damage.Dtype == DamageTypes.Magic)
            {
                dest.CheckMagicDamage(damage);
            }
            return damage.Value;
        }

        public static List<MemBaseSkill> GetMemSkillDataForMonster(LiveMonster liveMonster)
        {
            List<MemBaseSkill> skills = new List<MemBaseSkill>();
            for (int i = 0; i < liveMonster.Avatar.MonsterConfig.Skills.Count; i++)
            {
                RLVector3 skillData = liveMonster.Avatar.MonsterConfig.Skills[i];
                int sklLevel = skillData.Z != 0 ? skillData.Z : liveMonster.Level;
                Skill skl = new Skill(skillData.X);
                skl.UpgradeToLevel(sklLevel);
                MemBaseSkill baseSkill = new MemBaseSkill(skl, skillData.Y);
                baseSkill.Self = liveMonster;
                baseSkill.Level = sklLevel;
                skills.Add(baseSkill);
            }

            return skills;
        }
    }
}

