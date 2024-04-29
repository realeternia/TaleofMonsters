using System.Collections.Generic;
using System.Drawing;
using ConfigDatas;
using NarlonLib.Core;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Controler.Resource;
using TaleofMonsters.DataType.Skills;

namespace TaleofMonsters.DataType.HeroSkills
{
    internal static class HeroSkillBook
    {
        static public bool IsSameGroupAndSmallLevel(int source, int target)
        {
            if (!ConfigData.HeroSkillCommonDict.ContainsKey(source) || !ConfigData.HeroSkillCommonDict.ContainsKey(target))
                return false;
            return ConfigData.HeroSkillCommonDict[source].Group == ConfigData.HeroSkillCommonDict[target].Group && ConfigData.HeroSkillCommonDict[source].Level >= ConfigData.HeroSkillCommonDict[target].Level;
        }

        static public Image GetHeroSkillCommonImage(int id)
        {
            string fname = string.Format("HeroSkill/{0}.JPG", ConfigData.HeroSkillCommonDict[id].Icon);
            if (!ImageManager.HasImage(fname))
            {
                Image image = PicLoader.Read("HeroSkill", string.Format("{0}.JPG", ConfigData.HeroSkillCommonDict[id].Icon));
                ImageManager.AddImage(fname, image);
            }
            return ImageManager.GetImage(fname);
        }

        static public Image GetHeroSkillCommonDirectImage(string icon)
        {
            string fname = string.Format("HeroSkill/{0}.JPG", icon);
            if (!ImageManager.HasImage(fname))
            {
                Image image = PicLoader.Read("HeroSkill", string.Format("{0}.JPG", icon));
                ImageManager.AddImage(fname, image);
            }
            return ImageManager.GetImage(fname);
        }

        static public Image GetHeroSkillSpecialImage(int id)
        {
            return SkillBook.GetSkillImage(ConfigData.GetHeroSkillSpecialConfig(id).SkillId);
        }

        public static AutoDictionary<int, int> GetSkillCommonAddons(int[] skillIds)
        {
            AutoDictionary<int, int> addons = new AutoDictionary<int, int>();

            foreach (int sid in skillIds)
            {
                if (sid == 0)
                    continue;

                HeroSkillCommonConfig heroSkillCommonConfig = ConfigData.GetHeroSkillCommonConfig(sid);
                if (heroSkillCommonConfig.Addon.Id != 0)
                {
                    addons[heroSkillCommonConfig.Addon.Id] += heroSkillCommonConfig.Addon.Value;
                }
            }
            return addons;
        }

        private static Dictionary<int, int[]> jobSkillSpecialDict = new Dictionary<int, int[]>();
        static public int[] GetSkillSpecialJob(int job)
        {
            if (!jobSkillSpecialDict.ContainsKey(job))
            {
                List<int> skills = new List<int>();
                foreach (HeroSkillSpecialConfig skill in ConfigData.HeroSkillSpecialDict.Values)
                {
                    if (skill.Job == 0 || skill.Job == job)
                    {
                        skills.Add(skill.Id);
                    }
                }
                jobSkillSpecialDict.Add(job, skills.ToArray());
            }
            return jobSkillSpecialDict[job];
        }

        static public Image GetSkillSpecialPreview(int id)
        {
            HeroSkillSpecialConfig heroSkillConfig = ConfigData.GetHeroSkillSpecialConfig(id);
            Skill skl = new Skill(heroSkillConfig.SkillId);
            skl.UpgradeToLevel(heroSkillConfig.Level);
            ControlPlus.TipImage tipData = new ControlPlus.TipImage();
            tipData.AddTextNewLine(skl.SkillConfig.Name, "White", 20);
            tipData.AddTextNewLine(string.Format("当前等级: {0}级", heroSkillConfig.Level), "White");
            tipData.AddLine();
            tipData.AddTextNewLine(skl.Descript, "Lime");
            return tipData.Image;
        }
    }
}
