﻿using System;
using System.Collections.Generic;
using System.Drawing;
using TaleofMonsters.Core;
using ConfigDatas;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.DataType.User;

namespace TaleofMonsters.DataType.Tasks
{
    static class TaskBook
    {
        public static int[] GetTaskByLevels()
        {
            List<int> tids = new List<int>();
            foreach(TaskConfig taskConfig in ConfigData.TaskDict.Values)
            {
                if(taskConfig.Level < 10)
                    tids.Add(taskConfig.Id);
            }
            return tids.ToArray();
        }

        public static List<int> GetAvailTask(int npcid)
        {
            List<int> ids = new List<int>();
            foreach (TaskConfig taskConfig in ConfigData.TaskDict.Values)
            {
                if (taskConfig.StartNpc == npcid && CanReceive(taskConfig.Id))
                {
                    ids.Add(taskConfig.Id);
                }
            }
            return ids;
        }

        public static List<int> GetFinishingTask(int npcid)
        {
            List<int> ids = new List<int>();
            foreach (TaskConfig taskConfig in ConfigData.TaskDict.Values)
            {
                if (taskConfig.EndNpc == npcid && CanFinish(taskConfig.Id))
                {
                    ids.Add(taskConfig.Id);
                }
            }
            return ids;
        }

        public static List<int> GetUnFinishingTask(int npcid)
        {
            List<int> ids = new List<int>();
            foreach (TaskConfig taskConfig in ConfigData.TaskDict.Values)
            {
                if (taskConfig.EndNpc == npcid && !CanFinish(taskConfig.Id))
                {
                    ids.Add(taskConfig.Id);
                }
            }
            return ids;
        }

        public static bool HasCard(int id)
        {
            foreach (TaskConfig taskConfig in ConfigData.TaskDict.Values)
            {
                if (taskConfig.Card == id)
                {
                    return true;
                }
            }
            return false;
        }

        static public int GetMoneyReal(int id)
        {
            TaskConfig taskConfig = ConfigData.GetTaskConfig(id);
            return (int)Math.Sqrt(ExpTree.GetNextRequired(taskConfig.Level) * 60) * taskConfig.Money / 100; 
        }

        static public int GetExpReal(int id)
        {
           TaskConfig taskConfig = ConfigData.GetTaskConfig(id);
           return ExpTree.GetNextRequired(taskConfig.Level) * taskConfig.Exp / 2000; 
        }

        static public bool CanReceive(int id)
        {
            TaskConfig taskConfig = ConfigData.GetTaskConfig(id);
            if (taskConfig.Former != 0 && UserProfile.InfoTask.GetTaskStateById(taskConfig.Former) != 3)
                return false;

            if (taskConfig.Exclude != 0 && UserProfile.InfoTask.GetTaskStateById(taskConfig.Exclude) != 0)
                return false;

            if (UserProfile.InfoTask.GetTaskStateById(id) != 0)
                return false;

            if (UserProfile.InfoBasic.level < taskConfig.Level)
                return false;

            return true;
        }

        static public string GetReceiveWord(int id)
        {
            TaskConfig taskConfig = ConfigData.GetTaskConfig(id);
            switch (taskConfig.Type)
            {
                case TaskTypes.Fight: return GetFightStr(id);
            }

            return string.Format("[Upd{0}-1]{1}", id, taskConfig.Name);
        }

        static private string GetFightStr(int id)
        {
            TaskConfig taskConfig = ConfigData.GetTaskConfig(id);
            return string.Format("[Mon{0}-{1}&Upw{2}-3]{3}", taskConfig.Content[0], taskConfig.Content[1], id, taskConfig.Name);
        }

        static public bool CanFinish(int id)
        {
            int state = UserProfile.InfoTask.GetTaskStateById(id);
            if (state == 0 || state == 3)
                return false;

            TaskConfig taskConfig = ConfigData.GetTaskConfig(id);
            switch (taskConfig.Type)
            {
                case TaskTypes.Item: return CheckItem(id);
                case TaskTypes.Fight: return state == 2;
                case TaskTypes.Talk: return true;
                case TaskTypes.Level: return UserProfile.InfoBasic.level >= taskConfig.Content[0];
                case TaskTypes.Teach: return true;
                case TaskTypes.Won: return UserProfile.InfoTask.GetTaskAddonById(id) >= taskConfig.Content[1];
                case TaskTypes.WonLevel: return UserProfile.InfoTask.GetTaskAddonById(id) >= taskConfig.Content[1];
                case TaskTypes.Resource:
                    GameResource res = new GameResource();
                    res.Add((GameResourceType)(taskConfig.Content[0] - 1), taskConfig.Content[1]);
                    return UserProfile.InfoBag.CheckResource(res.ToArray());
                case TaskTypes.Special: return state == 2;
            }

            return false;
        }

        static public bool CheckItem(int id)
        {
            TaskConfig taskConfig = ConfigData.GetTaskConfig(id);
            for (int i = 0; i < taskConfig.Content.Length; i += 2)
            {
                if (UserProfile.InfoBag.GetItemCount(taskConfig.Content[i]) < taskConfig.Content[i + 1])
                {
                    return false;
                }
            }
            return true;
        }

        static public string[] GetFinishWord(int id)
        {
            TaskConfig taskConfig = ConfigData.GetTaskConfig(id);
            List<string> words = new List<string>();
            switch (taskConfig.Type)
            {
                case TaskTypes.Item: words.Add(GetItemStr(id)); break;
                case TaskTypes.Teach: words.AddRange(GetTeachStr(id)); break;
                default: words.Add(string.Format("[Upd{0}-3]{1}", id, taskConfig.Name)); break;
            }

            return words.ToArray();
        }

        static private string GetItemStr(int id)
        {
            string itemstr = "";
            TaskConfig taskConfig = ConfigData.GetTaskConfig(id);
            for (int i = 0; i < taskConfig.Content.Length; i += 2)
            {
                if (itemstr.Length > 0)
                {
                    itemstr += '-';
                }
                itemstr += string.Format("{0}-{1}", taskConfig.Content[i], taskConfig.Content[i + 1]);
            }

            return string.Format("[Upd{0}-3&Dit{1}]{2}", id, itemstr, taskConfig.Name);
        }

       static private string[] GetTeachStr(int id)
        {
            TaskConfig taskConfig = ConfigData.GetTaskConfig(id);
            List<string> strs = new List<string>();
            for (int i = 0; i < taskConfig.Content.Length; i += 2)
            {
                strs.Add(string.Format("[Upd{0}-3&Asp{1}-{2}]{3}({4}+{2})", id, taskConfig.Content[i], taskConfig.Content[i + 1], taskConfig.Name, ConfigData.GetEquipAddonConfig(taskConfig.Content[i]).Name));
            }

            return strs.ToArray();
        }

        static public void Award(int id)
        {
            TaskConfig taskConfig = ConfigData.GetTaskConfig(id);
            Profile user = UserProfile.Profile;

            GameResource res = GameResource.Parse(taskConfig.Resource);
            res.Add(GameResourceType.Gold, GetMoneyReal(id));
            if (taskConfig.Card != 0)
            {
                user.InfoCard.AddCard(taskConfig.Card);
            }
            if (taskConfig.Item.Count > 0)
            {
                for (int i = 0; i < taskConfig.Item.Count; i++)
                {
                    if (taskConfig.Item[i].Value == 1)
                        user.InfoBag.AddItem(taskConfig.Item[i].Id, 1);
                    else
                        user.InfoBag.AddEquip(taskConfig.Item[i].Id);
                }
            }
            user.InfoBag.AddResource(res.ToArray());
            user.InfoBasic.AddExp(GetExpReal(id));

            if (taskConfig.Type == TaskTypes.Item)
            {
                for (int i = 0; i < taskConfig.Content.Length; i += 2)
                {
                    user.InfoBag.DeleteItem(taskConfig.Content[i], taskConfig.Content[i + 1]);
                }
            }
            else if (taskConfig.Type == TaskTypes.Resource)
            {
                GameResource subres = new GameResource();
                subres.Add((GameResourceType)(taskConfig.Content[0] - 1), taskConfig.Content[1]);
                user.InfoBag.SubResource(subres.ToArray());
            }
        }

        static public Image GetPreview(int id)
        {
            TaskConfig taskConfig = ConfigData.GetTaskConfig(id);

            ControlPlus.TipImage tipData = new ControlPlus.TipImage();
            tipData.AddTextNewLine(taskConfig.Name, "White", 20);
            string des = taskConfig.Descript;
            while (true)
            {
                tipData.AddTextNewLine(des.Substring(0, Math.Min(des.Length, 20)), "Gray");
                if (des.Length <= 20)
                    break;
                des = des.Substring(20);
            }
            tipData.AddLine();
            tipData.AddTextNewLine("任务指导", "White");
            des = taskConfig.Hint;
            while (true)
            {
                tipData.AddTextNewLine(des.Substring(0, Math.Min(des.Length, 20)), "Lime");
                if (des.Length <= 20)
                    break;
                des = des.Substring(20);
            }
            if (UserProfile.InfoTask.GetTaskStateById(id) == 1)
            {
                int addon = UserProfile.InfoTask.GetTaskAddonById(id);
                if (addon > 0)
                {
                    tipData.AddText(string.Format("({0})", addon), "Red");
                }
            }
            tipData.AddLine();
            tipData.AddTextNewLine(string.Format("奖励-常规 {0}", GetMoneyReal(id)), "Gold");
            tipData.AddImage(HSIcons.GetIconsByEName("res1"));
            tipData.AddText(string.Format(" {0}", GetExpReal(id)), "Purple");
            tipData.AddImage(HSIcons.GetIconsByEName("oth5"));
            if (taskConfig.Card > 0)
            {
                tipData.AddTextNewLine("    -卡片 ", "Gold");
                tipData.AddImage(CardAssistant.GetCard(taskConfig.Card).GetCardImage(30, 30));
            }
            if (taskConfig.Item.Count > 0)
            {
                tipData.AddTextNewLine("    -物品 ", "Gold");
                for (int i = 0; i < taskConfig.Item.Count; i++)
                {
                    if (taskConfig.Item[i].Value == 1)
                        tipData.AddImage(Items.HItemBook.GetHItemImage(taskConfig.Item[i].Id));
                    else
                        tipData.AddImage(Equips.EquipBook.GetEquipImage(taskConfig.Item[i].Id));
                }
            }

            return tipData.Image;
        }
    }
}
