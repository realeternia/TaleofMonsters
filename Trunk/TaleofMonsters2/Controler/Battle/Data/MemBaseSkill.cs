using System.Collections.Generic;
using NarlonLib.Math;
using TaleofMonsters.Controler.Battle.Data.MemEffect;
using TaleofMonsters.Controler.Battle.Data.MemFlow;
using TaleofMonsters.Controler.Battle.Data.MemMonster;
using TaleofMonsters.Controler.Battle.DataTent;
using TaleofMonsters.DataType.Buffs;
using TaleofMonsters.DataType.Effects;
using TaleofMonsters.DataType.Skills;
using ConfigDatas;

namespace TaleofMonsters.Controler.Battle.Data
{
    internal class MemBaseSkill
    {
        protected int skillId;
        protected int level;
        protected int percent;
        protected Skill skillInfo;
        protected LiveMonster self;
        protected Dictionary<int, bool> burst;

        internal int SkillId
        {
            get { return skillId; }
        }

        internal int Percent
        {
            get { return percent; }
        }

        internal int Level
        {
            get { return level; }
            set { level = value; }
        }

        internal MemBaseSkill(Skill skill, int per)
        {
            skillInfo = skill;
            skillId = skill.Id;
            percent = per;
            burst= new Dictionary<int, bool>();
        }

        internal LiveMonster Self
        {
            get { return self; }
            set { self = value; }
        }

        internal bool IsBurst(int keyid)
        {
            if (!burst.ContainsKey(keyid))
            {
                return false;
            }
            return burst[keyid];
        }

        internal Skill SkillInfo
        {
            get { return skillInfo; }
        }

        internal SkillConfig SkillConfig
        {
            get { return skillInfo.SkillConfig; }
        }

        internal bool CheckRate()
        {
            return percent > MathTool.GetRandom(100);
        }

        internal void CheckBurst(LiveMonster src, LiveMonster dest,bool isActive)
        {
            bool isBurst = CheckRate();

            if (self == src && skillInfo.SkillConfig.Active == SkillActiveType.Passive)
            {
                isBurst = false;
            }
            else if (self == dest && skillInfo.SkillConfig.Active == SkillActiveType.Active)
            {
                isBurst = false;
            }

            if (skillInfo.SkillConfig.CanBurst != null && !skillInfo.SkillConfig.CanBurst(src, dest, isActive))
            {
                isBurst = false;
            }

            int key = src.Id + dest.Id;
            if(!burst.ContainsKey(key))
                burst.Add(key, isBurst);
            burst[key] = isBurst;
        }

        internal void CheckInitialEffect()
        {
            if (skillInfo.SkillConfig.OnAdd != null)
            {
                skillInfo.SkillConfig.OnAdd(self, level);
                SendEffect();
            }
        }

        internal void CheckHit(LiveMonster source, LiveMonster target, ref int hit)
        {
            if (skillInfo.SkillConfig.CheckHit!=null)
            {
                skillInfo.SkillConfig.CheckHit(source, target, ref hit, level);
                SendEffect();
            }
        }

        internal void CheckDamage(LiveMonster source, LiveMonster target, bool isActive, HitDamage damage, ref int minDamage, ref bool deathHit)
        {
            if (skillInfo.SkillConfig.CheckDamage != null)
            {
                skillInfo.SkillConfig.CheckDamage(source, target, isActive, damage, ref minDamage, ref deathHit, level);
                SendEffect();
            }
        }

        internal void CheckHitEffectAfter(LiveMonster source, LiveMonster target, HitDamage damage)
        {
            if (skillInfo.SkillConfig.AfterHit != null)
            {
                skillInfo.SkillConfig.AfterHit(source, target, damage, !target.IsAlive, level);
                SendEffect();
            }
        }

        internal void CheckTimelySkillEffect()
        {
            if (skillInfo.SkillConfig.OnRoundUpdate != null && CheckRate())
            {
                if (skillInfo.SkillConfig.CanBurst != null && !skillInfo.SkillConfig.CanBurst(self, null, true))
                {
                    return;
                }

                skillInfo.SkillConfig.OnRoundUpdate(self, level);
                SendEffect();
            }
        }

        internal bool CheckSpecial()
        {
            if (skillInfo.SkillConfig.CheckSpecial != null && CheckRate())
            {
                if (skillInfo.SkillConfig.CanBurst!=null&& !skillInfo.SkillConfig.CanBurst(self, null, true))
                {
                    return false;
                }

                if (self.HasBuff(BuffEffectTypes.NoSpecialSkill))
                {
                    return false;
                }

                skillInfo.SkillConfig.CheckSpecial(self, level);
                SendEffect();
                if (skillInfo.SkillConfig.Effect!="")
                {
                    EffectQueue.Instance.Add(new ActiveEffect(EffectBook.GetEffect(skillInfo.SkillConfig.Effect), self, false));
                }
                return true;
            }
            return false;
        }

        internal void SendEffect(int key, string exp)
        {
            if (self != null && skillId < 10000 && key != 0)
            {
                FlowWordQueue.Instance.Add(new FlowSkillInfo(skillId, self.Position, 0, "lime", 20, 50, exp.Replace("+-", "-")), true);
            }
        }

        internal void SendEffect()
        {
            SendEffect(-1, "");
        }        

    }
}
