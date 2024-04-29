using System;
using System.Collections.Generic;
using System.Drawing;
using ConfigDatas;
using NarlonLib.Math;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Controler.Resource;

namespace TaleofMonsters.DataType.Cards.Monsters
{
    static class MonsterBook
    {
        private static Dictionary<int, List<int>> starMidDict;
        private static List<int> randomMonsterIdList;

        static public int GetRandMonsterId()
        {
            if (randomMonsterIdList == null)
            {
                randomMonsterIdList = new List<int>();
                foreach (MonsterConfig MonsterConfig in ConfigData.MonsterDict.Values)
                {
                    randomMonsterIdList.Add(MonsterConfig.Id);
                }
            }
            return randomMonsterIdList[MathTool.GetRandom(randomMonsterIdList.Count)];
        }

        static public int GetRandStarMid(int star)
        {
            if (starMidDict == null)
            {
                starMidDict = new Dictionary<int, List<int>>();
                foreach (MonsterConfig monsterConfig in ConfigData.MonsterDict.Values)
                {
                    if (!starMidDict.ContainsKey(monsterConfig.Star))
                    {
                        starMidDict.Add(monsterConfig.Star, new List<int>());
                    }
                    starMidDict[monsterConfig.Star].Add(monsterConfig.Id);
                }
            }

            return starMidDict[star][MathTool.GetRandom(starMidDict[star].Count)];
        }

        static public int[] GetSkillMids(int sid)
        {
            List<int> mids = new List<int>();
            foreach (MonsterConfig monsterConfig in ConfigData.MonsterDict.Values)
            {
                for (int i = 0; i < monsterConfig.Skills.Count; i++)
                {
                    if (monsterConfig.Skills[i].X == sid)
                    {
                        mids.Add(monsterConfig.Id);
                        break;
                    }
                }
            }
            return mids.ToArray();
        }

        static public string GetAttrByString(int id, string des)
        {
            MonsterConfig monsterConfig = ConfigData.GetMonsterConfig(id);
            switch (des)
            {
                case "hp": return monsterConfig.VitP.ToString();//todo
                case "race": return Core.HSTypes.I2Race(monsterConfig.Race);
                case "type": return Core.HSTypes.I2Attr(monsterConfig.Type);
                case "star": return monsterConfig.Star.ToString();
                case "atk": return monsterConfig.AtkP.ToString();
                case "def": return monsterConfig.DefP.ToString();
            }
            return "";
        }

        static public Image GetMonsterImage(int id, int width, int height)
        {
            MonsterConfig monsterConfig = ConfigData.GetMonsterConfig(id);

            string fname = string.Format("Monsters/{0}{1}x{2}", monsterConfig.Icon, width, height);
            if (!ImageManager.HasImage(fname))
            {
                Image image = PicLoader.Read("Monsters", string.Format("{0}.JPG", monsterConfig.Icon));
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
