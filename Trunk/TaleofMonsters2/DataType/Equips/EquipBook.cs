using System.Collections.Generic;
using System.Drawing;
using ConfigDatas;
using NarlonLib.Core;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Controler.Resource;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.User;

namespace TaleofMonsters.DataType.Equips
{
    static class EquipBook
    {
        static public Image GetEquipImage(int id)
        {
            string fname = string.Format("Equip/{0}.JPG", ConfigData.GetEquipConfig(id).Url);
            if (!ImageManager.HasImage(fname))
            {
                Image image = PicLoader.Read("Equip", string.Format("{0}.JPG", ConfigData.GetEquipConfig(id).Url));
                ImageManager.AddImage(fname, image);
            }
            return ImageManager.GetImage(fname);
        }

        public static int[] GetCanMergeId()
        {
            List<int> datas = new List<int>();
            foreach (EquipConfig equipConfig in ConfigData.EquipDict.Values)
            {
                if (equipConfig.BaseId != 0)
                {
                    datas.Add(equipConfig.Id);
                }
            }
            return datas.ToArray();
        }

        static public int[] GetEquipIdByPackId(int id)
        {
            List<int> rt = new List<int>();
            foreach (EquipConfig equipConfig in ConfigData.EquipDict.Values)
            {
                if (equipConfig.PackId == id)
                    rt.Add(equipConfig.Id);
            }
            return rt.ToArray();
        }

        static public void CalculateAddons(int id, AutoDictionary<int, int> addons)
        {
            EquipConfig equipConfig = ConfigData.GetEquipConfig(id);

            for (int i = 0; i < equipConfig.Addon.Count; i++)
            {
                RLIdValue pv = equipConfig.Addon[i];
                if (pv.Id == 9)
                {
                    for (int j = 1; j < 9; j++)
                    {
                        addons[j] += pv.Value;
                    }
                }
                else if (pv.Id != 0)
                {
                    addons[pv.Id] += pv.Value;
                }
            }
        }

        static public void CalculatePackAddons(int id, AutoDictionary<int, int> addons, int addCount)
        {
            EquipPackConfig equipPackConfig = ConfigData.GetEquipPackConfig(id);

            for (int i = 1; i < equipPackConfig.Addon.Count; i++)
            {
                int type = equipPackConfig.Addon[i - 1].Id;
                int value = equipPackConfig.Addon[i - 1].Value;
                if (type == 9)
                {
                    for (int j = 1; j < 9; j++)
                    {
                        addons[i] += value;
                    }
                }
                else if (type != 0)
                {
                    addons[type] += value;
                }
            }
        }

        static public bool CanEquip(int id)
        {
            EquipConfig equipConfig = ConfigData.GetEquipConfig(id);

            if (UserProfile.InfoBasic.level < equipConfig.LvNeed)
            {
                return false;
            }

            if (equipConfig.JobNeed != 0 && UserProfile.InfoBasic.job != equipConfig.JobNeed)
            {
                return false;
            }

            return true;
        }

        static public Image GetPreview(int id)
        {
            EquipConfig equipConfig = ConfigData.GetEquipConfig(id);

            ControlPlus.TipImage tipData = new ControlPlus.TipImage();
            tipData.AddTextNewLine(equipConfig.Name, HSTypes.I2QualityColor(equipConfig.Quality), 20);
            tipData.AddTextNewLine(string.Format("       装备部位:{0}", HSTypes.I2EPosition(equipConfig.Position)), "White");
            tipData.AddTextNewLine(string.Format("       装备等级:{0}", equipConfig.Level), "White");
            tipData.AddTextNewLine("", "White");
            for (int i = 0; i < equipConfig.Addon.Count; i++)
            {
                RLIdValue pv = equipConfig.Addon[i];
                if (pv.Id != 0)
                {
                    EquipAddonConfig eAddon = ConfigData.GetEquipAddonConfig(pv.Id);
                    tipData.AddTextNewLine(string.Format(eAddon.Format, pv.Value), HSTypes.I2EaddonColor(eAddon.Rare));
                }
            }
            tipData.AddLine();
            if (equipConfig.PackId != 0)
            {
                EquipPackConfig equipPackConfig = ConfigData.GetEquipPackConfig(equipConfig.PackId);
                tipData.AddTextNewLine(equipPackConfig.Name, "Gold");
                int[] packIds = GetEquipIdByPackId(equipConfig.PackId);
                int packCount = 0;
                foreach (int equipId in packIds)
                {
                    EquipConfig equipPartConfig = ConfigData.GetEquipConfig(equipId);

                    if (equipPartConfig.Position != EquipTypes.Ring && UserProfile.InfoBag.equipon[equipPartConfig.Position] == equipPartConfig.Id)
                    {
                        tipData.AddTextNewLine(" " + equipPartConfig.Name, "Lime");
                        packCount++;
                    }
                    else if (equipPartConfig.Position == EquipTypes.Ring && (UserProfile.InfoBag.equipon[5] == equipPartConfig.Id || UserProfile.InfoBag.equipon[7] == equipPartConfig.Id))
                    {
                        tipData.AddTextNewLine(" " + equipPartConfig.Name, "Lime");
                        packCount++;
                    }
                    else
                        tipData.AddTextNewLine(" " + equipPartConfig.Name, "DarkGray");
                }

                for (int i = 2; i <= packCount; i++)
                {
                    RLIdValue add = equipPackConfig.Addon[i - 2];
                    EquipAddonConfig eAddon = ConfigData.GetEquipAddonConfig(add.Id);
                    tipData.AddTextNewLine(string.Format("{0}+{1}", eAddon.Name, add.Value), HSTypes.I2EaddonColor(eAddon.Rare));
                }
                tipData.AddLine();
            }

            if (equipConfig.JobNeed != 0)
            {
                tipData.AddTextNewLine(string.Format("需要职业:{0}", equipConfig.JobNeed), "Gray");
            }
            tipData.AddTextNewLine(string.Format("需要等级:{0}", equipConfig.LvNeed), UserProfile.InfoBasic.level < equipConfig.LvNeed ? "Red" : "Gray");
            tipData.AddTextNewLine(string.Format("出售价格:{0}", equipConfig.Value), "Yellow");
            tipData.AddImage(HSIcons.GetIconsByEName("res1"));
            tipData.AddImageXY(GetEquipImage(id), 8, 8, 48, 48, 7, 24, 32, 32);
            return tipData.Image;
        }

        public static AutoDictionary<int, int> GetEquipsAddons(int[] equipIds)
        {
            AutoDictionary<int, int> addons = new AutoDictionary<int, int>();
            Dictionary<int, int> packAddons = new Dictionary<int, int>();

            int lastId = 0;
            foreach (int eid in equipIds)
            {
                if (eid == 0)
                    continue;

                EquipConfig equip = ConfigData.GetEquipConfig(eid);
                CalculateAddons(eid, addons);
                if (equip.PackId > 0 && equip.Id != lastId)
                {
                    lastId = equip.Id;
                    if (!packAddons.ContainsKey(equip.PackId))
                    {
                        packAddons.Add(equip.PackId, 1);
                    }
                    else
                    {
                        packAddons[equip.PackId]++;
                    }
                }
            }
            foreach (int pid in packAddons.Keys)
            {
                if (packAddons[pid] > 1)
                {
                    CalculatePackAddons(pid, addons, packAddons[pid]);
                }
            }
            return addons;
        }
    }
}
