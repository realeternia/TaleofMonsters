﻿using System;
using System.Collections.Generic;
using System.Drawing;
using NarlonLib.Math;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Controler.Resource;
using ConfigDatas;

namespace TaleofMonsters.DataType.Skills
{
    static class SkillBook
    {
        private static List<int> randomSkillIds;

        static public int GetRandSkillId()
        {
            if (randomSkillIds==null)
            {
                randomSkillIds = new List<int>();
                foreach (SkillConfig skillConfig in ConfigData.SkillDict.Values)
                {
                    if (skillConfig.IsRandom)
                    {
                        randomSkillIds.Add(skillConfig.Id);
                    }
                }
            }
            return randomSkillIds[MathTool.GetRandom(randomSkillIds.Count)];            
        }

        public static string GetAttrByString(int id, string info)
        {
            SkillConfig skillConfig = ConfigData.SkillDict[id];

            switch (info)
            {
                case "type": return skillConfig.Type;
                case "des": return new Skill(id).Descript;
            }
            return "";
        }

        public static bool IsBasicSkill(int id)
        {
            if (id == 0)
            {
                return false;
            }
            return ConfigData.SkillDict[id].Type == "基本"; 
        }

        static public Image GetSkillImage(int id)
        {
            SkillConfig skillConfig = ConfigData.SkillDict[id];

            string fname = string.Format("Skill/{0}", skillConfig.Icon);
            if (!ImageManager.HasImage(fname))
            {
                Image image = PicLoader.Read("Skill", string.Format("{0}.JPG", skillConfig.Icon));
                ImageManager.AddImage(fname, image);
            }
            return ImageManager.GetImage(fname);
        }

        static public Image GetSkillImage(int id, int width, int height)
        {
            SkillConfig skillConfig = ConfigData.SkillDict[id];

            string fname = string.Format("Skill/{0}{1}x{2}", id, width, height);
            if (!ImageManager.HasImage(fname))
            {
                Image image = PicLoader.Read("Skill", string.Format("{0}.JPG", skillConfig.Icon));
                if (image.Width != width || image.Height != height)
                {
                    image = image.GetThumbnailImage(width, height, null, new IntPtr(0));
                }
                ImageManager.AddImage(fname, image);
            }
            return ImageManager.GetImage(fname);
        }
    }
}
