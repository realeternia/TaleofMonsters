using System;
using System.Collections.Generic;
using System.IO;
using ConfigDatas;
using NarlonLib.Core;
using TaleofMonsters.Core;
using TaleofMonsters.Core.Interface;
using TaleofMonsters.DataType.Achieves;
using TaleofMonsters.DataType.Items;

namespace TaleofMonsters.DataType.User
{
    internal class InfoBag : INlSerlizable
    {
        public GameResource resource = new GameResource();
        public int diamond;
        public int[] equipon = new int[9];
        public int[] equipoff = new int[25];
        public IntPair[] items = new IntPair[50];
        public int bagCount;
        public List<int> dayItems= new List<int>();

        [Obsolete("此数据不用存回，但目前罗技有问题")]
        public AutoDictionary<int, int> tpBonusItem = new AutoDictionary<int, int>();

        public bool CheckResource(int[] resourceInfo)
        {
            if (resource.Gold >= resourceInfo[0] &&
                resource.Lumber >= resourceInfo[1] &&
                resource.Stone >= resourceInfo[2] &&
                resource.Mercury >= resourceInfo[3] &&
                resource.Carbuncle >= resourceInfo[4] &&
                resource.Sulfur >= resourceInfo[5] &&
                resource.Gem >= resourceInfo[6])
                return true;
            return false;
        }

        public bool HasResource(GameResourceType type, int value)
        {
            return resource.Has(type, value);
        }

        public void AddDiamond(int value)
        {
            diamond += value;
            MainForm.Instance.AddTip(string.Format("|获得|Cyan|{0}||钻石", value), "White");
        }

        public void AddResource(int[] res)
        {
            resource.Gold += res[0];
            resource.Lumber += res[1];
            resource.Stone += res[2];
            resource.Mercury += res[3];
            resource.Carbuncle += res[4];
            resource.Sulfur += res[5];
            resource.Gem += res[6];

            for (int i = 0; i < 7; i++)
            {
                if (res[i] > 0)
                    MainForm.Instance.AddTip(string.Format("|获得|{0}|{1}||x{2}", HSTypes.I2ResourceColor(i), HSTypes.I2Resource(i), res[i]), "White");
            }

            AchieveBook.CheckByCheckType("resource");
        }

        public void AddResource(GameResourceType type, int value)
        {
            resource.Add(type, value);
            if (type > 0)
            {
                MainForm.Instance.AddTip(string.Format("|获得|{0}|{1}||x{2}", HSTypes.I2ResourceColor((int)type), HSTypes.I2Resource((int)type), value), "White");
                AchieveBook.CheckByCheckType("resource"); 
            }
        }

        public bool PayDiamond(int value)
        {
            if (diamond < value)
            {
                MainForm.Instance.AddTip("钻石不足", "Red");
                return false;
            }
            diamond -= value;
            MainForm.Instance.AddTip(string.Format("|失去了|Cyan|{0}||钻石,账户剩余|Cyan|{1}||钻石", value, diamond), "White");
            return true;
        }

        public void SubResource(int[] res)
        {
            resource.Gold -= res[0];
            resource.Lumber -= res[1];
            resource.Stone -= res[2];
            resource.Mercury -= res[3];
            resource.Carbuncle -= res[4];
            resource.Sulfur -= res[5];
            resource.Gem -= res[6];
        }

        public void AddEquip(int id)
        {
            EquipConfig equipConfig = ConfigData.GetEquipConfig(id);
            MainForm.Instance.AddTip(string.Format("|获得装备-|{0}|{1}", HSTypes.I2QualityColor(equipConfig.Quality), equipConfig.Name), "White");

            for (int i = 0; i < 36; i++)
            {
                if (equipoff[i] == 0)
                {
                    equipoff[i] = id;
                    return;
                }
            }
        }

        public bool DeleteEquip(int id)
        {
            for (int i = 0; i < 36; i++)
            {
                if (equipoff[i] == id)
                {
                    equipoff[i] = 0;
                    return true;
                }
            }
            return false;
        }

        public void AddItem(int id, int num)
        {
            HItemConfig itemConfig = ConfigData.GetHItemConfig(id);
            MainForm.Instance.AddTip(string.Format("|获得物品-|{0}|{1}||x{2}", HSTypes.I2RareColor(itemConfig.Rare), itemConfig.Name, num), "White");

            int max = itemConfig.MaxPile;
            if (max <= 0)
            {
                return;
            }

            if (tpBonusItem[id] > 0)
            {
                tpBonusItem[id] = Math.Max(tpBonusItem[id] - num, 0);
            }

            int count = num;
            for (int i = 0; i < bagCount; i++)
            {
                if (items[i].type == id && items[i].value < max)
                {
                    if (items[i].value + count <= max)
                    {
                        items[i].value += count;
                        return;
                    }
                    count -= max - items[i].value;
                    items[i].value = max;
                }
            }
            for (int i = 0; i < bagCount; i++)
            {
                if (items[i].type == 0)
                {
                    if (count <= max)
                    {
                        items[i].type = id;
                        items[i].value = count;
                        return;
                    }
                    items[i].type = id;
                    count -= max;
                    items[i].value = max;
                }
            }
        }

        public void UseItemByPos(int pos, int type)
        {
            if (items[pos].value <= 0)
                return;

            if (HItemAssistant.UseItemsById(items[pos].type, type))
            {
                items[pos].value--;
                if (items[pos].value <= 0)
                    items[pos].type = 0;
            }
        }

        public void ClearItemAllByPos(int pos)
        {
            items[pos].value = 0;
            items[pos].type = 0;
        }

        public void SellItemAllByPos(int pos)
        {
            if (items[pos].type > 0 && items[pos].value > 0)
            {
                int money = ConfigData.GetHItemConfig(items[pos].type).Value * items[pos].value;
                AddResource(GameResourceType.Gold, money);
            }
            ClearItemAllByPos(pos);
        }

        public int GetItemCount(int id)
        {
            int count = 0;
            for (int i = 0; i < bagCount; i++)
            {
                if (items[i].type == id)
                {
                    count += items[i].value;
                }
            }
            return count;
        }

        public int GetEquipCount(int id)
        {
            int count = 0;
            for (int i = 0; i < 25; i++)
            {
                if (equipoff[i] == id)
                {
                    count++;
                }
            }
            return count;
        }

        public void DeleteItem(int id, int num)
        {
            int count = num;
            for (int i = 0; i < bagCount; i++)
            {
                if (items[i].type == id)
                {
                    if (items[i].value > count)
                    {
                        items[i].value -= count;
                        return;
                    }
                    count -= items[i].value;
                    items[i].type = 0;
                    items[i].value = 0;
                }
            }
        }

        public void SortItem()
        {
            Array.Sort(items, new CompareByMid());
            for (int i = 0; i < 999; i++)
            {
                if (items[i].type == 0)
                    break;
                int max = ConfigData.GetHItemConfig(items[i].type).MaxPile;
                if (items[i].value < max && items[i].type == items[i + 1].type)
                {
                    if (items[i].value + items[i + 1].value <= max)
                    {
                        items[i].value = items[i].value + items[i + 1].value;
                        items[i + 1].type = 0;
                        items[i + 1].value = 0;
                    }
                    else
                    {
                        items[i + 1].value = items[i].value + items[i + 1].value - max;
                        items[i].value = max;
                    }
                }
            }
        }

        public List<IntPair> GetItemCountBySubtype(int type)
        {
            AutoDictionary<int, int> counter = new AutoDictionary<int, int>();
            for (int i = 0; i < bagCount; i++)
            {
                HItemConfig itemConfig = ConfigData.GetHItemConfig(items[i].type);
                if (itemConfig != null && itemConfig.SubType == type)
                {
                    counter[itemConfig.Id] += items[i].value;
                }
            }
            List<IntPair> datas = new List<IntPair>();
            foreach (int itemId in counter.Keys())
            {
                IntPair pairData = new IntPair();
                pairData.type = itemId;
                pairData.value = counter[itemId];
                datas.Add(pairData);
            }
            return datas;
        }

        [Obsolete("暂时没用的")]
        public bool IsItemTaskNeed(int itemid)
        {
            return tpBonusItem[itemid] > 0;
        }

        public bool GetDayItem(int id)
        {
            foreach (int dayItem in dayItems)
            {
                if (dayItem == id)
                {
                    return true;
                }
            }
            return false;
        }

        public void SetDayItem(int id)
        {
            dayItems.Add(id);
        }

        #region INlSerlizable 成员

        public void Write(BinaryWriter bw)
        {
            bw.Write(resource.Gold);
            bw.Write(resource.Lumber);
            bw.Write(resource.Stone);
            bw.Write(resource.Mercury);
            bw.Write(resource.Carbuncle);
            bw.Write(resource.Sulfur);
            bw.Write(resource.Gem);
            bw.Write(diamond);
            for (int i = 0; i < 9; i++)
            {
                bw.Write(equipon[i]);
            }
            for (int i = 0; i < 25; i++)
            {
                bw.Write(equipoff[i]);
            }
            bw.Write(bagCount);
            for (int i = 0; i < bagCount; i++)
            {
                bw.Write(items[i].type);
                bw.Write(items[i].value);
            }
            bw.Write(dayItems.Count);
            for (int i = 0; i < dayItems.Count; i++)
            {
                bw.Write(dayItems[i]);
            }
        }

        public void Read(BinaryReader br)
        {
            resource.Gold = br.ReadInt32();
            resource.Lumber = br.ReadInt32();
            resource.Stone = br.ReadInt32();
            resource.Mercury = br.ReadInt32();
            resource.Carbuncle = br.ReadInt32();
            resource.Sulfur = br.ReadInt32();
            resource.Gem = br.ReadInt32();
            diamond = br.ReadInt32();
            for (int i = 0; i < 9; i++)
            {
                equipon[i] = br.ReadInt32();
            }
            for (int i = 0; i < 25; i++)
            {
                equipoff[i] = br.ReadInt32();
            }
            bagCount = br.ReadInt32();
            items = new IntPair[bagCount];
            for (int i = 0; i < bagCount; i++)
            {
                IntPair pair = new IntPair();
                pair.type = br.ReadInt32();
                pair.value = br.ReadInt32();
                items[i] = pair;
            }
            int dayItemCount = br.ReadInt32();
            dayItems.Clear();
            for (int i = 0; i < dayItemCount; i++)
            {
                dayItems.Add(br.ReadInt32());
            }
        }

        #endregion
    }
}
