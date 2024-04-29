﻿using System;
using System.Collections.Generic;
using System.Drawing;
using ConfigDatas;
using NarlonLib.Math;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Controler.Resource;

namespace TaleofMonsters.DataType.Cards.Weapons
{
    static class WeaponBook
    {
        private static List<int> randomWeaponIdList;

        static public int GetRandWeaponId()
        {
            if (randomWeaponIdList == null)
            {
                randomWeaponIdList = new List<int>();
                foreach (WeaponConfig weaponConfig in ConfigData.WeaponDict.Values)
                {
                    randomWeaponIdList.Add(weaponConfig.Id);
                }
            }
            return randomWeaponIdList[MathTool.GetRandom(randomWeaponIdList.Count)];  
        }

        static public int[] GetSkillWids(int sid)
        {
            List<int> sids = new List<int>();
            foreach (WeaponConfig weaponConfig in ConfigData.WeaponDict.Values)
            {
                if (weaponConfig.SkillId==sid)
                {
                    sids.Add(weaponConfig.Id);
                }
            }
            return sids.ToArray();
        }

        static public string GetAttrByString(int id, string des)
        {
            WeaponConfig weaponConfig = ConfigData.GetWeaponConfig(id);

            switch (des)
            {
                case "attr": return Core.HSTypes.I2Attr(weaponConfig.Attr);
                case "star": return weaponConfig.Star.ToString();
                case "atf": return string.Format("{0}/{1}", weaponConfig.Atk, weaponConfig.Def);
                case "skill": return weaponConfig.SkillId == 0 ? "无" : ConfigData.GetSkillConfig(weaponConfig.SkillId).Name;
            }
            return "";
        }

        static public Image GetWeaponImage(int id, int width, int height)
        {
            WeaponConfig weaponConfig = ConfigData.GetWeaponConfig(id);
            string fname = string.Format("Weapon/{0}{1}x{2}", weaponConfig.Icon, width, height);
            if (!ImageManager.HasImage(fname))
            {
                Image image = PicLoader.Read("Weapon", string.Format("{0}.JPG", weaponConfig.Icon));
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
