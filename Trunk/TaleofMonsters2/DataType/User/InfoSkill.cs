using System.Collections.Generic;
using System.IO;
using ConfigDatas;
using TaleofMonsters.Core.Interface;
using TaleofMonsters.DataType.HeroSkills;

namespace TaleofMonsters.DataType.User
{
    internal class InfoSkill : INlSerlizable
    {
        public List<int> skillCommons = new List<int>();
        public List<int> skillSpecials = new List<int>();//已学习列表，不一定可以使用
        public int[] skillUse = new int[5];
        private Dictionary<int, int> skillAttrs = new Dictionary<int, int>();

        public void AddSkillCommon(int id)
        {
            for (int i = 0; i < skillCommons.Count; i++)
            {
                if (HeroSkillBook.IsSameGroupAndSmallLevel(id, skillCommons[i]))
                {
                    skillCommons[i] = id;
                    return;
                }
            }
            skillCommons.Add(id);
        }

        public void AddSkillSpecial(int id)
        {
            HeroSkillSpecialConfig sourceConfig = ConfigData.GetHeroSkillSpecialConfig(id);
            for (int i = 0; i < skillSpecials.Count; i++)
            {
                HeroSkillSpecialConfig targetConfig = ConfigData.GetHeroSkillSpecialConfig(skillSpecials[i]);
                if (id == skillSpecials[i] || sourceConfig.Group == targetConfig.Group)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (skillUse[j] == targetConfig.Id)
                        {
                            skillUse[j] = id;
                        }
                    }
                    skillSpecials[i] = id;
                    return;
                }
            }
            skillSpecials.Add(id);
        }

        public int GetSkillCommonLevelByGroup(int gid)
        {
            for (int i = 0; i < skillCommons.Count; i++)
            {
                HeroSkillCommonConfig heroSkillCommonConfig = ConfigData.GetHeroSkillCommonConfig(skillCommons[i]);
                if (heroSkillCommonConfig.Group == gid)
                {
                    return heroSkillCommonConfig.Level;
                }
            }
            return 0;
        }

        public int GetSkillSpecialLevelByGroup(int gid)
        {
            for (int i = 0; i < skillSpecials.Count; i++)
            {
                HeroSkillSpecialConfig skillSpecialConfig = ConfigData.GetHeroSkillSpecialConfig(skillSpecials[i]);
                if (skillSpecialConfig.Group == gid)
                {
                    return skillSpecialConfig.Level;
                }
            }
            return 0;
        }

        public bool HasSkillCommon(int id)
        {
            foreach (int skill in skillCommons)
            {
                if (skill == id || HeroSkillBook.IsSameGroupAndSmallLevel(skill, id))
                    return true;
            }
            return false;
        }

        public bool HasSkillSpecial(int id)
        {
            foreach (int skill in skillSpecials)
            {
                if (skill == id)
                    return true;
            }
            return false;
        }

        public bool HasSkillSpecialUse(int id)
        {
            foreach (int skill in skillUse)
            {
                if (skill == id)
                    return true;
            }
            return false;
        }

        public int GetSkillAttrLevel(int sid)
        {
            if (skillAttrs.ContainsKey(sid))
            {
                return skillAttrs[sid];
            }
            return 0;
        }

        public void AddSkillAttrLevel(int sid)
        {
            if (skillAttrs.ContainsKey(sid))
            {
                skillAttrs[sid]++;
            }
            else
            {
                skillAttrs.Add(sid, 1);
            }
        }

        public void CheckSkillAttrSkill(Controler.Battle.Data.Players.PlayerState playerState)
        {
            foreach (int sid in skillAttrs.Keys)
            {
                int slevel = skillAttrs[sid];
                HeroSkillAttrConfig heroSkillAttrConfig = ConfigData.GetHeroSkillAttrConfig(sid);
                playerState.AddSkillAttr(heroSkillAttrConfig.Atk * slevel, heroSkillAttrConfig.Def * slevel, heroSkillAttrConfig.Magic * slevel, heroSkillAttrConfig.Hit * slevel, heroSkillAttrConfig.Dhit * slevel, heroSkillAttrConfig.Spd * slevel, heroSkillAttrConfig.Hp * slevel);
            }
        }

        #region INlSerlizable 成员

        public void Write(BinaryWriter bw)
        {
            bw.Write(skillCommons.Count);
            foreach (int skillCommon in skillCommons)
            {
                bw.Write(skillCommon);
            }
            bw.Write(skillSpecials.Count);
            foreach (int skillSpecial in skillSpecials)
            {
                bw.Write(skillSpecial);
            }
            for (int i = 0; i < 5; i++)
            {
                bw.Write(skillUse[i]);
            }
            bw.Write(skillAttrs.Count);
            foreach (KeyValuePair<int, int> keyValuePair in skillAttrs)
            {
                bw.Write(keyValuePair.Key);
                bw.Write(keyValuePair.Value);
            }
        }

        public void Read(BinaryReader br)
        {
            int count = br.ReadInt32();
            skillCommons.Clear();
            for (int i = 0; i < count; i++)
            {
                skillCommons.Add(br.ReadInt32());
            }
            count = br.ReadInt32();
            skillSpecials.Clear();
            for (int i = 0; i < count; i++)
            {
                skillSpecials.Add(br.ReadInt32());
            }
            for (int i = 0; i < 5; i++)
            {
                skillUse[i] = br.ReadInt32();
            }
            count = br.ReadInt32();
            skillAttrs.Clear();
            for (int i = 0; i < count; i++)
            {
                int key = br.ReadInt32();
                int value = br.ReadInt32();
                skillAttrs.Add(key, value);
            }
        }

        #endregion
    }
}
