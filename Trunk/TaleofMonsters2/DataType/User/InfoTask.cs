using System.Collections.Generic;
using System.IO;
using ConfigDatas;
using TaleofMonsters.Core;
using TaleofMonsters.Core.Interface;
using TaleofMonsters.DataType.Achieves;
using TaleofMonsters.DataType.Peoples;
using TaleofMonsters.DataType.Tasks;

namespace TaleofMonsters.DataType.User
{
    internal class InfoTask : INlSerlizable
    {
        private Dictionary<int, TaskState> tasks = new Dictionary<int, TaskState>();
        public InfoBag InfoBag;

        public void BeginTask(int tid)
        {
            TaskConfig taskConfig = ConfigData.GetTaskConfig(tid);
            SetTaskStateById(tid, 1);

            MainForm.Instance.AddTip(string.Format("|接受任务-|Lime|{0}", taskConfig.Name), "White");
            int give = taskConfig.ItemGive;
            if (give != 0)
            {
                InfoBag.AddItem(give, 1);
            }

            SoundManager.Play("System", "QuestActivateWhat1.wav");
        }

        public void EndTask(int tid)
        {
            SetTaskStateById(tid, 3);

            AchieveBook.CheckByCheckType("task");
            MainForm.Instance.AddTip(string.Format("|完成任务-|Lime|{0}", ConfigData.GetTaskConfig(tid).Name), "White");
        }

        public Dictionary<int, TaskState> Task
        {
            get { return tasks; }
        }

        public int GetTaskDoneCount()
        {
            int count = 0;
            foreach (TaskState state in tasks.Values)
            {
                if (state.state == 3)
                    count++;
            }
            return count;
        }

        public int GetTaskStateById(int tid)
        {
            if (tasks.ContainsKey(tid))
                return tasks[tid].state;
            return 0;
        }

        public void SetTaskStateById(int tid, int state)
        {
            if (tasks.ContainsKey(tid))
                tasks[tid] = new TaskState(tid, state, tasks[tid].addon);
            else
                tasks.Add(tid, new TaskState(tid, state, 0));

            CheckTaskNeedItem(tid);
            MainForm.Instance.RefreshNpcState();
        }

        public int GetTaskAddonById(int tid)
        {
            if (tasks.ContainsKey(tid))
                return tasks[tid].addon;
            return 0;
        }

        public void UpdateTaskAddonWin(int mid, int tlevel, int addon)
        {
            int tid = 0;
            foreach (TaskState state in tasks.Values)
            {
                TaskConfig taskConfig = ConfigData.GetTaskConfig(state.tid);
                if (state.state == 1)
                {
                    if ((taskConfig.Type == TaskTypes.Won && taskConfig.Content[0] == mid) || PeopleBook.IsMonster(mid) && taskConfig.Type == TaskTypes.WonLevel && taskConfig.Content[0] <= tlevel)
                    {
                        tid = state.tid;
                        break;
                    }
                }
            }
            if (tasks.ContainsKey(tid))
                tasks[tid] = new TaskState(tid, tasks[tid].state, tasks[tid].addon + addon);
        }

        private void CheckTaskNeedItem(int tid)
        {
            if (tasks[tid].state == 1)
            {
                TaskConfig taskConfig = ConfigData.GetTaskConfig(tid);
                if (taskConfig.Type == TaskTypes.Item)
                {
                    for (int i = 0; i < taskConfig.Content.Length; i += 2)
                    {
                        int itemid = taskConfig.Content[i];
                        int itemcount = taskConfig.Content[i + 1];
                        if (InfoBag.GetItemCount(itemid) < itemcount)
                        {
                            InfoBag.tpBonusItem[itemid] += itemcount - InfoBag.GetItemCount(itemid);
                        }
                    }
                }
            }
        }

        #region INlSerlizable 成员

        public void Write(BinaryWriter bw)
        {
            bw.Write(tasks.Count);
            foreach (KeyValuePair<int, TaskState> keyValuePair in tasks)
            {
                bw.Write(keyValuePair.Key);
                keyValuePair.Value.Write(bw);
            }
        }

        public void Read(BinaryReader br)
        {
            int count = br.ReadInt32();
            tasks.Clear();
            for (int i = 0; i < count; i++)
            {
                int key = br.ReadInt32();
                TaskState ts = new TaskState();
                ts.Read(br);
                tasks.Add(key, ts);
            }
        }

        #endregion
    }
}
