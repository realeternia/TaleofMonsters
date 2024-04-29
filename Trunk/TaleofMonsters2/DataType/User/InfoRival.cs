using System.Collections.Generic;
using System.IO;
using TaleofMonsters.Core.Interface;
using TaleofMonsters.DataType.Achieves;
using TaleofMonsters.DataType.Peoples;

namespace TaleofMonsters.DataType.User
{
    internal class InfoRival : INlSerlizable
    {
        private Dictionary<int, RivalState> rivals = new Dictionary<int, RivalState>();

        public RivalState GetRivalState(int id)
        {
            if (!rivals.ContainsKey(id))
            {
                return new RivalState(id);
            }
            return rivals[id];
        }

        public void AddRivalState(int id, bool isWin)
        {
            if (PeopleBook.IsPeople(id))//打怪不记录战绩
            {
                if (!rivals.ContainsKey(id))
                {
                    rivals[id] = new RivalState(id);
                }
                if (isWin)
                {
                    rivals[id].win++;
                }
                else
                {
                    rivals[id].loss++;
                }
            }

            AchieveBook.CheckByCheckType("fight");
        }

        public void SetRivalAvail(int id)
        {
            if (!rivals.ContainsKey(id))
            {
                rivals[id] = new RivalState(id);
            }

            rivals[id].avail = true;
            AchieveBook.CheckByCheckType("people");
        }

        public Dictionary<int, RivalState> Rivals
        {
            get { return rivals; }
        }

        public int GetRivalAvailCount()
        {
            int count = 0;
            foreach (RivalState state in rivals.Values)
            {
                if (state.avail)
                    count++;
            }
            return count;
        }

        #region INlSerlizable 成员

        public void Write(BinaryWriter bw)
        {
            bw.Write(rivals.Count);
            foreach (KeyValuePair<int, RivalState> keyValuePair in rivals)
            {
                bw.Write(keyValuePair.Key);
                keyValuePair.Value.Write(bw);
            }
        }

        public void Read(BinaryReader br)
        {
            int count = br.ReadInt32();
            rivals.Clear();
            for (int i = 0; i < count; i++)
            {
                int key = br.ReadInt32();
                RivalState rs = new RivalState();
                rs.Read(br);
                rivals.Add(key, rs);
            }
        }

        #endregion
    }
}
