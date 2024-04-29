using System;
using System.Drawing;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Controler.Resource;
using ConfigDatas;

namespace TaleofMonsters.DataType.Buffs
{
    static class BuffBook
    {
        public static bool IsImmune(int id, int race)
        {
            BuffConfig buffConfig = ConfigData.BuffDict[id];
            int tp = 1 << race;
            return (buffConfig.Immune & tp) != 0;
        }

        public static bool HasEffect(int id, BuffEffectTypes etype)
        {
            BuffConfig buffConfig = ConfigData.BuffDict[id];
            foreach (int eff in buffConfig.Effect)
            {
                if ((int)etype == eff)
                {
                    return true;
                }
            }
            return false;
        }

        static public Image GetBuffImage(int id)
        {
            BuffConfig buffConfig = ConfigData.BuffDict[id];

            string fname = string.Format("Buff/{0}", buffConfig.Icon);
            if (!ImageManager.HasImage(fname))
            {
                Image image = PicLoader.Read("Buff", string.Format("{0}.PNG", buffConfig.Icon));
                ImageManager.AddImage(fname, image.GetThumbnailImage(32, 16, null, new IntPtr(0)));
            }
            return ImageManager.GetImage(fname);
        }
    }
}
