using System.Collections.Generic;
using System.IO;
using TaleofMonsters.Core.Interface;

namespace TaleofMonsters.DataType.User
{
    internal class InfoRecord:INlSerlizable
    {
        private Dictionary<int, int> records = new Dictionary<int, int>();

        public int GetRecordById(int id)
        {
            if (records.ContainsKey(id))
                return records[id];
            return 0;
        }

        public void SetRecordById(int id, int value)
        {
            if (records.ContainsKey(id))
                records[id] = value;
            else
                records.Add(id, value);
        }

        public void AddRecordById(int id, int value)
        {
            if (records.ContainsKey(id))
                records[id] += value;
            else
                records.Add(id, value);
        }


        #region INlSerlizable ≥…‘±

        public void Write(BinaryWriter bw)
        {
            bw.Write(records.Count);
            foreach (KeyValuePair<int, int> keyValuePair in records)
            {
                bw.Write(keyValuePair.Key);
                bw.Write(keyValuePair.Value);
            }
        }

        public void Read(BinaryReader br)
        {
            int count = br.ReadInt32();
            records.Clear();
            for (int i = 0; i < count; i++)
            {
                int key = br.ReadInt32();
                int value = br.ReadInt32();
                records.Add(key, value);
            }
        }

        #endregion
    }
}
